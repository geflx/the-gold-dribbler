using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    #region Singleton
	public static HordeManager instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    public int horde;
    public bool hordeOn, intervalOn;
    private string lastRun;

    void Start()
    {
        horde = 1;
        hordeOn = false;

        /* First method execution will the interval timer (game's beginning). */
        intervalOn = true;
        lastRun = "interval";
        IntervalTimer.instance.StartTimer();
    }

    void Update()
    {
        handleFunctionCall();
    }

    private void handleFunctionCall ()
    {
        /* Activate HordeTimer if last call was intervalTimer, and vice-versa.
            Save current method call as "last call" */
        if(!hordeOn && !intervalOn) {
            
            if(lastRun == "interval")
                HordeTimer.instance.StartTimer();
            else
                IntervalTimer.instance.StartTimer();

            lastRun = (lastRun == "interval") ? "horde" : "interval";
        }
    }
}
