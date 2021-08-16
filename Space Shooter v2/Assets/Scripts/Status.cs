using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected string thisName;

    [SerializeField] protected int thisHP;
    protected int currentHP;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        currentHP = thisHP;
    }

    public virtual void GetDamage(int _damage)
    {
        if (!isDead)
        {
            currentHP -= _damage;
        }
    }
}