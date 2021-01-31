using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 1;
    public bool isPressingJump = false;
    public bool isJumping = false;
    public bool invulnerable = false;
    private bool hasJumped = false, hasKilledEnemy = false;
    public AudioClip[] audioClips;
    public Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("StateMachine");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    IEnumerator StateMachine() {
        while (true)
        {
            AnimationStateMachine();
            AudioStateMachine();
            yield return null;
        }
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
                hasJumped = true;
            }
        }
        isJumping = (GetComponent<Rigidbody>().velocity.y < -0.1 || GetComponent<Rigidbody>().velocity.y > 0.1);

        if (isJumping) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.2f))
            {
                if (hit.transform.tag == "Thief") {
                    hit.transform.gameObject.SendMessage("Die");
                }
            }
        }
    }

    void AnimationStateMachine() {
        animator.SetFloat("X_direction", Input.GetAxis("Horizontal"));
        animator.SetFloat("Z_direction", Input.GetAxis("Vertical"));
        animator.SetBool("isJumping", isJumping);
    }

    void AudioStateMachine() {
        if (hasJumped) {
            PlayAudioClip(Random.Range(1,5), stopPrevious: true);
            hasJumped = false;
            return;
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            PlayAudioClip(0);
            return;
        }
        else {
            StopAudioClip(0);
        }
    }

    private void PlayAudioClip(int index, bool stopPrevious = false) {
        try {
            if (!stopPrevious && audioSource.isPlaying) {
                return;
            }
            if (audioSource.clip != audioClips[index]) {
                if (audioSource.clip) {
                    audioSource.Stop();
                }
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
        } catch (System.Exception e) {

        }
    }

    private void StopAudioClip(int index) {
        try {
            if (audioSource.clip == audioClips[index]) {
                audioSource.Stop();
                audioSource.clip = null;
            }
        } catch (System.Exception e) {

        }
    }

    void BeHit(Vector3 direction) {
        if (invulnerable) {
            return;
        }
        print("BEEN HIT!");
        GetComponent<Rigidbody>().AddForce(direction * 3 * Time.deltaTime, ForceMode.Impulse);
        StartCoroutine("Invulnerable");
    }

    IEnumerator Invulnerable() {
        invulnerable = true;
        yield return new WaitForSeconds(3f);
        invulnerable = false;
    }
}
