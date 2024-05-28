using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
        public void BtnBestScores()
    {
        SceneManager.LoadScene("BestScoreScene");
    }
    public void BtnQuit()
    {
        Application.Quit();
    }
}
