using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject phone_rotate;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameObject frame;

    [SerializeField]
    private GameObject text;

    [SerializeField]
    private GameObject play;

    [SerializeField]
    private GameObject gaz;

    [SerializeField]
    private GameObject left;

    [SerializeField]
    private GameObject skip;

    [SerializeField]
    private GameObject dropdown;

    private string[] screens = {"first-1", "first-2", "race-1", "race-2", "puzzle-1", "puzzle-2", "main-1"};

    public void ShowScreen(int index) {
        switch (index) {
            case 0:
                Debug.Log(screens[index]);
                HideAll();
                SetFrameVisibility(true);
                SetBackgroungVisibility(true);
                SetText("Покажи камере титульную страницу твоей книги");
                break;

            case 1:
                Debug.Log(screens[index]);
                HideAll();
                SetSkipVisibility(true);
                break;

            case 2:
                Debug.Log(screens[index]);
                HideAll();
                SetBackgroungVisibility(true);
                SetFrameVisibility(true);
                SetPhoneRotateVisibility(true);
                SetText("Открой страницу 34, обведи маркером линии и после покажи что получилось камере перевернув свой смартфон");
                break;

            case 3:
                Debug.Log(screens[index]);
                HideAll();
                SetPlayVisibility(true);
                SetGazVisibility(true);
                SetLeftVisibility(true);
                break;

            case 4:
                Debug.Log(screens[index]);
                HideAll();
                SetBackgroungVisibility(true);
                SetFrameVisibility(true);
                SetText("Открой страницу 87, покажи камере картину «Новгородский Торг», собери пазл и получи игру");
                break;

            case 5:
                Debug.Log(screens[index]);
                HideAll();
                SetSkipVisibility(true);
                break;

            case 6:
                Debug.Log(screens[index]);
                HideAll();
                SetDropdownVisibility(true);
                break;
        }
    }

    //show/hide phone-rotate
    public void SetPhoneRotateVisibility(bool visibility) {
        phone_rotate.SetActive(visibility);
    }

    //show/hide background
    public void SetBackgroungVisibility(bool visibility)
    {
        background.SetActive(visibility);
    }

    //show/hide frame
    public void SetFrameVisibility(bool visibility)
    {
        frame.SetActive(visibility);
    }

    public void SetText(string t)
    {
        text.GetComponent<Text>().text = t;
    }

    public void SetPlayVisibility(bool visibility)
    {
        play.SetActive(visibility);
    }

    public void SetGazVisibility(bool visibility)
    {
        gaz.SetActive(visibility);
    }

    public void SetLeftVisibility(bool visibility)
    {
        left.SetActive(visibility);
    }

    public void SetSkipVisibility(bool visibility)
    {
        skip.SetActive(visibility);
    }

    public void SetDropdownVisibility(bool visibility)
    {
        dropdown.SetActive(visibility);
    }

    public void HideAll() {
        SetBackgroungVisibility(false);
        SetDropdownVisibility(false);
        SetFrameVisibility(false);
        SetGazVisibility(false);
        SetLeftVisibility(false);
        SetPhoneRotateVisibility(false);
        SetPlayVisibility(false);
        SetSkipVisibility(false);
        SetText(" ");
    }
}
