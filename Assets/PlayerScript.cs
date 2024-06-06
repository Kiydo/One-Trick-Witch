using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movementSpeed = 15f;
    public float reducedSpeed = 5f;
    public float jumpForce = 10f;
    public Animator animator;
    public bool isGrounded;
    public bool isFacingLeft = false;
    public bool jumpReady = true;
    public bool jumpReadyCoolDown = false;

    float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
        gameObject.name = "Olivia";

        // Prevents model from rotating
        myRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite(); // Flipping sprite for directional facing

        Debug.Log(jumpReady);

        // For Horizontal and Vertical Movement
        if (Input.GetKey(KeyCode.Space) && isGrounded && jumpReady) // "C" to jump
        {
            PlayerMovement();
        } else if (!jumpReady && jumpReadyCoolDown == false) // If cooldown occured improperly
        {
            StartCoroutine(JumpCoolDown());
        }
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = new Vector2(horizontalInput * movementSpeed, myRigidbody.velocity.y);
        animator.SetFloat("FloatSpeed", Math.Abs(myRigidbody.velocity.x));
        animator.SetFloat("JumpSpeed", myRigidbody.velocity.y);
    }

    void PlayerMovement()
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        isGrounded = false;
        animator.SetBool("isJumping", !isGrounded);

        StartCoroutine(JumpCoolDown());
    }

    void FlipSprite()
    {
        if(isFacingLeft && horizontalInput > 0f || !isFacingLeft && horizontalInput < 0f )
        {
            isFacingLeft = !isFacingLeft;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded );
    }



    IEnumerator JumpCoolDown() // Cooldown for jumping to prevent jump spam, looks weird
    {
        Debug.Log("in enumerator, jump on cooldown");
        jumpReady = false;
        jumpReadyCoolDown = true;
        yield return new WaitForSeconds(1);
        jumpReady = true;
        jumpReadyCoolDown = false;
        Debug.Log("UwU");
    }
}