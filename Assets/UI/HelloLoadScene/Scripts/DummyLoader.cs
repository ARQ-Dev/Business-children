using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLoader : MonoBehaviour
{

    public void StartProgressBar() 
    {
        StartCoroutine(Load());
      
    }



    IEnumerator Load()
    {
        for (int p = 0; p <= 100; p++)
        {
            GetComponent<LoadDisplayer>().SetLoadProgress(p);
            yield return new WaitForSecondsRealtime(0.01f); 
        }
       
    }
}
