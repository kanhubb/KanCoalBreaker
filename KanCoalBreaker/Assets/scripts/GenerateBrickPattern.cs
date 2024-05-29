using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBrickPattern : MonoBehaviour
{
    public GameObject brickPrefab;
    public int numDepthRows = 10;
    public int numWidthColumns = 10;
    public float brickSpacing = 0.1f;

    public void GenerateBricks()
    {
        // Détruire les briques existantes avant de générer de nouvelles
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Vector3 brickSize = brickPrefab.GetComponent<Renderer>().bounds.size;
        float totalWidth = numWidthColumns * (brickSize.x + brickSpacing) - brickSpacing;
        float totalDepth = numDepthRows * (brickSize.z + brickSpacing) - brickSpacing;
        Vector3 startOffset = new Vector3(-totalWidth / 2, 0, (-totalDepth / 2) + 3);

        int bricksCount = 0;

        for (int z = 0; z < numDepthRows; z++)
        {
            for (int x = 0; x < numWidthColumns; x++)
            {
                if (Random.value > 0.5f)
                {
                    float offsetX = Random.Range(-0.1f, 0.1f);
                    float offsetZ = Random.Range(-0.1f, 0.1f);
                    Vector3 position = new Vector3(
                        x * (brickSize.x + brickSpacing) + offsetX,
                        0,
                        z * (brickSize.z + brickSpacing) + offsetZ
                    ) + startOffset;
                    GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity, transform);

                    int resistanceLevel = Random.Range(1, 4);
                    newBrick.GetComponent<Brick>().SetResistance(resistanceLevel);
                    bricksCount++;
                }
            }
        }
        GameManager.Instance.SetTotalBricks(bricksCount);
    }
}
