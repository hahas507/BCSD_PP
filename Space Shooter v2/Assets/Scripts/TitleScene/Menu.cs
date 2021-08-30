using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject FadeOut;
    [SerializeField] private GameObject MenuPanel;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CalculateDistance();
        TryOpenMenu();
    }

    private void CalculateDistance()
    {
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance > maxDistance)
        {
            GameManager.isMenuOpen = false;
        }
    }

    private void TryOpenMenu()
    {
        OpenMenu();
        CloseMenu();
    }

    public void OpenMenu()
    {
        MenuPanel.SetActive(GameManager.isMenuOpen);
    }

    #region BOTTONCLICK

    public void CloseMenu()
    {
        MenuPanel.SetActive(GameManager.isMenuOpen);
    }

    public void ClickToCloseMenu()
    {
        GameManager.isMenuOpen = false;
    }

    public void ClickToSave()
    {
        //Save Game;
    }

    public void LoadBattleTest()
    {
        FadeOut.SetActive(true);
        GameManager.sceneNameToLoad = "Battle_Test";
    }

    public void LoadStage01()
    {
        FadeOut.SetActive(true);
        GameManager.sceneNameToLoad = "Battle_Level_01";
    }

    #endregion BOTTONCLICK
}