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
    private bool hasJumped = false;
    public AudioClip[] audioClips;
    public Animator animator;
    private AudioSource audioSource;
    private Rigidbody rb;
    public SpriteRenderer mainSpriteRenderer;

    public bool blocked = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine("StateMachine");
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (blocked ==  false) {
            Move();
        }
    }

    void Update()
    {
        if (blocked ==  false) {
            Jump();
        }
    }

    IEnumerator StateMachine() {
        while (true)
        {
            while (Time.deltaTime == 0) {
                yield return null;
            }
            AnimationStateMachine();
            AudioStateMachine();
            yield return null;
        }
    }

    void Move() {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        rb.MovePosition(transform.position + Vector3.right * x + Vector3.forward * z);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump")) {    
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }
        }

        if (isJumping) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.2f))
            {
                if (hit.transform.tag == "Thief" && invulnerable == false) {
                    hit.transform.gameObject.SendMessage("Die");
                }
            }
        }
        isJumping = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1f) == false;
    }

    void AnimationStateMachine() {
        animator.SetFloat("X_direction", Input.GetAxis("Horizontal"));
        animator.SetFloat("Z_direction", Input.GetAxis("Vertical"));
        animator.SetBool("X_greater_than_Z", Input.GetAxis("Horizontal") * Input.GetAxis("Horizontal") > Input.GetAxis("Vertical") * Input.GetAxis("Vertical"));
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
        rb.AddForce(direction * 6, ForceMode.Impulse);
        StartCoroutine("Invulnerable");
    }

    IEnumerator Invulnerable() {
        invulnerable = true;
        Color color = mainSpriteRenderer.color;
        float time = Time.time;
        float deltaAlpha = -0.05f;
        WaitForSeconds wts = new WaitForSeconds(0.02f);
        while(Time.time < time + 3) {
            color.a += deltaAlpha;
            mainSpriteRenderer.color = color;
            deltaAlpha = (color.a < 0.33f || color.a > 0.99f) ? -deltaAlpha : deltaAlpha;
            yield return wts;
        }
        while (mainSpriteRenderer.color.a < 1) {
            color.a += 0.1f;
            mainSpriteRenderer.color = color;
            yield return wts;
        }
        color.a = 1;
        mainSpriteRenderer.color = color;
        invulnerable = false;
    }
}
