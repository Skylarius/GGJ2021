using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandlerOfEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, piano, myCamera, finalPointOfCamera;
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;
    public GameObject[] roomToDisable;
    public GameObject[] otherThingsToEnable;

    public static bool triggerGameOver = false;
    void Start()
    {   
        StartCoroutine("GameOverCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11)) {
            GameManager.GameOver = true;
            triggerGameOver = true;
        }
        if (triggerGameOver) {
            GameManager.GameOver = true;
        }
        if (GameManager.GameOver == true) {
            triggerGameOver = true;
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
        foreach (GameObject o in roomToDisable) {
            if (o.activeSelf == true) {
                o.SetActive(false);
            }
        }
        foreach (GameObject o in otherThingsToEnable) {
            if (o.activeSelf == false) {
                o.SetActive(true);
            }
        }
        float i = 0f;
        while (Vector3.Distance(finalPointOfCamera.transform.position, myCamera.transform.position) > 1) {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, finalPointOfCamera.transform.position, i*Time.deltaTime/1000f);
            i += 0.01f;
            yield return null;
        }
        print("SUCA");
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
}
