using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[] StageCleared;

    public GameData()
    {
        StageCleared = new bool[3];
        StageCleared[0] = GameManager.Stage01Cleared;
        StageCleared[1] = GameManager.Stage02Cleared;
        StageCleared[2] = GameManager.Stage03Cleared;
    }
}