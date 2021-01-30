using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 1;
    public bool isPressingJump = false;
    public bool isJumping = false;
    public AudioClip[] audioClips;
    public Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationStateMachine();
        AudioStateMachine();
        Move();
        Jump();
    }

    void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += (Vector3.right * x + Vector3.forward * z) * speed * Time.deltaTime;
    }

    void Jump() {
        if (Input.GetButtonDown("Jump")) {    
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        isJumping = (GetComponent<Rigidbody>().velocity.y < -0.1 || GetComponent<Rigidbody>().velocity.y > 0.1);
    }

    void AnimationStateMachine() {
        animator.SetFloat("X_direction", Input.GetAxis("Horizontal"));
        animator.SetFloat("Z_direction", Input.GetAxis("Vertical"));
        animator.SetBool("isJumping", isJumping);
    }

    void AudioStateMachine() {
        if (!audioSource) {
            return;
        }
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") >0) {
            PlayAudioClip(0);
        }
        if (isJumping) {
           PlayAudioClip(1);
        }
    }

    private void PlayAudioClip(int index) {
        try {
            if (audioSource.clip != audioClips[index]) {
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
        } catch (Exception e) {
            
        }
    }
}
