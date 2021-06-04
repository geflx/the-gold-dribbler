using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionManager : MonoBehaviour
{
    public float introductionTime;

    void Update ()
    {
        introductionTime -= Time.deltaTime;

        if (introductionTime < 0.0f)
            LoadMainMenu();
    }

    private void LoadMainMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

}
