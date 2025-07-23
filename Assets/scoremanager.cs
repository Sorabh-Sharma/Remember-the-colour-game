using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    private int score = 0;
    private void Start()
    {
        score = 0;
        updateScoreText();
    }
    public void Addpoints()
    {
        score++;
        updateScoreText();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
    void updateScoreText()
    {
        scoreText.text = "Score : " + score;
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0);
    }
    public void ResetScore()
    {
        score = 0;
        updateScoreText();
    }
    public void SetScoreZero()
    {
        score = 0;
        updateScoreText();
    }
}
