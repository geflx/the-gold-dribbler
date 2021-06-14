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

    private string lastFunctionCall;

    public bool intervalOn;
    public bool nextHordeTriggerOn;
    public bool hordeOn;
    public bool runScore; /* Same as hordeOn, but with additional 2 seconds after horde is over. */

    void Start()
    {
        horde = 0;
        hordeOn = false;
        runScore = false;

        /* First method execution: interval timer (game beginning). */
        nextHordeTriggerOn = false;
        intervalOn = true;
        lastFunctionCall = "nextHordeTrigger";
        IntervalTimer.instance.StartTimer();
    }

    void Update()
    {
        manageHorde();
    }


    private void manageHorde ()
    {   
        /* Game beginning interval... */
        if (intervalOn)
            return;

        /* Activate HordeTimer if last call was intervalTimer, and vice-versa.
            Save current method call as "last call" */
        if(!hordeOn && !nextHordeTriggerOn) {
            
            if(lastFunctionCall == "nextHordeTrigger") {
                horde++;
                CanvasManager.instance.ShowHorde();
                HordeTimer.instance.StartTimer();
            }
            else {
                CanvasManager.instance.HideHorde();
                NextHordeTrigger.instance.StartNextHordeTrigger();
            }

            lastFunctionCall = (lastFunctionCall == "nextHordeTrigger") ? "horde" : "nextHordeTrigger";
        }
    }
}
