using UnityEngine;
using UnityEngine.UI;

public class MyButtonHandler : MonoBehaviour
{
    public void OnValueChanged(bool v)
    {
        if (v)
        {
            this.GetComponentInChildren<Text>().text = "ЗАПУСТИТЬ";
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "ЗАВЕРШИТЬ";
        }
    }
}
