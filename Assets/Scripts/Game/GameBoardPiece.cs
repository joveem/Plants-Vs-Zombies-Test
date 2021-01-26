using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardPiece : MonoBehaviour
{
    public bool isActive = false;
    public float alpha = 0, aplhaSensibility = 0.5f;
    public int posX, posY;
    public GameObject feedbackObject, floorObject;

    void Update()
    {

        if (!isActive)
        {
            alpha -= Time.deltaTime * aplhaSensibility;
        }

        alpha = Mathf.Clamp(alpha, 0, 1);

        Material feedbackMaterial = feedbackObject.GetComponent<MeshRenderer>().materials[0];

        feedbackMaterial.color = new Color(feedbackMaterial.color.r, feedbackMaterial.color.g, feedbackMaterial.color.b, alpha);

    }

    public void setColor(Color color_){

        feedbackObject.GetComponent<MeshRenderer>().materials[0].color = color_;

    }

    public void setActive(bool value_)
    {

        isActive = value_;

        if (value_)
        {

            alpha = 1;

        }

    }
}
