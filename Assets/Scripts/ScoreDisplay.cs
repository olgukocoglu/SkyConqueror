using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;
    string currentSceneName;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        currentSceneName = FindObjectOfType<Level>().GetCurrentSceneName();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSceneName == "Game Over" || currentSceneName == "Congratulations")
        {
            scoreText.text = "SCORE: " + gameSession.GetScore().ToString();
            Debug.Log("SCORE: " + gameSession.GetScore().ToString());
        }
        else
        {
            scoreText.text = gameSession.GetScore().ToString();
        };
    }
}
