using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerController thePlayer;
    private float maxHP;
    private float maxLazer;
    private int maxBooster;

    [SerializeField] private Image[] imageGauge;

    private const int HP = 0, BOOSTER = 1, LAZER = 2;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        maxHP = thePlayer.CURRENTHP;
        maxLazer = thePlayer.BEAMGAUGE;
        maxBooster = thePlayer.BOOSTERGAUGE;
    }

    private void Update()
    {
        imageGauge[HP].fillAmount = thePlayer.CURRENTHP / maxHP;
        imageGauge[BOOSTER].fillAmount = (float)thePlayer.BOOSTERGAUGE / maxBooster;
        imageGauge[LAZER].fillAmount = thePlayer.BEAMGAUGE / maxLazer;
    }
}