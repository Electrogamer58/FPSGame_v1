﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _dieMenu;
    public PlayerMovementScript player = null;

    int _currentScore;
    string _highScore = "HighScore";
    public bool inMenu = false;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        //INCREASE SCORE//
        //TODO replace with real implementation; this is for test purposes
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        //EXIT LEVEL//
        //bring up navigation pop-up menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
        }

        PauseMenu();
        DieMenu();

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

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log(_pauseMenu.activeSelf);
        inMenu = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log(_pauseMenu.activeSelf);
        inMenu = true;
    }


    public void PauseMenu()
    {
        if (inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        } else if (!inMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void DieMenu()
    {
        if (player.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _dieMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Player Died");
        } else if (!player.isDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _dieMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display so new score is shown
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

}
