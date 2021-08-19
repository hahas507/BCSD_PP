using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private PlayerController thePlayer;
    private float maxHP;
    private float maxLazer;
    private int maxBooster;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        maxHP = thePlayer.CURRENTHP;
        maxLazer = thePlayer.BEAMGAUGE;
        maxBooster = thePlayer.BOOSTERGAUGE;
    }

    private void Update()
    {
        Debug.Log(thePlayer.CURRENTHP);
    }
}