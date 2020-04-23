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
    private FocusSquare flappyInstantiator;
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
        panelController.OpenPanel(panels[i]);
        if (i != 3)
        {
            if (gamesInstantiator.isActiveAndEnabled)
                gamesInstantiator.SetCurrentGame(gamesInstantiator.list_Names[i]);
            else
            {
                gamesInstantiator.gameObject.SetActive(true);
                gamesInstantiator.SetCurrentGame(gamesInstantiator.list_Names[i]);
            }
            gamesInstantiator.HideCanvas += MainCanvasSetActive;
            MascotControlle.HideGame += OnGameClose;
            RacingControlle.HideGame += OnGameClose;
            PuzzleGameController.HideGame += OnGameClose;
        }
        else
        {
            flappyInstantiator.gameObject.SetActive(true);
            flappyInstantiator.HideCanvas += MainCanvasSetActive;
            GameController.HideGame += OnGameClose;
        }
    }

    public void OnGameClose()
    {
        flappyInstantiator.gameObject.SetActive(false);
        flappyInstantiator.HideCanvas -= MainCanvasSetActive;
        gamesInstantiator.HideCanvas -= MainCanvasSetActive;
        MascotControlle.HideGame -= OnGameClose;
        RacingControlle.HideGame -= OnGameClose;
        PuzzleGameController.HideGame -= OnGameClose;
        GameController.HideGame -= OnGameClose;
        gamesInstantiator.gameObject.SetActive(false);
        MainCanvasSetActive(true);
        panelController.OpenPanel(mainPanel);
    }
}
