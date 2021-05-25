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

    private float[] durations = {3f};
    private float duration;

    public List<JewelFactory> factories;

    public void StartTimer()
    {
        HordeManager.instance.hordeOn = true;
        duration = durations[0];
    }

    void Update()
    {
        if(HordeManager.instance.hordeOn) {
            runClock();
            ShotJewels();

            if (duration < 0.0f)
                OnHordeIsOver();
        }
    }

    private void runClock ()
    {
        duration -= Time.deltaTime;
    }

    public void OnHordeIsOver()
    {
        Debug.Log("Horde is over...");
        HordeManager.instance.hordeOn = false;
    }

    public void ShotJewels()
    {
        foreach(JewelFactory jf in factories)
        {
            jf.GetNewDiamond();
        }
    }
}
