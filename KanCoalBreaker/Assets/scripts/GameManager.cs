using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
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
            timerText.text = "Time: " + gameTimer.ToString("F2");
        }
        else
        {
            Win();
        }
    }

    public void StartGame()
    { 
        gameActive = true;
        brickPatternGenerator.GenerateBricks();
    }

    public void Win()
    {
        Debug.Log("You Win!");
        gameActive = false;
        ballController.StopBall();
        saveScoreScript.ActivateEndGamePanel(true);
    }

    public void Lose()
    {
        Debug.Log("Game Over!");
        gameActive = false;
        ballController.StopBall();
        saveScoreScript.ActivateEndGamePanel(false);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Tital Bricks" + totalBricks);
        if(totalBricks == 0){

            brickPatternGenerator.GenerateBricks();
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("Score text component not set!");
        }
    }

    public void SetTotalBricks(int count)
    {
        Debug.Log("Total bricks: " + count);
        totalBricks = count;
        if (totalBricks == 0 && gameActive)
        {
            brickPatternGenerator.GenerateBricks();
        }
    }
}
