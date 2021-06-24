using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionManager : MonoBehaviour
{
    public float introductionTime;
    public GameObject skipButton;

    void Start ()
    {
        /* Player can skip introduction if he already had achieved some score */
        if (PlayerPrefs.GetFloat("timeHighscore", 0f) > 0.0f)
            skipButton.SetActive(true);
    }

    void Update ()
    {
        introductionTime -= Time.deltaTime;

        if (introductionTime < 0.0f)
            LoadMainMenu();
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

}
