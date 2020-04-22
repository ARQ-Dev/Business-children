using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingControlle : MonoBehaviour
{
    public delegate void Handler();
    public static event Handler HideGame;

    public void CloseScene()
    {
        //SaveState();
        //GlobalGameController.Instance.OnGameClose();
        this.gameObject.SetActive(false);
        HideGame();
    }

    public void SaveState()
    {
        //if ((int)Game.current.progress > (int)Progress.Racing) return;
        //GlobalGameController.Instance.SaveProgress(Progress.Racing);
    }

}
