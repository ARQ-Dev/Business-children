using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject percentText;
    [SerializeField]
    private GameObject activeLoadLine;
    [SerializeField]
    private GameObject loadLine;

    private int progr = 0;


    void Start()
    {
        SetLoadProgress(0);
    }

    public void SetLoadProgress(int progress) {
        percentText.GetComponent<Text>().text = progress.ToString() + '%';

        float loadSize = loadLine.GetComponent<RectTransform>().sizeDelta.x;
        float progressSize = loadSize * progress / 100f;
        activeLoadLine.GetComponent<RectTransform>().sizeDelta = new Vector2(progressSize, 4);
        activeLoadLine.GetComponent<RectTransform>().localPosition = new Vector3(-134+progressSize/2,0,0);
    }


}
