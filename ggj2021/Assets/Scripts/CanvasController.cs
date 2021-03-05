using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject PauseObj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PauseMenuCoroutine");
    }

    IEnumerator PauseMenuCoroutine() {
        while (GameManager.GameOver == false) {
            if (Input.GetButtonDown("Pause")) {
                if (Time.timeScale == 0 && isPaused == false) {
                    yield return null;
                } else if (isPaused) {
                    Time.timeScale = 1;
                    isPaused = false;
                    PauseObj.SetActive(false);
                } else {
                    Time.timeScale = 0;
                    isPaused = true;
                    PauseObj.SetActive(true);
                }
            }
            yield return null;
        }
    }
}
