﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Queue<GameObject> inventory = new Queue<GameObject>(3);
    public static Queue<GameObject> fragments = new Queue<GameObject>(3);

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
        
    }

}
