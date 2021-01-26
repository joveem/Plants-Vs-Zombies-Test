using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float velocity = 1, damage = 20;

    public AttackType type = AttackType.normal; 

    void Update(){

        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        //transform.parent.

    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("zombie")){

            other.GetComponent<ZombiePiece>().applyDamage(damage, type);

            Destroy(gameObject);

        }
        
    }
}
