using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioClip playSound;
    public AudioClip quitSound;

    public void PlayGame()
    {
        clickSound.clip = playSound;
        clickSound.Play();

        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        clickSound.clip = quitSound;
        clickSound.Play();

        Application.Quit();
    }
}
