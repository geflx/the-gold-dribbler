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
    private string difficulty;

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
        else if (horde <= 15) {
            shotsInterval = 2.0f;
            difficulty = "insane";
        }
        else {
            shotsInterval = 1.0f;
            difficulty = "impossible";
        }
    }

    public void OnHordeIsOver()
    {
        HordeManager.instance.hordeOn = false;
    }

    private IEnumerator coroutineShot (float time, int bullets)
    {
        yield return new WaitForSeconds(time);

        if (difficulty == "easy")                EasyShots(bullets);   
        else if (difficulty == "medium")         MediumShots(bullets);
        else if (difficulty == "hard")           HardShots(bullets);
        else if (difficulty == "insane")         InsaneShots(bullets);
        else if (difficulty == "impossible")     ImpossibleShots(bullets);
    }

    public void SetupShots()
    {
        IEnumerator coroutine;
        int bullets = 0;

        for (float dur = 0.0f; dur < chosenDuration; dur += shotsInterval) {
            coroutine = coroutineShot(dur, bullets);
            StartCoroutine (coroutine);
            ++bullets;
        }
    }

    private void knuthShuffle(int[] numbers)
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
        
        knuthShuffle(randomNumbers);

        return randomNumbers;
    }

    public void EasyShots (int bullets)
    {
        
    }

    public void MediumShots (int bullets)
    {
        
    }

    public void HardShots (int bullets)
    {

    }

    public void InsaneShots (int bullets)
    {

    }

    public void ImpossibleShots (int bullets)
    {

    }
}
