using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSoundsManagement : MonoBehaviour
{
    public AudioClip[] pickUpQueue = new AudioClip[14];
    public int counter = 0;

    public AudioClip ReproducePickUpSound()
    {
        if(counter < 13)
        {
            counter++;
            return pickUpQueue[counter];
        }
        else
        {
            counter = 0;
            return pickUpQueue[13];
        }
    }
}
