using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    List<Memory> Memories = new List<Memory>();
    public GameObject interactionButton;
    bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Start()
    {
        interactionButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNearby)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                DepositAllFragments();
            }
        }
    }

    //Detect if player is nearby.
    //If true, it shows prompt to deposit fragments and enable deposit mech.
    //If enemy is nearby it remove one fragment from one memory.
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Happened!");
        if(col.gameObject.tag == "Player" && GameManager.inventory.Count > 0)
        {
            Debug.Log("It's a player!");
            interactionButton.SetActive(true);
            isPlayerNearby = true;
        }
        else if(col.gameObject.tag == "thief")
        {
            //StealFragment();
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

    void DepositAllFragments()
    {
        int inventoryCount = GameManager.inventory.Count;

        for(int i = 0; i < inventoryCount; i++)
        {
            GameObject fragment = GameManager.inventory.Dequeue();
            GameObject memory = fragment.GetComponent<Fragment>().memory;
            memory.GetComponent<Memory>().AddFragment(fragment);
        }
    }

    /*public Fragment StealFragment()
    {

    }*/
}
