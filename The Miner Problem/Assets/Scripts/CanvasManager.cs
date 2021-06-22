using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CanvasManager : MonoBehaviour
{
    #region Singleton
	public static CanvasManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of CanvasManager found!");
			return;
		}
		instance = this;
	}
	#endregion

    public TMP_Text hordeLabel;
    public TMP_Text scoreLabel;
    public TMP_Text hordeHighscore;
    public TMP_Text timeHighscore;

    void FixedUpdate () 
    {
        scoreLabel.text = (GameManager.instance.score).ToString("0.0");
    }

    public void ShowHorde ()
    {
        hordeLabel.text = LanguageTranslation.fields["Level"] + " " + HordeManager.instance.horde;
    }

    public void HideHorde ()
    {
        hordeLabel.text = "";
    }

    public void PlayAgain ()
    {
        SceneManager.LoadScene("Arena");
    }

    public void GoToMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update highscore if current score is greater than highscore
    public void UpdateHighscore (float currScore)
    {
        int currHorde = HordeManager.instance.horde;

        float maxTime = PlayerPrefs.GetFloat("timeHighscore", 0f);
        int maxHorde = PlayerPrefs.GetInt("hordeHighscore", 1);

        if(maxTime < currScore) {
            PlayerPrefs.SetFloat("timeHighscore", currScore);
            maxTime = currScore;
        }

        if(maxHorde < currHorde) {
            PlayerPrefs.SetInt("hordeHighscore", currHorde);
            maxHorde = currHorde;
        }

        hordeHighscore.text = maxHorde.ToString();
        timeHighscore.text = maxTime.ToString("0.0");
    }
}
