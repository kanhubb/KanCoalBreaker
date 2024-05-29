using System.Collections.Generic;
using UnityEngine;
using TMPro; // Pour utiliser TextMeshPro
using System.IO;
using System.Linq;

public class BestScores : MonoBehaviour
{
    public GameObject scorePanel; // Panneau pour afficher les scores
    public TMP_Text scoresText; // TextMeshPro Text où afficher les scores
    private const string ScoreFilePath = "highscores.csv"; // Même chemin de fichier que SaveScore

    void Start()
    {
        DisplayBestScores();
    }

    // Affiche les 5 meilleurs scores
    public void DisplayBestScores()
    {
        List<string> bestScores = GetBestScores();
        scoresText.text = string.Join("\n", bestScores); // Affiche les scores dans le TextMeshPro Text
    }

    // Récupère et trie les scores
    private List<string> GetBestScores()
    {
        string path = Path.Combine(Application.persistentDataPath, ScoreFilePath);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Score file not found!");
            return new List<string>() { "No scores yet" };
        }

        var scoreLines = File.ReadAllLines(path);
        return scoreLines.Select(line =>
        {
            var data = line.Split(',');
            return new { Name = data[0], Score = int.Parse(data[1]) };
        })
        .OrderByDescending(score => score.Score) // Trie les scores par ordre décroissant
        .Take(5) // Prend les 5 meilleurs scores
        .Select(score => $"{score.Name} - {score.Score}") // Formatte pour l'affichage
        .ToList();
    }
}
