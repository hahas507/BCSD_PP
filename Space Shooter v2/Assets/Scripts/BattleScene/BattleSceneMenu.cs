using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneMenu : MonoBehaviour
{
    [SerializeField] private GameObject FadeOut;

    public void ReturnToLobby()
    {
        FadeOut.SetActive(true);
        BattleSceneManager.isSceneChanging = true;
    }

    public void SaveGameAndReturnToLobby()
    {
        //Save
        FadeOut.SetActive(true);
        BattleSceneManager.isSceneChanging = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}