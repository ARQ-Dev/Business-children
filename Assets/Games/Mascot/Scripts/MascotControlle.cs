using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotControlle : MonoBehaviour
{
    [SerializeField]
    private GameObject mascotMesh;
    [SerializeField]
    private GameObject logo;

    public delegate void Handler();
    public static event Handler HideGame;

    public delegate void HandlerSaving(Progress foo);
    public static event HandlerSaving SaveProgress;

    public void OnTimelineEnd()
    {
        mascotMesh.SetActive(false);
        logo.SetActive(true);
    }

    private void OnEnable()
    {
        mascotMesh.SetActive(true);
        logo.SetActive(false);
    }

    public void CloseScene()
    {
        SaveState();
        this.gameObject.SetActive(false);
        HideGame();
    }

    public void SaveState()
    {
        if ((int)Game.current.progress > (int)Progress.Cover) return;
        SaveProgress(Progress.Cover);
    }

}
