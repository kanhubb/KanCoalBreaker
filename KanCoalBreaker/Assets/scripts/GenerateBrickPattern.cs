using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBrickPattern : MonoBehaviour
{
    public GameObject brickPrefab; // Préfabriqué de la brique
    public int numRows = 5; // Nombre de rangées de briques
    public int numColumns = 10; // Nombre de colonnes de briques
    public float brickSpacing = 0.1f; // Espacement entre les briques

    // Start is called before the first frame update
    void Start()
    {
        GenerateBricks();
    }

    void GenerateBricks()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                Vector3 position = new Vector3(col * (brickPrefab.transform.localScale.x + brickSpacing),
                                               row * (brickPrefab.transform.localScale.y + brickSpacing),
                                               0);
                GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity);
                newBrick.transform.SetParent(transform);

                // Attribuer un niveau de résistance aléatoire à la brique
                int resistanceLevel = Random.Range(1, 4); // Génère 1, 2 ou 3
                newBrick.GetComponent<Brick>().SetResistance(resistanceLevel);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


