using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class FocusSquare : MonoBehaviour
{
    public delegate void Handler(bool isActive);
    public event Handler HideCanvas;
    public enum FocusState
    {
        Initializing,
        Finding,
        Found
    }

    public GameObject findingSquare;
    public GameObject foundSquare;

    [SerializeField]
    private ARRaycastManager m_RaycastManager;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float angleOffset;
    [SerializeField]
    private Button btn;

    //for editor version
    public float maxRayDistance = 30.0f;
    public LayerMask collisionLayerMask;
    public float findingSquareDist = 0.5f;

    private FocusState squareState;
    public FocusState SquareState
    {
        get
        {
            return squareState;
        }
        set
        {
            squareState = value;
            foundSquare.SetActive(squareState == FocusState.Found);
            findingSquare.SetActive(squareState != FocusState.Found);
        }
    }
    bool trackingInitialized;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    // Use this for initialization
    void Start()
    {
        SquareState = FocusState.Initializing;
        trackingInitialized = true;
        
    }

    bool HitTestWithResultType(Vector3 point)
    { 
        
        if (m_RaycastManager.Raycast(point, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            foundSquare.transform.position = hitPose.position;
            foundSquare.transform.rotation = Quaternion.identity;

            return true;
        }

        return false;
    }

    void Update()
    {
        if (SquareState == FocusState.Found)
        {
            btn.gameObject.SetActive(true);
        }
        else
        {
            btn.gameObject.SetActive(false);
        }

        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, findingSquareDist);

        if (HitTestWithResultType(center))
		{
			SquareState = FocusState.Found;
			return;
		}

        


        //if you got here, we have not found a plane, so if camera is facing below horizon, display the focus "finding" square
        if (trackingInitialized)
        {
            SquareState = FocusState.Finding;

            //check camera forward is facing downward
            if (Vector3.Dot(Camera.main.transform.forward, Vector3.down) > 0)
            {

                //position the focus finding square a distance from camera and facing up
                findingSquare.transform.position = Camera.main.ScreenToWorldPoint(center);

                //vector from camera to focussquare
                Vector3 vecToCamera = findingSquare.transform.position - Camera.main.transform.position;

                //find vector that is orthogonal to camera vector and up vector
                Vector3 vecOrthogonal = Vector3.Cross(vecToCamera, Vector3.up);

                //find vector orthogonal to both above and up vector to find the forward vector in basis function
                Vector3 vecForward = Vector3.Cross(vecOrthogonal, Vector3.up);


                findingSquare.transform.rotation = Quaternion.LookRotation(vecForward, Vector3.up);

            }
            else
            {
                //we will not display finding square if camera is not facing below horizon
                findingSquare.SetActive(false);
            }

        }

    }

    public void BtnPressed()
    {
        if(SquareState == FocusState.Found)
        {
            HideCanvas(false);
            findingSquare.SetActive(false);
            foundSquare.SetActive(false);
            var instantiatedPrefab = Instantiate(prefab);
            instantiatedPrefab.transform.position = new Vector3(foundSquare.transform.position.x, foundSquare.transform.position.y + offset, foundSquare.transform.position.z);
            instantiatedPrefab.transform.rotation = Quaternion.LookRotation(foundSquare.transform.position - Camera.current.transform.position);
            instantiatedPrefab.transform.eulerAngles = new Vector3(0, instantiatedPrefab.transform.eulerAngles.y + angleOffset, 0);
            this.gameObject.SetActive(false);
        }
    }

}
