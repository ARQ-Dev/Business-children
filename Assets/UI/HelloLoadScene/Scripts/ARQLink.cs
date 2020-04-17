using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARQLink : MonoBehaviour
{
 
    public void OpenPage() 
    {
        Application.OpenURL("http://arq.su");
        Debug.Log("Buuton");
    }

}
