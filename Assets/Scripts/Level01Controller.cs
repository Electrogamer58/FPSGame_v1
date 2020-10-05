using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _pauseMenu;

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
        //TODO bring up navigation pop-up menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ExitLevel();
            if (inMenu)
            {
                Resume();
            }
            else if (!inMenu)
            {
                Pause();
            }
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

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pauseMenu.SetActive(false);
        inMenu = false;
        Time.timeScale = 1;
        Debug.Log(_pauseMenu.activeSelf);
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pauseMenu.SetActive(true);
        inMenu = true;
        Time.timeScale = 0;
        Debug.Log(_pauseMenu.activeSelf);
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display so new score is shown
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

}
