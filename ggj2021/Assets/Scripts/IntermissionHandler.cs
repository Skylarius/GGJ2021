using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermissionHandler : MonoBehaviour
{
    public int secondsToNextScene = 65;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GoToNextSceneAfterSeconds");
    }

    // Update is called once per frame
    IEnumerator GoToNextSceneAfterSeconds() {
        yield return new WaitForSeconds(secondsToNextScene);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
