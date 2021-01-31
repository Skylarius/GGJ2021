using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private GameObject fragment;
    public GameObject memory;

    // Start is called before the first frame update
    void Start()
    {
        fragment = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {            
            if(GameManager.inventory.Count < 3)
            {
                GameManager.inventory.Enqueue(fragment);
                col.gameObject.transform.GetChild(0).GetChild(GameManager.inventory.Count - 1).gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                fragment.SetActive(false);
            }
        }
    }
    
}
