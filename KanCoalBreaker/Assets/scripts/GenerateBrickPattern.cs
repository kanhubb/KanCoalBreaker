using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBrickPattern : MonoBehaviour
{
    public GameObject brickPrefab; // Préfabriqué de la brique
    public int numDepthRows = 10; // Nombre de rangées en profondeur
    public int numWidthColumns = 10; // Nombre de colonnes en largeur
    public float brickSpacing = 0.1f; // Espacement entre les briques

    // Start is called before the first frame update
    void Start()
    {
        GenerateBricks();
    }
void GenerateBricks()
{
    Vector3 brickSize = brickPrefab.GetComponent<Renderer>().bounds.size;

    // Calculer le décalage pour centrer les briques
    float totalWidth = numWidthColumns * (brickSize.x + brickSpacing) - brickSpacing;
    float totalDepth = numDepthRows * (brickSize.z + brickSpacing) - brickSpacing;
    Vector3 startOffset = new Vector3(-totalWidth / 2, 0, (-totalDepth / 2)+3 );

    for (int z = 0; z < numDepthRows; z++)
    {
        for (int x = 0; x < numWidthColumns; x++)
        {
            if (Random.value > 0.5f) // 50% chance to place a brick
            {
                float offsetX = Random.Range(-0.1f, 0.1f); // Offset for more randomness in position
                float offsetZ = Random.Range(-0.1f, 0.1f);
                Vector3 position = new Vector3(
                    x * (brickSize.x + brickSpacing) + offsetX,
                    0,
                    z * (brickSize.z + brickSpacing) + offsetZ
                ) + startOffset;
                GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity);
                
                int resistanceLevel = Random.Range(1, 4); // Génère 1, 2 ou 3
                newBrick.GetComponent<Brick>().SetResistance(resistanceLevel);
            }
        }
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
