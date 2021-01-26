using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaltZombie : ZombiePiece
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!isAttacking)
        {

            transform.Translate(-velocity * velocityMultiplier * Time.deltaTime, 0, 0);

        }

        idleTime += Time.deltaTime * velocityMultiplier;

        idleTime = Mathf.Clamp(idleTime, 0, attackInterval);


        Ray ray_ = new Ray(transform.position + new Vector3(1, 0.5f, 0), Vector3.left);

        Debug.DrawLine(transform.position + new Vector3(1, 0.5f, 0), transform.position + new Vector3(-attackRange, 0.5f, 0), Color.red);

        if (Physics.Raycast(ray_, out RaycastHit hit_, attackRange + 1, LayerMask.GetMask("plants")))
        {

            Debug.DrawLine(transform.position + new Vector3(1, 0.5f, 0), hit_.point, Color.green);

            isAttacking = true;

            if(idleTime == attackInterval){

                Debug.Log("- ZOMBIE ATTACK!");

                idleTime = 0;

                hit_.transform.GetComponent<PlantPiece>().applyDamage(damage);

            }

        }else{

            isAttacking = false;

        }

    }
}
