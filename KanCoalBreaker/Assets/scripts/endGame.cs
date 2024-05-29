using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class SaveScore : MonoBehaviour
{
    public GameObject endGamePanel;
    public TMP_InputField playerNameInput;
    public Button saveButton;
    public GameObject additionalButtonsContainer; // Conteneur pour les boutons Best Scores, Restart, et Back to Menu

    private const string ScoreFilePath = "highscores.csv"; // Chemin du fichier pour simplifier la gestion

    void Start()
    {
        additionalButtonsContainer.SetActive(false); // S'assurer que les boutons supplémentaires sont désactivés au démarrage
        endGamePanel.SetActive(false);
    }

    public void ActivateEndGamePanel(bool won)
    {
        endGamePanel.SetActive(true); // Activer le panneau de fin de jeu
        saveButton.interactable = true; // Réactiver le bouton de sauvegarde si nécessaire
        endGamePanel.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = won ? "You Win!" : "Game Over!";
    }

    public void SaveScoreToFile()
    {
        string playerName = string.IsNullOrWhiteSpace(playerNameInput.text) ? "Anonymous" : playerNameInput.text;
        int score = GameManager.Instance.GetScore();
        string newScoreEntry = $"{playerName},{score}\n";
        AppendScoreToFile(newScoreEntry);
        PostSaveActions();
    }

    private void AppendScoreToFile(string newScoreEntry)
    {
        string path = Path.Combine(Application.persistentDataPath, ScoreFilePath);
        Debug.Log($"Saving score to {path}");
        File.AppendAllText(path, newScoreEntry);
    }

    private void PostSaveActions()
    {
        saveButton.interactable = false;
        additionalButtonsContainer.SetActive(true);
    }
}
