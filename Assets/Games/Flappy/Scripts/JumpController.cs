using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class JumpController : MonoBehaviour
{
    [SerializeField] GameObject fireObject;
    [SerializeField] float fireDuration = 1f;
    public Animator animator;
    public float jumpForce = 200f;
    public float minVelocity = -2.0f;
    public float maxVelocity = 2.0f;
    public float gravityConst = 0.0f;

    //[SerializeField] BoxCollider fall;
    //[SerializeField] BoxCollider fly;


    private Rigidbody characterRigidBody;
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField]
    EventSystem m_EventSystem;

    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody>();
        var gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = gravityConst * transform.TransformDirection(transform.up);



    }


    void Update()
    {
        //if (Input.touchCount < 1) return;
        if (Input.GetMouseButtonDown(0))
        //if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
        

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                characterRigidBody.velocity = Vector3.zero;
                characterRigidBody.angularVelocity = Vector3.zero;
                return;
            }


            characterRigidBody.AddForce(transform.up * jumpForce);
            animator.SetTrigger("Jump");
            fireObject.SetActive(true);
            CancelInvoke();

            //fly.enabled = true;
            //fall.enabled = false;

            Invoke("FireOff", fireDuration);
        }
        characterRigidBody.velocity = new Vector3(0, Mathf.Clamp(characterRigidBody.velocity.y, minVelocity, maxVelocity), 0);

    }
    private void OnCollisionEnter(Collision collision)
    {
        characterRigidBody.velocity = new Vector3(0, 0, 0);
        characterRigidBody.angularVelocity = Vector3.zero;
        characterRigidBody.Sleep();
        gameController.CharacterCollided();

    }

    void FireOff()
    {
        fireObject.SetActive(false);
        //fly.enabled = false;
        //fall.enabled = true;
    }


}

