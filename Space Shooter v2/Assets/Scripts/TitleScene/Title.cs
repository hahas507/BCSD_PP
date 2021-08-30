using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Title instance;
    private FadeToLoad FtL;
    [SerializeField] private GameObject FadeOut;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClickStartGame()
    {
        GameData data = SaveOrLoad.loadData();
        if (data != null)
        {
            GameManager.Stage01Cleared = data.StageCleared[0];
            GameManager.Stage02Cleared = data.StageCleared[1];
            GameManager.Stage03Cleared = data.StageCleared[2];
        }

        FadeOut.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}