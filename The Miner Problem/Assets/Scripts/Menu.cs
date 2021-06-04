using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator commandsAnim;
    public GameObject commandsPanel;

    public Animator creditsAnim;
    public GameObject creditsPanel;

    void Start ()
    {
        /* Activate Panels */
        commandsPanel.SetActive(true);
        creditsPanel.SetActive(true);
    }

    public void LoadGame () {
        SceneManager.LoadScene("Arena");
    }

    public void ShowCommands (bool show)
    {
        commandsAnim.SetBool("isActive", show);
    }

    public void ShowCredits (bool show)
    {
        creditsAnim.SetBool("isActive", show);
    }
}
