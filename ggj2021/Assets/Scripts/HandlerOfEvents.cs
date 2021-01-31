using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerOfEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, piano, camera, finalPointOfCamera;
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
        while (Vector3.Distance(camera.transform.position, finalPointOfCamera.transform.position) > 1) {
            camera.transform.position = Vector3.Lerp(finalPointOfCamera.transform.position, camera.transform.position, 1);
        }
        Application.Quit();
        
    }
}
