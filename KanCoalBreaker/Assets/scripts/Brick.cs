using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour
{

    public int resistance;
    public void SetResistance(int level)
    {
        resistance = level;
        // Modifier la couleur ou d'autres propriétés en fonction de la résistance
        switch (resistance)
        {
            case 1:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 3:
                GetComponent<Renderer>().material.color = Color.red;
                break;
        }
    }
}