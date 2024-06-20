//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerScript : MonoBehaviour
//{
//    public Rigidbody2D myRigidbody;
//    public float movementSpeed = 15f;
//    public float reducedSpeed = 5f;
//    public float jumpForce = 10f;
//    public Animator animator;
//    public Transform groundCheck;
//    public LayerMask groundLayer;
//    public bool isFacingLeft = false;
//    public float jumpCooldown = 1f;

//    public bool isGrounded;
//    public bool jumpReady = true;
//    private float horizontalInput;

//    private void Start()
//    {
//        if (myRigidbody == null)
//        {
//            myRigidbody = GetComponent<Rigidbody2D>();
//        }
//        gameObject.name = "Olivia";

//        // Prevents model from rotating
//        myRigidbody.freezeRotation = true;
//    }

//    private void Update()
//    {
//        horizontalInput = Input.GetAxis("Horizontal");

//        FlipSprite(); // Flipping sprite for directional facing

//        // For Horizontal and Vertical Movement
//        if (Input.GetKey(KeyCode.Space) && isGrounded && jumpReady) // Space to jump
//        {
//            Jump();
//        }
//    }

//    private void FixedUpdate()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

//        myRigidbody.velocity = new Vector2(horizontalInput * movementSpeed, myRigidbody.velocity.y);
//        animator.SetFloat("FloatSpeed", Mathf.Abs(myRigidbody.velocity.x));
//        animator.SetBool("isGrounded", isGrounded);
//    }

//    private void Jump()
//    {
//        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
//        animator.SetTrigger("Jump");
//        jumpReady = false;
//        StartCoroutine(JumpCooldown());
//    }

//    private void FlipSprite()
//    {
//        if (isFacingLeft && horizontalInput > 0f || !isFacingLeft && horizontalInput < 0f)
//        {
//            isFacingLeft = !isFacingLeft;
//            Vector3 ls = transform.localScale;
//            ls.x *= -1f;
//            transform.localScale = ls;
//        }
//    }

//    private IEnumerator JumpCooldown()
//    {
//        yield return new WaitForSeconds(jumpCooldown);
//        jumpReady = true;
//    }
//}