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
    public bool hordeOn, intervalOn, nextHordeTriggerOn;
    private string lastRun;
    public bool runScore; /* Same as hordeOn, but with additional 2 seconds after horde is over. */

    void Start()
    {
        horde = 0;
        hordeOn = false;
        runScore = false;

        /* First method execution will the interval timer (game's beginning). */
        nextHordeTriggerOn = false;
        intervalOn = true;
        lastRun = "nextHordeTrigger";
        IntervalTimer.instance.StartTimer();
    }

    void Update()
    {
        handleFunctionCall();
    }

    private void handleFunctionCall ()
    {   
        /* Game beginning interval... */
        if (intervalOn)
            return;

        /* Activate HordeTimer if last call was intervalTimer, and vice-versa.
            Save current method call as "last call" */
        if(!hordeOn && !nextHordeTriggerOn) {
            
            if(lastRun == "nextHordeTrigger") {
                horde++;
                CanvasManager.instance.ShowHorde();
                HordeTimer.instance.StartTimer();
            }
            else {
                CanvasManager.instance.HideHorde();
                NextHordeTrigger.instance.StartNextHordeTrigger();
            }

            lastRun = (lastRun == "nextHordeTrigger") ? "horde" : "nextHordeTrigger";
        }
    }
}
