using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioClip playSound;
    public AudioClip quitSound;
    public Image blackFadingScreenImage;

    public void PlayGame()
    {
        StartCoroutine("PlayGameCoroutine");
    }

    IEnumerator PlayGameCoroutine() {
        blackFadingScreenImage.gameObject.SetActive(true);
        clickSound.clip = playSound;
        clickSound.Play();
        Color color = blackFadingScreenImage.color;

        while (blackFadingScreenImage.color.a < 1) {
            color.a += 0.005f;
            blackFadingScreenImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void QuitGame()
    {
        clickSound.clip = quitSound;
        clickSound.Play();

        Application.Quit();
    }
}
