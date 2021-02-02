using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Queue<GameObject> inventory = new Queue<GameObject>(3);
    public static int completedMemoriesCounter = 0;

    public static bool GameOver = false; 

    void AddFragmentToInventory(GameObject f)
    {
        inventory.Enqueue(f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (completedMemoriesCounter == 10)
        {
            Debug.Log("Triggering Game Over from Manager!");
            Camera.main.GetComponent<AudioSource>().Stop();
            HandlerOfEvents.triggerGameOver = true;
        }
    }

    

}
