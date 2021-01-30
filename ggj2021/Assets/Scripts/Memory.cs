using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    public int id;
    public Queue<GameObject> fragments = new Queue<GameObject>();
    public bool isComplete = false;
    public Dialogue dialogue;
    public Text dialogueText;
    public GameObject dialogueBox;

    public static bool gameIsPaused = false;

    //Method to add fragment to memory when deposited at cabinet
    public void AddFragment(GameObject fragment)
    {
        fragments.Enqueue(fragment);

        if(fragments.Count == 3)
        {
            isComplete = true;
            CompleteMemory();
        }
    }

    //Method to remove single fragment to memory when stole by enemy at cabinet
    public GameObject RemoveFragment()
    {
        GameObject removedFragment = fragments.Dequeue();

        return removedFragment;
    }

    public void CompleteMemory()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue.sentence;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(gameIsPaused)
            {
                dialogueBox.SetActive(false);
                gameIsPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
