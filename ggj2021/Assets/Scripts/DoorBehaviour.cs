using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int countOfMemoriesBeforOpening;
    public int degreesToRotate;
    public bool isOpen;
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.completedMemoriesCounter >= countOfMemoriesBeforOpening && isOpen == false) {
            StartCoroutine("OpenDoorCoroutine");
        }
    }

    IEnumerator OpenDoorCoroutine() {
        while (((transform.eulerAngles.y - degreesToRotate) < 1 && (transform.eulerAngles.y - degreesToRotate) > -1) == false){
            transform.eulerAngles = Vector3.Lerp(
                transform.eulerAngles, 
                Vector3.right * transform.eulerAngles.x + Vector3.up * degreesToRotate + Vector3.forward * transform.eulerAngles.z,
                Time.deltaTime * 0.001f);
            yield return null;
        }
        isOpen = true;
    }
}
