using UnityEngine;
using System.Collections.Generic;



public class Brick : MonoBehaviour
{
    public Material[] resistanceMaterials; // Matériaux pour chaque niveau de résistance
    public Material bonusMaterial; 
    public Material malusMaterial; 
    public Material BedrockMaterial;
    public enum BrickType { Normal, Bonus, Malus,Bedrock }
    public BrickType brickType; // Type de la brique (Normal, Bonus, Malus)
    public string effect;       // L'effet à appliquer si la brique est un bonus ou un malus
    public int resistance;      // Niveau de résistance actuel de la brique
    private int initialResistance;  // Niveau de résistance initial de la brique
    public int pointsPerResistance = 100;  // Points gagnés par niveau de résistance

    private static Dictionary<BrickType, List<string>> possibleEffects = new Dictionary<BrickType, List<string>>()
    {
        { BrickType.Bonus, new List<string> { "SpeedUp", "EnlargePaddle", "ExtraPoints","barSpeedUp" } },
        { BrickType.Malus, new List<string> { "SlowDown", "ShrinkPaddle", "ReducePoints","BarSpeedDown" } }
    };

    public void SetResistance(int level)
    {
        resistance = level;
        initialResistance = level;  // Stocker le niveau de résistance initial
        UpdateMaterial(); // Mise à jour du matériau selon le niveau de résistance
    }

    public void UpdateMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        // Vérifier le type de la brique et assigner le matériau correspondant
        if (brickType == BrickType.Bonus && bonusMaterial != null)
        {
            renderer.material = bonusMaterial; // Utiliser le matériau bonus pour les briques de type bonus
        }
        else if (brickType == BrickType.Malus && malusMaterial != null)
        {
            renderer.material = malusMaterial; // Utiliser le matériau malus pour les briques de type malus
        }
        else if (brickType == BrickType.Bedrock)
        {
            renderer.material = BedrockMaterial; // Utiliser le matériau de résistance la plus basse pour les briques de type Bedrock
        }
        else if (resistanceMaterials != null && resistance > 0 && resistance - 1 < resistanceMaterials.Length)
        {
            renderer.material = resistanceMaterials[resistance - 1]; // Sélectionner le matériau basé sur la résistance
        }
        else
        {
            // Gestion d'erreur : Aucun matériau n'est disponible
            Debug.LogError("No valid material found for the current brick settings.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))  // Vérifier si l'objet en collision est la balle
        {
            if (brickType == BrickType.Bedrock)
            {
                return; // Ne rien faire si la brique est de type Bedrock
            }
            resistance--;
       
            if (resistance <= 0)
            {
                ApplyEffects();  // Appliquer les effets avant de détruire la brique
                DestroyBrick();
            }
            else
            {
                UpdateMaterial(); // Mise à jour du matériau selon le niveau de résistance
            }
        }
    }
    void ApplyEffects()
    {
        if (brickType != BrickType.Normal && !string.IsNullOrEmpty(effect))
        {
            GameManager.Instance.ApplyEffect(effect,brickType); // gére l'affichage du bonus
            switch (effect)
            {
                case "SpeedUp":
                    FindObjectOfType<BallController>().IncreaseSpeed();
                    break;
                case "EnlargePaddle":
                    FindObjectOfType<Bar>().EnlargePaddle();
                    break;
                case "ExtraPoints":
                    GameManager.Instance.AddScore(500);
                    break;
                case "SlowDown":
                    FindObjectOfType<BallController>().DecreaseSpeed();
                    break;
                case "ShrinkPaddle":
                    FindObjectOfType<Bar>().ShrinkPaddle();
                    break;
                case "ReducePoints":
                    GameManager.Instance.AddScore(-250);
                    break;
                case "barSpeedUp":
                    FindObjectOfType<Bar>().speedUp();
                    break;
                case "BarSpeedDown":
                    FindObjectOfType<Bar>().slowDown();
                    break;
            }
        }
    }
    void DestroyBrick()
    {
        // Réduire le nombre total de briques et ajouter des points au score
        GameManager.Instance.totalBricks--;
        GameManager.Instance.AddScore(initialResistance * pointsPerResistance);
        Destroy(gameObject);
    }

    // Choix d'un effet basé sur le type de brique
    public string ChooseEffect(BrickType type)
    {
        if (possibleEffects.ContainsKey(type))
        {
            int index = Random.Range(0, possibleEffects[type].Count);
            return possibleEffects[type][index];
        }
        return "";
    }
}
