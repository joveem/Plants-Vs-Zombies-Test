using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : PlantPiece
{
    public GameObject projectilePivot;

    void Update()
    {
        
        if(health <= 0){

            Destroy(gameObject);

        }

    }
}
