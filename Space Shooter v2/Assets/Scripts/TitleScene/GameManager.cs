using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManager = new GameObject("GameManager");
                gameManager.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public static bool isMenuOpen = false;
    public static string sceneNameToLoad = null;

    public static bool Stage01Cleared = false;
    public static bool Stage02Cleared = false;
    public static bool Stage03Cleared = false;

    private void Awake()
    {
        _instance = this;
    }
}