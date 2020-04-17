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
    private GameObject[] generators;
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
        //generators[i].SetActive(true);
        panelController.OpenPanel(panels[i]);
    }
    public void OnGameClose()
    {
        foreach (var generator in generators)
        {
            //generator.SetActive(false);
        }
        MainCanvasSetActive(true);
        panelController.OpenPanel(mainPanel);
    }
}
