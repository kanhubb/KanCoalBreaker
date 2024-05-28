using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton pour accéder facilement à l'instance de GameManager depuis d'autres scripts

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Assurer que le GameManager persiste entre les changements de scène
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        
        score += points;
        Debug.Log("Score: " + score);
    }
}
