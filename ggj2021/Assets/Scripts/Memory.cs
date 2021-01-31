using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    public Stack<GameObject> fragments = new Stack<GameObject>();
    public bool isComplete = false;
    public Dialogue dialogue;
    public GameObject messageBackground;
    private Text dialogueText;
    private GameObject dialogueBox;

    public static bool gameIsPaused = false;

    //Method to add fragment to memory when deposited at cabinet
    public void AddFragment(GameObject fragment)
    {
        fragments.Push(fragment);

        Sprite sprite = fragment.GetComponent<SpriteRenderer>().sprite;

        if(fragments.Count == 1)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else if(fragments.Count == 2)
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else if(fragments.Count == 3)
        {
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sprite;
            isComplete = true;
            GameManager.completedMemoriesCounter += 1;
            CompleteMemory();
        }
    }

    //Method to remove single fragment to memory when stole by enemy at cabinet
    public GameObject RemoveFragment()
    {
        GameObject removedFragment = fragments.Peek();
        gameObject.transform.GetChild(fragments.Count - 1).GetComponent<SpriteRenderer>().sprite = null;
        fragments.Pop();

        return removedFragment;
    }

    public void CompleteMemory()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        //messageBackground.GetComponent<Image>().enabled = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue.sentence;
    }

    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueText = GameObject.Find("DialogueBox").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(gameIsPaused)
            {
                //messageBackground.GetComponent<Image>().enabled = false;
                dialogueBox.SetActive(false);
                gameIsPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
