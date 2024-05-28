using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float speed = 10.0f; // Vitesse de déplacement de la barre

    // Update is called once per frame
    void Update()
    {
        // Vérifie si la touche droite est pressée
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        // Vérifie si la touche gauche est pressée
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
    }
    void Start(){
        
    }

    void MoveRight()
    {
        transform.Translate( 0,-speed * Time.deltaTime,0); // le y est pas une erreur . le x est le y et le y est le x

        // Optionnel: Empêcher la barre de sortir de l'écran à droite
        var xPos = Mathf.Clamp(transform.position.x,-9.34f,9.34f);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    
    }

    void MoveLeft()
    {
        transform.Translate(0,speed * Time.deltaTime,0); // le y est pas une erreur . le x est le y et le y est le x

        // Optionnel: Empêcher la barre de sortir de l'écran à gauche
        var xPos = Mathf.Clamp(transform.position.x,-9.34f,9.34f);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}
