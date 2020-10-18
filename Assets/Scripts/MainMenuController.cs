using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;
    string _highScore = "HighScore";

    //Start is called before the first frame update
    void Start()
    {
        //load high score display
        int highScore = PlayerPrefs.GetInt(_highScore);
        _highScoreTextView.text = highScore.ToString();

        //play starting song
        if(_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
            AudioManager.Instance.isPlaying = true;
        }
    }

    public void resetData(bool buttonPressed)
    {
        if (buttonPressed)
        {
            PlayerPrefs.SetInt(_highScore, 0);
            int highScore = PlayerPrefs.GetInt(_highScore);
            _highScoreTextView.text = highScore.ToString();
        }
    }

    public void quit(bool buttonPressed)
    {
        if (buttonPressed)
        {
            Application.Quit();
            Debug.Log("User has left the game");
        }
    }
}
