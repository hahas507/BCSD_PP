using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTeacher : MonoBehaviour
{
    [Header("Start of Tutorial")]
    [SerializeField] private GameObject AttackTutorialIndicator;

    [SerializeField] private GameObject followArrow;

    private void Awake()
    {
        BattleSceneManager.isMainTargetDefeated = false;
    }

    private void Update()
    {
        FollowArrow();
    }

    private void FollowArrow()
    {
        if (followArrow.activeSelf)
        {
            followArrow.transform.position = AttackTutorialIndicator.transform.position - (Vector3.forward * 17);
            if (!AttackTutorialIndicator.activeSelf)
            {
                followArrow.SetActive(false);
            }
        }
        else
        {
            return;
        }
    }
}