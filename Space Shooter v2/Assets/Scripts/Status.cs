using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected string thisName;

    [SerializeField] protected float thisHP;
    protected float currentHP;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        currentHP = thisHP;
    }

    public virtual void GetDamage(float _damage)
    {
        if (!isDead)
        {
            currentHP -= _damage;
        }
    }
}