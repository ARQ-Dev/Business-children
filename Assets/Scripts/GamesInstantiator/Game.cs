using UnityEngine;
using System.Collections;


[System.Serializable]
public class Game
{
    public static Game current;
    public Progress progress;

    public Game()
    {
        progress = Progress.InitialState;

    }
}

public enum Progress
{
    InitialState = 0,
    Cover = 1,
    Racing = 2,
    Puzzle = 3
}