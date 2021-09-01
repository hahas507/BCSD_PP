using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTeacher : MonoBehaviour
{
    [Header("Start of Tutorial")]
    [SerializeField] private GameObject AttackTutorialIndicator;

    [SerializeField] private GameObject followArrow;
    [SerializeField] private GameObject pressMenu;

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
        if (AttackTutorialIndicator != null)
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
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tutorial_Attack")
        {
            Destroy(AttackTutorialIndicator);
            pressMenu.SetActive(true);
        }
    }
}