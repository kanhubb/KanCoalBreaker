using UnityEngine;


public class Brick : MonoBehaviour
{
    public int resistance;  // Niveau de résistance actuel de la brique
    private int initialResistance;  // Niveau de résistance initial de la brique
    public int pointsPerResistance = 100;  // Points gagnés par niveau de résistance


    public void SetResistance(int level)
    {
        resistance = level;
        initialResistance = level;  // Stocker le niveau de résistance initial
        UpdateColor();
    }

    void UpdateColor()
    {
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))  // Assurez-vous que l'objet qui entre en collision est la balle
        {
            resistance--;
            UpdateColor();
            if (resistance <= 0)
            {
                DestroyBrick();
            }
        }
    }

    void DestroyBrick()
    {
        GameManager.Instance.totalBricks--; 
        GameManager.Instance.AddScore(initialResistance * pointsPerResistance); 
        Destroy(gameObject);

    }
}
