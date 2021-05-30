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
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    public TMP_Text hordeLabel;
    public TMP_Text scoreLabel;

    void FixedUpdate () 
    {
        scoreLabel.text = (GameManager.instance.score).ToString("0.0");
    }

    public void ShowHorde ()
    {
        hordeLabel.text = "Horde " + HordeManager.instance.horde;
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
}
