using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPiece : MonoBehaviour
{
    public float health = 100, damage = 20, attackInterval = 1.5f, idleTime = 0f;
    public AttackType type = AttackType.normal;
    public bool isAttacking = false;
    public int posX, posY;


    public void applyDamage(float damage_){

        health -= damage_;

    }

}
public enum AttackType {

    normal = 1,
    ice

}