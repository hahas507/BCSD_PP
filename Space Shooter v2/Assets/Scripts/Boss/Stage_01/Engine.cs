using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Status
{
    private Warship theWarship;
    private float hpToPercentage;
    private float startSpeed;
    [SerializeField] private float engindDownDamage;
    private Engine theEngine;

    protected override void Awake()
    {
        base.Awake();
        theWarship = FindObjectOfType<Warship>();
    }

    private void Start()
    {
        startSpeed = theWarship.CURRENTSPEED;
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (!isDead)
        {
            theWarship.CURRENTSPEED = startSpeed * (hpToPercentage / 100);
            if (currentHP <= 0)
            {
                isDead = true;
                theWarship.GetDamage(engindDownDamage);
                theEngine.enabled = false;
            }
        }
    }

    private void Update()
    {
        hpToPercentage = ((currentHP / thisHP) * 100);
    }
}