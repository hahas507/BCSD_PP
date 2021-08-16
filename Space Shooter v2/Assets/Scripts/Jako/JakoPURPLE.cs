using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JakoPURPLE : JakoParent
{
    protected override void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(target.position);
        }
    }
}