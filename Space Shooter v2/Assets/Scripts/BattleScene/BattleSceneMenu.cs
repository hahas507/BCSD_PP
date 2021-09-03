using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneMenu : MonoBehaviour
{
    [SerializeField] private GameObject FadeOut;

    public void ReturnToLobby()
    {
        BattleSceneManager.isSceneChanging = true;
        FadeOut.SetActive(true);
    }

    public void SaveGameAndReturnToLobby()
    {
        SaveOrLoad.SaveData();
        FadeOut.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}