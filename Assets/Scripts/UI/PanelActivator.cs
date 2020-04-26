using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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

    [SerializeField]
    private ARPlaneManager _arPlaneManager;


    private void OnEnable()
    {
        //panelController.firstPanel = mainPanel;
        //generators[0].SetActive(false);
    }

   
    public void MainCanvasSetActive(bool isActive)
    {
        mainCanvas.SetActive(isActive);
    }
    
    public void GetAllCollections()
    {
        gamesInstantiator.GetCollections();
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
            StartPlaneManager();
            flappyInstantiator.gameObject.SetActive(true);
            flappyInstantiator.HideCanvas += MainCanvasSetActive;
            GameController.HideGame += OnGameClose;
            GameController.HideGame += StopPlaneManager;
        }
    }

    public void StopPlaneManager()
    {
        _arPlaneManager.enabled = false;
        
    }


    public void StartPlaneManager()
    {
        _arPlaneManager.enabled = true;
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
        GameController.HideGame -= StopPlaneManager;
        gamesInstantiator.gameObject.SetActive(false);
        MainCanvasSetActive(true);
        panelController.OpenPanel(mainPanel);
    }
}
