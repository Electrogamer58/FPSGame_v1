﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;
    string _highScore = "HighScore";

    private void Update()
    {
        //INCREASE SCORE//
        //TODO replace with real implementation; this is for test purposes
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        //EXIT LEVEL//
        //TODO bring up navigation pop-up menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
    }

    public void ExitLevel()
    {
        //compare current score with saved high score
        int highScore = PlayerPrefs.GetInt(_highScore);
        if (_currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt(_highScore, _currentScore);
            Debug.Log("New High Score: " + _currentScore);
        }

        //load new level, or in this case, the main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display so new score is shown
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

}
