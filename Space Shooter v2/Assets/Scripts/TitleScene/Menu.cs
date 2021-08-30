using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject FadeOut;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance > maxDistance)
        {
            GameManager.isMenuOpen = false;
        }
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
}