using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    public static bool isMenuOpen = false;
    public static string sceneNameToLoad = null;

    private void Update()
    {
        TryOpenMenu();
    }

    private void TryOpenMenu()
    {
        OpenMenu();
        CloseMenu();
    }

    public void OpenMenu()
    {
        MenuPanel.SetActive(isMenuOpen);
    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(isMenuOpen);
    }

    public void ClickToCloseMenu()
    {
        isMenuOpen = false;
    }
}