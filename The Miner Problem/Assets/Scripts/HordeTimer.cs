using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeTimer : MonoBehaviour
{
    #region Singleton
	public static HordeTimer instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of HordeTimer found!");
			return;
		}
		instance = this;
	}
	#endregion

    private float[] durations = {14f, 16f, 25f};
    private float currDuration, chosenDuration;
    private float shotsInterval;
    private string difficulty;

    public List<JewelFactory> factories;

    public void StartTimer()
    {
        HordeManager.instance.hordeOn = true;
        HordeManager.instance.runScore = true;
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

        if (horde <= 3) {
            shotsInterval = 6.0f;
            difficulty = "easy";
        }
        else if (horde <= 7) {
            shotsInterval = 5.0f;
            difficulty = "medium";
        }
        else if (horde <= 11) {
            shotsInterval = 3.0f;
            difficulty = "hard";
        }
        else {
            shotsInterval = 2.0f;
            difficulty = "insane";
        }
    }

    private IEnumerator disableScore (float delay)
    {
        yield return new WaitForSeconds(delay);
        HordeManager.instance.runScore = false;
    }

    public void OnHordeIsOver()
    {
        IEnumerator coroutine;
        coroutine = disableScore (4.0f);
        StartCoroutine(coroutine);
        
        HordeManager.instance.hordeOn = false;
    }

    private IEnumerator coroutineShot (float time, int bullets)
    {
        yield return new WaitForSeconds(time);

        bullets = Mathf.Min(bullets, factories.Count);

        if (difficulty == "easy")                triggerEasyShots(bullets);   
        else if (difficulty == "medium")         triggerNormalShots(bullets);
        else if (difficulty == "hard")           triggerHardShots(bullets);
        else                                     triggerInsaneShots(bullets);
    }

    public void SetupShots()
    {
        IEnumerator coroutine;
        int bullets = 2;

        for (float dur = 0.0f; dur < chosenDuration; dur += shotsInterval) {
            coroutine = coroutineShot(dur, bullets++);
            StartCoroutine (coroutine);
        }
    }

    private void shuffleArray(int[] numbers)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < numbers.Length; t++ )
        {
            int tmp = numbers[t];
            int r = Random.Range(t, numbers.Length);
            numbers[t] = numbers[r];
            numbers[r] = tmp;
        }
    }

    private int[] getRandomFactories ()
    {
        int[] randomNumbers = new int [factories.Count];

        for (int i = 0; i < factories.Count; i++) 
            randomNumbers[i] = i;
        
        shuffleArray(randomNumbers);

        return randomNumbers;
    }

    private void triggerEasyShots (int bullets)
    {
        // Reducing minimal number of bullets to 1.
        bullets--;
        
        int[] order = getRandomFactories();
        for (int i = 0; i < bullets; i++) {
            factories[order[i]].GetNewJewel(1);
        }
    }

    private void triggerNormalShots (int bullets)
    {
        int[] order = getRandomFactories();
        for (int i = 0; i < bullets; i++) {
            factories[order[i]].GetNewJewel(2);
        }
    }

    private void triggerHardShots (int bullets)
    {
        int[] order = getRandomFactories();
        for (int i = 0; i < bullets; i++) {
            factories[order[i]].GetNewJewel(3);
        }
    }

    private void triggerInsaneShots (int bullets)
    {
        int[] order = getRandomFactories();
        for (int i = 0; i < bullets; i++) {
            factories[order[i]].GetNewJewel(4);
        }
    }
}
