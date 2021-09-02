using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject ResultPanel;

    public static bool isMenuOpen = false;
    public static bool isPlayerDead = false;
    public static bool isMainTargetDefeated = false;
    public static bool isSceneChanging = false;

    private void Start()
    {
        isMenuOpen = false;
    }

    private void Update()
    {
        TryOpenMenu();
        TimeScale();
        MouseVisible();
    }

    private void MouseVisible()
    {
        if (isMenuOpen == true)
        {
            Cursor.visible = true;
        }
        else if (isMenuOpen == false)
        {
            Cursor.visible = false;
        }
    }

    private void TimeScale()
    {
        if (!isMainTargetDefeated)
        {
            if (!isMenuOpen)
            {
                isSceneChanging = false;
                Time.timeScale = 1f;
            }
            else if (isMenuOpen)
            {
                Time.timeScale = 0f;

                if (isSceneChanging)
                {
                    Time.timeScale = 1f;
                }
            }
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void TryOpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPlayerDead && !isMainTargetDefeated)
        {
            isMenuOpen = !isMenuOpen;
            MainPanel.SetActive(isMenuOpen);
            MenuPanel.SetActive(isMenuOpen);
        }

        if (isPlayerDead || isMainTargetDefeated)
        {
            isMenuOpen = true;
            MainPanel.SetActive(isMenuOpen);
            ResultPanel.SetActive(isMenuOpen);
        }
    }
}