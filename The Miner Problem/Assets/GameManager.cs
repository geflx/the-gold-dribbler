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
    public float[] hordeDuration = {20.0f, 15.0f};
    public float intervalTime = 5f;

    void Start ()
    {
        horde = 0;
    }

    public void handleGameOver ()
    {
        /* show something... */
        Debug.Log("Game over!");
    }
}
