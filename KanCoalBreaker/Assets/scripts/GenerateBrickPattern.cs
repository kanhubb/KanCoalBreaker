using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est conçu pour générer dynamiquement un motif de briques sur une grille.
public class GenerateBrickPattern : MonoBehaviour
{
    public GameObject brickPrefab; // Préfabriqué de la brique à utiliser pour la génération.
    public int numDepthRows = 10; // Nombre de rangées de briques.
    public int numWidthColumns = 10; // Nombre de colonnes de briques.
    public float brickSpacing = 0.1f; // Espacement entre les briques.

    // Fonction pour générer les briques.
    public void GenerateBricks()
    {
        // Détruit toutes les briques existantes dans ce conteneur avant de générer les nouvelles.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Calcule la taille de la brique à partir du préfabriqué.
        Vector3 brickSize = brickPrefab.GetComponent<Renderer>().bounds.size;
        // Détermine la largeur totale et la profondeur totale du motif des briques.
        float totalWidth = numWidthColumns * (brickSize.x + brickSpacing) - brickSpacing;
        float totalDepth = numDepthRows * (brickSize.z + brickSpacing) - brickSpacing;
        // Calcule le décalage pour centrer le motif de briques.
        Vector3 startOffset = new Vector3(-totalWidth / 2, 0, (-totalDepth / 2) + 3);

        int bricksCount = 0; // Compteur pour le nombre total de briques générées.

        // Boucle sur chaque position de la grille pour placer les briques.
        for (int z = 0; z < numDepthRows; z++)
        {
            for (int x = 0; x < numWidthColumns; x++)
            {
                // Condition pour ajouter une variation en ne plaçant une brique que la moitié du temps.
                if (Random.value > 0.2f)
                {
                    Vector3 position = new Vector3(
                        x * (brickSize.x + brickSpacing),
                        0,
                        z * (brickSize.z + brickSpacing)
                    ) + startOffset;
                    GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity, transform);
                    Brick brickComponent = newBrick.GetComponent<Brick>();  // Assure-toi que cette ligne est correcte.

                    // Décider aléatoirement si la brique est spéciale (bonus/malus)
                    if (Random.value > 0.9f)  // Plus rare que les briques normales
                    {
                        brickComponent.brickType = Random.value > 0.5f ? Brick.BrickType.Bonus : Brick.BrickType.Malus;
                        brickComponent.effect = brickComponent.ChooseEffect(brickComponent.brickType);
                        brickComponent.SetResistance(1); // Forcer la résistance à 1 pour les briques bonus
                        brickComponent.UpdateMaterial(); // Assure que le matériau est mis à jour immédiatement.
                    }
                    else
                    {
                        // Assignation aléatoire d'un niveau de résistance à chaque brique normale ou malus.
                        int resistanceLevel = Random.Range(1, 4);
                        brickComponent.SetResistance(resistanceLevel);
                    }

                    bricksCount++; // Incrémentation du compteur de briques.
                }
            }
        }
        // Mise à jour du nombre total de briques dans le gestionnaire de jeu.
        GameManager.Instance.SetTotalBricks(bricksCount);
    }
}
