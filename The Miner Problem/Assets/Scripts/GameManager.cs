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

    void Start ()
    {
        horde = 0;
        score = 0;
    }

    public void handleGameOver ()
    {
        Debug.Log("Game over!");
        /* show something on screen... */
    }

    public void OnHordeEnd ()
    {
        horde++;
        subHordes *= 2;

        /* activate horde timer */
        /* ... */
    }
}
