using UnityEngine;
using UnityEngine.UI;

namespace Puzzle{
    public class PuzzleUserInput : MonoBehaviour
    {

        //[SerializeField] private GameObject textObject;

        private Transform elementTransform;
        public PuzzleGameController puzzleGameController;
        private void Update()
        {
            //if (Input.touchCount < 1) return;
            //var touch = Input.GetTouch(0);
            //var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);

            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(touch.position);


            //if (touch.phase == TouchPhase.Began)
            //{
            //        if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Element")))
            //        {
            //        textObject.GetComponent<Text>().text = hit.transform.gameObject.name;
            //        elementTransform = hit.transform;
            //        }
            //}
            //if (touch.phase == TouchPhase.Moved)
            //{       

            //    if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Surface")))
            //    {
            //        if (elementTransform == null) return;
            //if (PuzzleGameController.Instance.CheckPosition(elementTransform.gameObject))
            //{

            //    elementTransform = null;
            //    return;
            //}
            //        elementTransform.position = hit.point;
            //    }

            //}

            //if (touch.phase == TouchPhase.Ended)
            //{
            //    elementTransform = null;
            //}
            var touch = Input.mousePosition;
            var screenPosition = Camera.main.ScreenToViewportPoint(touch);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch);

            
            if (Input.GetMouseButtonDown(0))
            {
               
                if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Element")))
                {
                    elementTransform = hit.transform;
                }
            }
            if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Surface")))
            {
                print(elementTransform);
                if (elementTransform == null) return;
                if(puzzleGameController.CheckPosition(elementTransform.gameObject))
                {             
                    elementTransform = null;
                    return;
                }
                elementTransform.position = hit.point;
            }

            if (Input.GetMouseButtonUp(0))
            {
                elementTransform = null;
            }

        }


    }
}
