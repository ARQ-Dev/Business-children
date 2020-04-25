using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hidder : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    public void HidePanel()
    {
        Destroy(panel);
        SceneManager.LoadScene(2);
    }
}
