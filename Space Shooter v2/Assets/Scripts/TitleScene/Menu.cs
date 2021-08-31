using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject FadeOut;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject[] ClearedText;
    [SerializeField] private string openSound;
    [SerializeField] private string closeSound;

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
        SoundManager.instance.PlaySE(openSound);
        if (GameManager.Stage01Cleared)
        {
            ClearedText[0].SetActive(true);
        }
        else if (GameManager.Stage02Cleared)
        {
            ClearedText[1].SetActive(true);
        }
    }

    #region BOTTONCLICK

    public void CloseMenu()
    {
        MenuPanel.SetActive(GameManager.isMenuOpen);
        SoundManager.instance.PlaySE(closeSound);
    }

    public void ClickToCloseMenu()
    {
        GameManager.isMenuOpen = false;
    }

    public void ClickToSave()
    {
        SaveOrLoad.SaveData();
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