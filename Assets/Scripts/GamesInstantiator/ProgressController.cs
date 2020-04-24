using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class ProgressController : MonoBehaviour
{
    [SerializeField]
    private EFE_Base panelController;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private PanelActivator panelActivator;
    private Game game = new Game();

    public static bool isFlappyAvaible = false;

    private void OnEnable()
    {
        //SaveProgress(Progress.Cover);
        string path = Application.persistentDataPath + "/savedProgress.gd";
        if (File.Exists(path))
        {
            game.progress = LoadProgress();
            Game.current = game;
            Debug.Log(Game.current.progress);
        }
        else
        {
            SaveProgress(Progress.InitialState);
            game.progress = Progress.InitialState;
            Game.current = game;
        }

        if (Game.current.progress == Progress.InitialState)
        {
            //panelController.firstPanel = coverPanel;
            panelActivator.GetAllCollections();
            panelActivator.ActivateGenerator(0);
        }
        else
        {
            panelController.firstPanel = mainPanel;
        }
        MascotControlle.SaveProgress += SaveProgress;
        RacingControlle.SaveProgress += SaveProgress;
        PuzzleGameController.SaveProgress += SaveProgress;
    }

    public void SaveProgress(Progress progress)
    {

        game.progress = progress;
        Game.current = game;
        SaveLoad.Save();

    }
    Progress LoadProgress()
    {
        Game loadedGame = SaveLoad.LoadGame();
        return loadedGame.progress;
    }


}
