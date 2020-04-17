using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void OnValueChanged(bool v) {
        if (v)
        {
            this.GetComponentInChildren<Text>().text = "ЗАПУСТИТЬ";
        }
        else {
            this.GetComponentInChildren<Text>().text = "ЗАВЕРШИТЬ";
        }
    }
}
