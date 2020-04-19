using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    [SerializeField]
    private EFE_Base panelController;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject mainCanvas;
    [SerializeField]
    private GamesInstantiator gamesInstantiator;
    [SerializeField]
    private GameObject[] panels;

    private void OnEnable()
    {
        panelController.firstPanel = mainPanel;
        //generators[0].SetActive(false);
    }

   
    public void MainCanvasSetActive(bool isActive)
    {
        mainCanvas.SetActive(isActive);
    }

    public void ActivateGenerator(int i)
    {
        if (gamesInstantiator.isActiveAndEnabled)
            gamesInstantiator.SetCurrentGame(gamesInstantiator.list_Names[i]);
        else
        {
            gamesInstantiator.gameObject.SetActive(true);
            gamesInstantiator.SetCurrentGame(gamesInstantiator.list_Names[i]);
        }
        panelController.OpenPanel(panels[i]);
    }
    public void OnGameClose()
    {
        gamesInstantiator.gameObject.SetActive(false);
        MainCanvasSetActive(true);
        panelController.OpenPanel(mainPanel);
    }
}
