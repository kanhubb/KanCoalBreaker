using UnityEngine;
using System.Collections;
using System.Collections.Generic;  

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI BonusText,startInstructionText,timerText,scoreText;
    public GameObject ball;
    public SaveScore saveScoreScript; 
    public GenerateBrickPattern brickPatternGenerator;

    private BallController ballController;
    public float gameTimer = 60.0f; 
    private bool gameActive = false;

    private int score = 0;
    public int totalBricks = 0; 
    
    void Start()
    {
        StartGame(); 
    }

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

    void Update()
    {
        if (gameActive)
        {
            UpdateGameTimer();
        }
    }

    private void UpdateGameTimer()
    {
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            timerText.text =  gameTimer.ToString("F2");
        }
        else
        {
            Win();
        }
    }

    public void StartGame()
    { 
        brickPatternGenerator.GenerateBricks();
    }
    public void SetGameActive(bool active)
    {
        gameActive = active;
    }
    public bool getGameActive()
    {
        return gameActive;
    }

    public void Win()
    {
        Debug.Log("You Win!");
        gameActive = false;
        ballController.StopBall();
        BonusText.gameObject.SetActive(false);
        saveScoreScript.ActivateEndGamePanel(true);
    }

    public void Lose()
    {
        Debug.Log("Game Over!");
        gameActive = false;
        ballController.ResetBall();
        BonusText.gameObject.SetActive(false);
        saveScoreScript.ActivateEndGamePanel(false);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int points)
    {
        score += points;
        if(totalBricks == 0){
            startInstructionText.gameObject.SetActive(true);
            ballController.ResetBall();
            brickPatternGenerator.GenerateBricks();
        }
        UpdateScoreText();
    }
    public void ApplyEffect(string effect, Brick.BrickType type)
    {
        if (BonusText != null)
        {
            BonusText.text = effect;

            // Détermine la couleur du texte en fonction du type de la brique
            switch (type)
            {
                case Brick.BrickType.Bonus:
                    BonusText.color = Color.green;  // Vert pour les bonus
                    break;
                case Brick.BrickType.Malus:
                    BonusText.color = Color.red;    // Rouge pour les malus
                    break;
                default:
                    BonusText.color = Color.white;  // Blanc pour les types normaux ou non spécifiés
                    break;
            }

            BonusText.gameObject.SetActive(true);
            StartCoroutine(HideEffectDisplayAfterTime(2));
        }
    }
    IEnumerator HideEffectDisplayAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (BonusText != null)
        {
            BonusText.gameObject.SetActive(false);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = ""+score;
        }
        else
        {
            Debug.LogError("Score text component not set!");
        }
    }

    public void SetTotalBricks(int count)
    {
        totalBricks = count;
        if (totalBricks == 0 && gameActive)
        {
            brickPatternGenerator.GenerateBricks();
        }
    }
    public void HideStartInstruction()
{
    if (startInstructionText.gameObject.activeSelf)
    {
        startInstructionText.gameObject.SetActive(false);
    }
}

}
