using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Puzzle{
    public class GameConroller : MonoBehaviour //Singleton<GameConroller>
    {
        [SerializeField] private float satisfyingRadius = 0.01f;
        [SerializeField] private GameObject elementsCollection;

        private Dictionary<GameObject, Transform> correctTransforms = new Dictionary<GameObject, Transform>();

        private void Awake()
        {
            foreach (Transform elementTransform in elementsCollection.transform)
            {
                correctTransforms.Add(elementTransform.gameObject, Instantiate(elementTransform));
            }
        }

        public void CheckPosition(GameObject element)
        {
            Transform elementTransform = element.transform;

            Transform correctTransform;
            correctTransforms.TryGetValue(element, out correctTransform);

            if (Vector3.Distance(elementTransform.position, correctTransform.position) < satisfyingRadius)
            {
                elementTransform = correctTransform;

            }
        }

    }
}

