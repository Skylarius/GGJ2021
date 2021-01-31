using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public List<GameObject> memories = new List<GameObject>();
    public GameObject interactionButton;
    private GameObject player;
    public AudioSource depositSound;
    public AudioClip[] depositSounds = new AudioClip[5];
    bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Start()
    {
        interactionButton.SetActive(false);
        GameObject Mems = gameObject.transform.GetChild(2).gameObject;

        for (int i = 0; i < 10; i++)
        {
            memories.Add(Mems.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNearby)
        {
            if(Input.GetKeyDown(KeyCode.E) && GameManager.inventory.Count > 0)
            {
                DepositOneFragment();
            }
        }
    }

    //Detect if player is nearby.
    //If true, it shows prompt to deposit fragments and enable deposit mech.
    //If enemy is nearby it remove one fragment from one memory.
    void OnCollisionEnter(Collision col)
    {
        player = col.gameObject;
        Debug.Log("Collision Happened!");
        if(col.gameObject.tag == "Player" && GameManager.inventory.Count > 0)
        {
            Debug.Log("It's a player!");
            interactionButton.SetActive(true);
            isPlayerNearby = true;
        }
    }

    //Remove prompt to deposit fragments and disable deposit mech.
    void OnCollisionExit(Collision col)
    {
        Debug.Log("Exited Collision!");
        if(col.gameObject.tag == "Player")
        {
            interactionButton.SetActive(false);
            isPlayerNearby = false;
        }
    }

    void DepositOneFragment()
    {
        int inventoryCount = GameManager.inventory.Count;

        RandomDepositSound();
        GameObject fragment = GameManager.inventory.Dequeue();
        GameObject memory = fragment.GetComponent<Fragment>().memory;
        player.transform.GetChild(0).GetChild(inventoryCount - 1).gameObject.GetComponent<SpriteRenderer>().sprite = null;
        RandomDepositSound();
        memory.GetComponent<Memory>().AddFragment(fragment);
    }

    void RandomDepositSound()
    {
        depositSound.clip = depositSounds[Random.Range(0, depositSounds.Length)];
        depositSound.Play();
    }

    /*void DepositAllFragments()
    {
        int inventoryCount = GameManager.inventory.Count;

        for(int i = 0; i < inventoryCount; i++)
        {
            GameObject fragment = GameManager.inventory.Dequeue();
            GameObject memory = fragment.GetComponent<Fragment>().memory;
            player.transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = null;
            memory.GetComponent<Memory>().AddFragment(fragment);
        }
    }*/

    public GameObject StealFragment()
    {
        foreach(GameObject memory in memories)
        {
            if (memory.GetComponent<Memory>().isComplete == false && memory.GetComponent<Memory>().fragments.Count > 0)
            {
                GameObject fragment = memory.GetComponent<Memory>().RemoveFragment();

                return fragment;
            }
        }
        return null;
    }
}
