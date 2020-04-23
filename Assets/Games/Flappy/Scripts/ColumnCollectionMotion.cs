using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColumnCollectionMotion : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField] 
    private float maxDisplacement = 1f;
    [SerializeField] 
    private float timeStep = 1f;
    [SerializeField] 
    private float delay = 1.0f;
    [SerializeField] 
    private float moveTime = 1.0f;
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private SceneAssembler sceneAssembler;

    private GameObject columnCollection;
    private List<GameObject> columnPairs;
    private float parentScale;
    public bool isCoroutineStop = false;
    public float rotAcum = 0;

    private void Start()
    {
        InitializeMotion();
    }

    public void InitializeMotion()
    {
        CancelInvoke();
        parentScale = transform.localScale.y;
        columnCollection = gameController.columnClollecion;
        columnPairs = gameController.columnsPairs;
        InvokeRepeating("ColumnsMovement", delay, timeStep);
    }
    public void StopOldMotion()
    {
        StopAllCoroutines();
    }
    private void Update()
    {

        columnCollection.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        rotAcum += rotationSpeed * Time.deltaTime * Mathf.PI / 180;

        if (rotAcum > sceneAssembler.angleStep)
        {
            gameController.UpdateScore();
            gameController.UpdateScoreText();
            rotAcum = 0;

        }
    }

    private void ColumnsMovement()
    {
        foreach (GameObject gmObject in columnPairs)
        {
            if (isCoroutineStop) { break; }
            float columnZPosition = transform.InverseTransformPoint(gmObject.transform.position).z;
            if (columnZPosition > 0) continue;
            float displacementCoeficient = Random.Range(-1f, 1f);
            Vector3 newPosition = new Vector3(0, maxDisplacement * displacementCoeficient - gmObject.transform.localPosition.y, 0) * parentScale;
            StartCoroutine(MoveColumn(newPosition, gmObject.transform, moveTime));
        }
    }


    private IEnumerator MoveColumn(Vector3 movement, Transform columnTransform, float executionTime)
    {

        while (executionTime > 0)
        {
            executionTime -= Time.deltaTime;
            columnTransform.Translate(movement * Time.deltaTime);

            foreach (Transform child in columnTransform.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.name == "column up")
                {
                    child.localScale -= movement * Time.deltaTime * 0.5f / parentScale;
                    child.Translate(-(movement * Time.deltaTime * 0.5f));
                }
                if (child.gameObject.name == "column down")
                {
                    child.localScale += movement * Time.deltaTime * 0.5f / parentScale;
                    child.Translate(-(movement * Time.deltaTime * 0.5f));
                }
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
