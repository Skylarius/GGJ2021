﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private GameObject fragment;
    public GameObject memory;
    public AudioClip pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        fragment = gameObject;
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {            
            if(GameManager.inventory.Count < 3)
            {
                fragment.SetActive(false);
                GameManager.inventory.Enqueue(fragment);
                col.gameObject.transform.GetChild(0).GetChild(GameManager.inventory.Count - 1).gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                pickUpSound = gameObject.transform.parent.gameObject.GetComponent<PickUpSoundsManagement>().ReproducePickUpSound();
                AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);            
            }
        }
    }
    
}
