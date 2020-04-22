
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameController : MonoBehaviour//Singleton<PuzzleGameController>
{
    [SerializeField] private float satisfyingRadius = 0.05f;
    [SerializeField] private GameObject elementsCollection;
    [SerializeField] private GameObject fakeElementsCollection;
    [SerializeField] private float distributionRadius = 0.45f;

    private int elementsCount = 0;

    private Dictionary<GameObject, Vector3> correctTransforms = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        //foreach (Transform elementTransform in elementsCollection.transform)
        //{
        //    correctTransforms.Add(elementTransform.gameObject, elementTransform.localPosition);
        //    //elementTransform.localPosition = 
        //    //elementTransform.localPosition = new Vector3(Random.Range(-distributionRadius, distributionRadius), Random.Range(-distributionRadius, distributionRadius), elementTransform.localPosition.z);
        //}

        //var elements = elementsCollection.t

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
            Debug.Log(elementsCount);
            if(elementsCount == 24) 
            {
                /*if ((int)Game.current.progress < (int)Progress.Puzzle)
                {
                    GlobalGameController.Instance.SaveProgress(Progress.Puzzle);
                    CloseScene();
                    GlobalGameController.Instance.ActivateGenerator(3);
                };*/
           
            }

            return true;
        }
        //Debug.Log(Vector3.Distance(elementTransform.position, correctPosition));
        return false;
    }

    public void CloseScene()
    {
        this.gameObject.SetActive(false);
        //GlobalGameController.Instance.OnGameClose();
    }



}
