using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalTimer : MonoBehaviour
{
    #region Singleton
	public static IntervalTimer instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    private float duration;
    public float defaultDuration = 6.0f;

    public void StartTimer()
    {
        duration = defaultDuration;
        HordeManager.instance.intervalOn = true;
    }

    void Update()
    {
        if(HordeManager.instance.intervalOn) {
            runClock();

            if (duration < 0.0f)
                OnTimeIsUp();
        }
    }

    private void runClock ()
    {
        duration -= Time.deltaTime;
    }

    private void OnTimeIsUp ()
    {
        HordeManager.instance.intervalOn = false;
    }
}
