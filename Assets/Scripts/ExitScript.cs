using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [SerializeField] GameObject exitText;
    [SerializeField] Level01Controller lvl01Controller;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] int scoreNecessary;

    bool inVolume;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            inVolume = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inVolume = false;
        }
    }

    private void Update()
    {
        if (inVolume)
        {
            exitText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
                {
                    if (lvl01Controller._currentScore >= scoreNecessary)
                    {
                        lvl01Controller.IncreaseScore(-scoreNecessary);
                        Debug.Log("GAME OVER!");
                        AudioHelper.PlayClip2D(victorySound, 3);
                    }

                    else if (lvl01Controller._currentScore < scoreNecessary)
                    {
                        Debug.Log("Not enough score");
                        AudioHelper.PlayClip2D(errorSound, 3);
                    }
                }
        }

        if (!inVolume)
        {
            exitText.SetActive(false);
        }
    }
}
