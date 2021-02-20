using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Sprite KeyboardSprite;
    public Sprite XBoxSprite;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0].ToLower().Contains("xbox")) {
            GetComponent<SpriteRenderer>().sprite = XBoxSprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = KeyboardSprite;
        }
    }

}
