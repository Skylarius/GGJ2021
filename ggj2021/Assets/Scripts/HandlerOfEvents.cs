using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerOfEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, piano, myCamera, finalPointOfCamera;
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;

    public bool triggerGameOver = false;
    void Start()
    {   
        StartCoroutine("GameOverCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerGameOver) {
            GameManager.GameOver = true;
        }
    }

    IEnumerator GameOverCoroutine() {
        while (GameManager.GameOver == false) {
            yield return null;
        }
        if (GameManager.GameOver) {
            foreach (GameObject o in objectsToDisable) {
                if (o.activeSelf == true) {
                    o.SetActive(false);
                }
            }
            foreach (GameObject o in objectsToEnable) {
                if (o.activeSelf == false) {
                    o.SetActive(true);
                }
            }
        }
        while (Vector3.Distance(player.transform.position, piano.transform.position) > 3) {
            yield return null;
        }
        player.GetComponent<PlayerController>().blocked = true;
        myCamera.GetComponent<CameraController>().blocked = true;
        float i = 0f;
        while (Vector3.Distance(finalPointOfCamera.transform.position, myCamera.transform.position) > 1) {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, finalPointOfCamera.transform.position, i/1000f);
            i = i + 0.001f;
            yield return null;
        }
        Application.Quit();
        
    }
}
