using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePiece : MonoBehaviour
{

    public int id;
    public float health = 100, damage = 20, velocity = 1, velocityMultiplier = 1, attackRange = 0.5f, attackInterval = 1.5f, idleTime = 0f, effectDuration = 0;
    public bool isAttacking = false;

    public void applyDamage(float damage_, AttackType attackType_)
    {

        health -= damage_;

        if (attackType_ == AttackType.ice)
        {

            effectDuration = 5;
            velocityMultiplier = 0.5f;

        }

    }

    private void Update()
    {

        if (effectDuration > 0)
        {

            effectDuration -= Time.deltaTime;
            effectDuration = Mathf.Clamp(effectDuration, 0, 5);

        }

        if(effectDuration == 0){

            velocityMultiplier = 1;

        }

    }


}
