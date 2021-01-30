using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 1;
    public bool isPressingJump = false;
    public bool isJumping = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimationStateMachine();
        Move();
        Jump();
    }

    void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += (Vector3.right * x + Vector3.forward * z) * Time.deltaTime;
    }

    void Jump() {
        if (Input.GetAxis("Jump") > 0 && isPressingJump == false) {    
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.2f))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isPressingJump = true;
            }
        }
        if (Input.GetAxis("Jump") == 0) {
            isPressingJump = false;
        }
        isJumping = (GetComponent<Rigidbody>().velocity.y < -1 || GetComponent<Rigidbody>().velocity.y > 1);
    }

    void AnimationStateMachine() {
        animator.SetFloat("X_direction", Input.GetAxis("Horizontal"));
        animator.SetFloat("Z_direction", Input.GetAxis("Vertical"));
        animator.SetBool("isJumping", isJumping);
    }
}
