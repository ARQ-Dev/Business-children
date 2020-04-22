using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropdown : MonoBehaviour
{

    #region Serialized

    [SerializeField]
    private GameObject mainButton;
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Sprite panelSprite;
    [SerializeField]
    private RectTransform arrow;
    #endregion

    private Sprite mainButtonSprite;
    private Image mainButtonImage;
    private Image panelImage;
    private Color transperent = new Color(1, 1, 1, 0);
    private Color notTransperent = new Color(1, 1, 1, 1);
    private Vector3 openRot = new Vector3(0, 0, 180);
    private Vector3 closeRot = new Vector3(0, 0, 0);

    private bool isOpen = false;

    private void Start()
    {
        mainButtonImage = mainButton.GetComponent<Image>();
        mainButtonSprite = mainButtonImage.sprite;

        panelImage = panel.GetComponent<Image>();

    }

    public void OnClick()
    {
        Activate(!isOpen);
        isOpen = !isOpen;
    }

   public void Activate(bool isActive)
    {
       
        foreach(var x in buttons)
        {
            x.SetActive(isActive);
        }

        panelImage.sprite = isActive ? panelSprite : null;
        panelImage.color = !isActive ? transperent : notTransperent;
        mainButtonImage.sprite = !isActive ? mainButtonSprite : null;
        mainButtonImage.color = isActive ? transperent : notTransperent;
        arrow.eulerAngles = isActive ? openRot : closeRot;
    }

    private void OnDisable()
    {
        isOpen = false;
        Activate(false);
    }
}
