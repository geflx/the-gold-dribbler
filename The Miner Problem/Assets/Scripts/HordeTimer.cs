using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeTimer : MonoBehaviour
{
    #region Singleton
	public static HordeTimer instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

    private float[] durations = {12f, 16f, 25f};
    private float currDuration, chosenDuration;
    private float shotsInterval;

    public List<JewelFactory> factories;

    public void StartTimer()
    {
        HordeManager.instance.hordeOn = true;
        chosenDuration = currDuration = durations[0];

        SetupHordeParameters();
        SetupShots();
    }

    void Update()
    {
        if(HordeManager.instance.hordeOn) {
            runClock();

            if (currDuration < 0.0f)
                OnHordeIsOver();
        }
    }

    private void runClock ()
    {
        currDuration -= Time.deltaTime;
    }

    private void SetupHordeParameters ()
    {
        int horde = HordeManager.instance.horde;
        
        if (horde <= 3)         shotsInterval = 6.0f;
        else if (horde <= 7)    shotsInterval = 5.0f;
        else if (horde <= 11)   shotsInterval = 3.0f;
        else if (horde <= 15)   shotsInterval = 2.0f;
        else                    shotsInterval = 1.0f;
    }

    public void OnHordeIsOver()
    {
        HordeManager.instance.hordeOn = false;
    }

    private IEnumerator coroutineShot (float time)
    {
        yield return new WaitForSeconds(time);
        ShotAllJewels();
    }

    public void SetupShots()
    {
        IEnumerator coroutine;

        for (float dur = 0.0f; dur < chosenDuration; dur += shotsInterval) {
            coroutine = coroutineShot(dur);
            StartCoroutine (coroutine);
        }
    }

    public void ShotAllJewels()
    {
        foreach(JewelFactory jf in factories)
        {
            jf.GetNewDiamond();
        }
    }
}
