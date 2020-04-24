using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingControlle : MonoBehaviour
{
    public delegate void Handler();
    public static event Handler HideGame;

    public delegate void HandlerSaving(Progress foo);
    public static event HandlerSaving SaveProgress;

    public void CloseScene()
    {
        SaveState();
        this.gameObject.SetActive(false);
        HideGame();
    }

    public void SaveState()
    {
        if ((int)Game.current.progress > (int)Progress.Racing) return;

        SaveProgress(Progress.Racing);
    }

}
