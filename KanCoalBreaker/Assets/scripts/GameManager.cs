using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Instance Singleton pour accéder globalement à GameManager
    public static GameManager Instance;

    // Références aux éléments textuels de l'interface utilisateur
    public TextMeshProUGUI BonusText, startInstructionText, timerText, scoreText;

    // Référence au GameObject de la balle
    public GameObject ball;

    // Scripts pour la sauvegarde des scores et la génération des motifs de briques
    public SaveScore saveScoreScript;
    public GenerateBrickPattern brickPatternGenerator;

    // Contrôleur de la balle pour gérer ses interactions
    private BallController ballController;

    // Chronomètre pour le temps de jeu, initialisé à 60 secondes
    public float gameTimer = 60.0f;

    // Indicateur pour savoir si le jeu est actif
    private bool gameActive = false;

    // Score actuel du joueur
    private int score = 0;

    // Nombre total de briques à détruire
    public int totalBricks = 0;

    // Initialise le jeu au démarrage
    void Start()
    {
        StartGame();
    }

    // Crée une instance unique de GameManager ou détruit l'instance supplémentaire
    void Awake()
    {
        ballController = ball.GetComponent<BallController>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Met à jour le jeu à chaque image, en particulier le compteur de temps
    void Update()
    {
        if (gameActive)
        {
            UpdateGameTimer();
        }
    }

    // Met à jour le compteur de temps et détermine si le jeu doit se terminer
    private void UpdateGameTimer()
    {
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = gameTimer.ToString("F2");
        }
        else
        {
            Win();
        }
    }

    // Démarre le jeu en générant des briques
    public void StartGame()
    {
        brickPatternGenerator.GenerateBricks();
    }

    // Active ou désactive l'état de jeu
    public void SetGameActive(bool active)
    {
        gameActive = active;
    }

    // Retourne si le jeu est actuellement actif
    public bool getGameActive()
    {
        return gameActive;
    }

    // Traite la condition de victoire
    public void Win()
    {
        Debug.Log("You Win!");
        gameActive = false;
        ballController.StopBall();
        BonusText.gameObject.SetActive(false);
        saveScoreScript.ActivateEndGamePanel(true);
    }

    // Traite la condition de défaite
    public void Lose()
    {
        Debug.Log("Game Over!");
        gameActive = false;
        ballController.StopBall();
        BonusText.gameObject.SetActive(false);
        saveScoreScript.ActivateEndGamePanel(false);
    }

    // Renvoie le score actuel
    public int GetScore()
    {
        return score;
    }

    // Ajoute des points au score et vérifie si de nouvelles briques doivent être générées
    public void AddScore(int points)
    {
        score += points;
        if (totalBricks == 0)
        {
            startInstructionText.gameObject.SetActive(true);
            ballController.ResetBall();
            brickPatternGenerator.GenerateBricks();
        }
        UpdateScoreText();
    }

    // Applique un effet visuel et textuel en fonction du type de brique touchée
    public void ApplyEffect(string effect, Brick.BrickType type)
    {
        if (BonusText != null)
        {
            BonusText.text = effect;
            switch (type)
            {
                case Brick.BrickType.Bonus:
                    BonusText.color = Color.green;
                    break;
                case Brick.BrickType.Malus:
                    BonusText.color = Color.red;
                    break;
                default:
                    BonusText.color = Color.white;
                    break;
            }
            BonusText.gameObject.SetActive(true);
            StartCoroutine(HideEffectDisplayAfterTime(2));
        }
    }

    // Coroutine pour masquer l'affichage des effets après un délai
    IEnumerator HideEffectDisplayAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (BonusText != null)
        {
            BonusText.gameObject.SetActive(false);
        }
    }

    // Met à jour l'affichage du score
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
        else
        {
            Debug.LogError("Le composant texte du score n'est pas défini!");
        }
    }

    // Définit le nombre total de briques
    public void SetTotalBricks(int count)
    {
        totalBricks = count;
        if (totalBricks == 0 && gameActive)
        {
            brickPatternGenerator.GenerateBricks();
        }
    }

    // Masque les instructions de démarrage si elles sont affichées
    public void HideStartInstruction()
    {
        if (startInstructionText.gameObject.activeSelf)
        {
            startInstructionText.gameObject.SetActive(false);
        }
    }
}
