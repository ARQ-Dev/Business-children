using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyUIChanger : MonoBehaviour
{
    private bool b = false;
    private int screen = 0;

    private void Start()
    {
        GetComponent<UIController>().ShowScreen(screen);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)) {
            GetComponent<UIController>().SetBackgroungVisibility(b = !b);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<UIController>().SetPhoneRotateVisibility(b = !b);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<UIController>().SetText((b = !b).ToString());
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            GetComponent<UIController>().SetFrameVisibility(b = !b);
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            screen++;

            GetComponent<UIController>().ShowScreen(screen % 7);
        }
    }
}
