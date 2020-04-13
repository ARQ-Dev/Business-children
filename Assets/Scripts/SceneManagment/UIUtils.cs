using UnityEngine;
using UnityEngine.SceneManagement;
public class UIUtils : MonoBehaviour
{

    public void OpenScene(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void OpenWepPage(string url)
    {
        Application.OpenURL(url);
    }

}
