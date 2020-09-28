using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;

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
