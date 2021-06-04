using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

	public static GameManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion

    public int horde;
    public float score;
    public int subHordes = 1;
    public float intervalTime = 5f;
    public bool gameOver = false;
    public GameObject gameOverPanel;

    void Start ()
    {
        horde = 0;
        score = 0f;        

        Time.timeScale = 1;
        AudioManager.instance.PlayGameplaySound();
    }

    public void handleGameOver ()
    {
        if(gameOver)
            return;
        
        gameOver = true;
        Debug.Log("Game over! Activate GameOverPanel...");

        Time.timeScale = 0;
        gameOverPanel.SetActive (true);
        AudioManager.instance.PlayGameOverSound();
        CanvasManager.instance.UpdateHighscore(score);
    }

}
