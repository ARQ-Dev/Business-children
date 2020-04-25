
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameController : MonoBehaviour
{
    public delegate void Handler();
    public static event Handler HideGame;

    public delegate void HandlerSaving(Progress foo);
    public static event HandlerSaving SaveProgress;

    [SerializeField] private float satisfyingRadius = 0.05f;
    [SerializeField] private GameObject elementsCollection;
    [SerializeField] private GameObject fakeElementsCollection;

    private int elementsCount = 0;

    private Dictionary<GameObject, Vector3> correctTransforms = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        for (int i = 0; i < elementsCollection.transform.childCount; i++)
        {
            correctTransforms.Add(elementsCollection.transform.GetChild(i).gameObject, elementsCollection.transform.GetChild(i).localPosition);
            elementsCollection.transform.GetChild(i).localPosition = fakeElementsCollection.transform.GetChild(i).localPosition;
        }

    }

    public bool CheckPosition(GameObject element)
    {
        Transform elementTransform = element.transform;

        Vector3 correctPosition;
        correctTransforms.TryGetValue(element, out correctPosition);

        if (Vector3.Distance(elementTransform.localPosition, correctPosition) < satisfyingRadius)
        {
            elementTransform.localPosition = correctPosition;
            elementTransform.localEulerAngles = Vector3.zero;
            elementTransform.gameObject.layer = 0;
            Rigidbody rb = elementTransform.gameObject.GetComponent<Rigidbody>();
            Destroy(rb);
            Collider col = elementTransform.gameObject.GetComponent<BoxCollider>();
            Destroy(col);

            elementsCount++;
            if(elementsCount == 24) 
            {

                HideGame();
                if ((int)Game.current.progress < (int)Progress.Puzzle)
                {
                    SaveProgress(Progress.Puzzle);
                    ProgressController.isFlappyAvaible = true;
                    CloseScene();
                };
           
            }

            return true;
        }
        //Debug.Log(Vector3.Distance(elementTransform.position, correctPosition));
        return false;
    }

    public void CloseScene()
    {
        this.gameObject.SetActive(false);
        HideGame();
    }



}
