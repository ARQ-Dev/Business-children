using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAssembler : MonoBehaviour
{
    [SerializeField]
    private int columnCount = 12;
    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private float clearance = 0.5f;
    [SerializeField]
    private float columnHeigth = 1f;
    [SerializeField]
    private Material columnMaterial;
    [SerializeField]
    private float columnWidthCoeficient = 0.3f;

    [SerializeField]
    private GameController gameController;

    private float columnWidth;
   
    private float cupWidth;
    private float cupHeight;

    private GameObject columnCollection;
    private List<GameObject> columnPairs = new List<GameObject>();

    public float angleStep;
    private void Awake()
    {
        angleStep = 2 * Mathf.PI / columnCount;
        AssembleScene();
     
    }

    public void AssembleScene()
    {
        columnWidth = columnWidthCoeficient * columnHeigth;
        cupWidth = 1.5f * columnWidth;
        cupHeight = 0.5f * columnWidth;

    
        float angle = 0;

        columnCollection = EmtyGameObject("column collection", transform, new Vector3(0, 0, 0));

        for (int i = 0; i < columnCount; i++)
        {
            angle += angleStep;
            float x = radius * Mathf.Sin(angle);
            float z = radius * Mathf.Cos(angle);

            columnPairs.Add(CreateColumnPair(columnCollection.transform, new Vector3(x, 0, z)));

        }
        columnCollection.transform.localScale = new Vector3(1, 1, 1);
        columnCollection.transform.localEulerAngles = new Vector3(0, 0, 0);
        gameController.columnClollecion = columnCollection;
        gameController.columnsPairs = columnPairs;
    }

    public void ReAssembleScene()
    {
        columnWidth = columnWidthCoeficient * columnHeigth;
        cupWidth = 1.5f * columnWidth;
        cupHeight = 0.5f * columnWidth;

        float angleStep = 2 * Mathf.PI / columnCount;
        float angle = 0;

        for (int i = 0; i < columnCount; i++)
        {
            angle += angleStep;
            float x = radius * Mathf.Sin(angle);
            float z = radius * Mathf.Cos(angle);

            columnPairs[i].transform.localPosition = new Vector3(x, 0, z);



            foreach (Transform pairComponent in columnPairs[i].transform)
            {
                float offset = 0.5f * (clearance + columnHeigth * 2.0f);

                if (pairComponent.name == "column up")
                {
                    pairComponent.transform.localPosition = new Vector3(0, offset, 0);
                    pairComponent.transform.localScale = new Vector3(columnWidth, columnHeigth, columnWidth);
                   
                }

                if (pairComponent.name == "column down")
                {
                    pairComponent.transform.localPosition = new Vector3(0, -offset, 0);
                    pairComponent.transform.localScale = new Vector3(columnWidth, columnHeigth, columnWidth);
                
                }
                offset = 0.5f * (clearance + cupHeight * 2.0f);
                if (pairComponent.name == "cup up")
                {
                    pairComponent.transform.localPosition = new Vector3(0, offset, 0);
                    pairComponent.transform.localScale = new Vector3(cupWidth, cupHeight, cupWidth);
                   
                }

                if (pairComponent.name == "cup down")
                {
                    pairComponent.transform.localPosition = new Vector3(0, -offset, 0);
                    pairComponent.transform.localScale = new Vector3(cupWidth, cupHeight, cupWidth);

                }
               

            }

        }

        columnCollection.transform.localScale = new Vector3(1, 1, 1);
        columnCollection.transform.localEulerAngles = new Vector3(0, 0, 0);


    }
    private GameObject EmtyGameObject(string objectName, Transform parentTransform, Vector3 localPosition)
    {
        GameObject gmObject = new GameObject(objectName);
        gmObject.transform.parent = parentTransform;
        gmObject.transform.localPosition = localPosition;
        return gmObject;
    }

    private GameObject CreatePrimitive(string objectName, PrimitiveType type, Transform parentTransform, Vector3 localPosition, Vector3 localScale)
    {
        GameObject primitive = GameObject.CreatePrimitive(type);
        primitive.name = objectName;
        primitive.transform.parent = parentTransform;
        primitive.transform.localPosition = localPosition;
        primitive.transform.localScale = localScale;
        primitive.GetComponent<MeshRenderer>().receiveShadows = false;
        return primitive;
    }

    private GameObject CreateColumnPair(Transform parentTransform, Vector3 localPosition)
    {
        GameObject columnPair = new GameObject("pair");
        GameObject currentObject;
        columnPair.transform.parent = parentTransform;
        columnPair.transform.localPosition = localPosition;

        float offset = 0.5f * (clearance + columnHeigth * 2.0f);
        currentObject = CreatePrimitive("column up", PrimitiveType.Cylinder, columnPair.transform, new Vector3(0, offset, 0), new Vector3(columnWidth, columnHeigth, columnWidth));
        currentObject.GetComponent<MeshRenderer>().material = columnMaterial;
        currentObject.GetComponent<CapsuleCollider>().enabled = false;
        currentObject.AddComponent<BoxCollider>();
        currentObject = CreatePrimitive("column down", PrimitiveType.Cylinder, columnPair.transform, new Vector3(0, -offset, 0), new Vector3(columnWidth, columnHeigth, columnWidth));
        currentObject.GetComponent<MeshRenderer>().material = columnMaterial;
        currentObject.GetComponent<CapsuleCollider>().enabled = false;
        currentObject.AddComponent<BoxCollider>();
        offset = 0.5f * (clearance + cupHeight * 2.0f);
        currentObject = CreatePrimitive("cup up", PrimitiveType.Cylinder, columnPair.transform, new Vector3(0, offset, 0), new Vector3(cupWidth, cupHeight, cupWidth));
        currentObject.GetComponent<MeshRenderer>().material = columnMaterial;
        currentObject.GetComponent<CapsuleCollider>().enabled = false;
        currentObject.AddComponent<BoxCollider>();
        currentObject = CreatePrimitive("cup down", PrimitiveType.Cylinder, columnPair.transform, new Vector3(0, -offset, 0), new Vector3(cupWidth, cupHeight, cupWidth));
        currentObject.GetComponent<MeshRenderer>().material = columnMaterial;
        currentObject.GetComponent<CapsuleCollider>().enabled = false;
        currentObject.AddComponent<BoxCollider>();
        return columnPair;
    }

}
