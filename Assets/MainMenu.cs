using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     void Start()
    {
        PlayerPrefs.SetInt("load", 0);
    }
    public void Playgame () // lancer la partie
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame() // reprendre la partie
    {
        if(PlayerPrefs.HasKey("x"))
        {
            PlayerPrefs.SetInt("load", 1);
            SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
        }
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
