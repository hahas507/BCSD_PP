using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject ClearedPanel;
    [SerializeField] private GameObject FailedPanel;

    public static bool isMenuOpen = false;
    public static bool isPlayerDead = false;
    public static bool isMainTargetDefeated = false;

    private bool stopTime = false;

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
        if (stopTime)
        {
            if (!isMenuOpen)
            {
                Time.timeScale = 1f;
            }
            else if (isMenuOpen)
            {
                Time.timeScale = 0f;
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
            stopTime = true;
            MainPanel.SetActive(isMenuOpen);
            MenuPanel.SetActive(isMenuOpen);
        }

        if (isMainTargetDefeated)
        {
            isMenuOpen = true;
            stopTime = false;
            MainPanel.SetActive(isMenuOpen);
            ClearedPanel.SetActive(isMenuOpen);
        }

        if (isPlayerDead)
        {
            isMenuOpen = true;
            stopTime = false;
            MainPanel.SetActive(isMenuOpen);
            FailedPanel.SetActive(isMenuOpen);
        }
    }
}