//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Cryptography.X509Certificates;
//using UnityEngine;

//public class PlayerAttackScript : MonoBehaviour
//{
//    // Attack Sound Effects
//    public GameObject chargeSound;
//    public GameObject laserSound;
//    public GameObject groundAttackSound;
//    public GameObject airAttackSound;

//    public Rigidbody2D myRigidbody;
//    public Animator animator;
//    public bool isCharging;
//    public bool attackReady = false;
//    public float chargeDuration = 3f;
//    public float chargeStartTime;
//    public bool isGrounded;
//    private PlayerMovement playerScript;

//    public float basicAttackCooldown = 0.2f;
//    public bool isBasicAttackOnCooldown = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        if (myRigidbody == null)
//        {
//            myRigidbody = GetComponent<Rigidbody2D>();
//        }

//        playerScript = GetComponent<PlayerMovement>();

//        chargeSound.SetActive(false);
//        laserSound.SetActive(false);
//        groundAttackSound.SetActive(false);
//        airAttackSound.SetActive(false);

//        // Prevents model from rotating
//        myRigidbody.freezeRotation = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        HandleCharging();
//        HandleAttack();
//    }
//    // For Charging Laser Attack
//    void HandleCharging()
//    {
//        if (Input.GetKeyDown(KeyCode.X) && isGrounded)
//        {
//            StartCharging();
//        }

//        if (isCharging)
//        {
//            float chargingTime = Time.time - chargeStartTime; // The timer for charging time
            
//            // If charging time is reached enables Laser Attack
//            if (chargingTime >= chargeDuration)
//            {
//                Debug.Log("Attack Ready");
//                attackReady = true;
//                animator.SetBool("AttackReady", true);
//                ResetCharging();
//            }
//            // If charging is cancled before charging time is reached
//            if (Input.GetKeyUp(KeyCode.X))
//            {
//                Debug.Log("Charging canceled");
//                ResetCharging();
//            }
//        }
//    }
//    // Handles which attack depending on User Input and scenario
//    void HandleAttack()
//    {
//        // Laser Attack if on the ground
//        if (attackReady && Input.GetKeyDown(KeyCode.Z) && isGrounded)
//        {
//            Debug.Log("Attacking");
//            StartCoroutine(BigAttack());
//        }
//        // Laser Attack if in the air
//        if (attackReady && Input.GetKeyDown(KeyCode.Z) && !isGrounded)
//        {
//            StartCoroutine(AirBigAttack());
//        }
//        // Basic Attack if on the Ground
//        if (Input.GetKey(KeyCode.C) && isGrounded && !isBasicAttackOnCooldown)
//        {
//            StartCoroutine(GroundBasic());
//            Debug.Log("ground attack");
//            //GroundBasic();
//        }
//        // Basic Attack if in the Air
//        if (Input.GetKey(KeyCode.C) && !isGrounded && !isBasicAttackOnCooldown)
//        {
//            StartCoroutine(AirBasic());
//            //AirBasic();
//        }
//    }
//    // Charging Laser Attack
//    void StartCharging()
//    {
//        if (!isCharging)
//        {
//            // For charge time, booleons and enables animation
//            isCharging = true;
//            chargeStartTime = Time.time;
//            animator.SetBool("ChargingAttack", true);

//            ChargeSound(); // Enables sound

//            playerScript.enabled = false; // prevents player from moving unless canceled
//            Debug.Log("Charging started");
//        }
//    }
//    // Big Laser Attack on Ground
//    IEnumerator BigAttack()
//    {
//        if (isGrounded)
//        {
//            LaserSound(); // Laser Audio

//            // Enable Booleons to proceed with animation
//            isBasicAttackOnCooldown = true;
//            attackReady = false;
//            animator.SetBool("AttackReady", false);
//            animator.SetBool("PlayerAttacking", true);
//            playerScript.enabled = false; // Prevents Player from moving during attack
           
//            yield return new WaitForSeconds(3.05f); // length of heavy attack, prevents interuption

//            // Reset sound and animations after completion
//            StartCoroutine(ResetSound());
//            animator.SetBool("PlayerAttacking", false);
//            playerScript.enabled = true;
//            isBasicAttackOnCooldown = false;


//        }
//    }
//    // Big Laser Attack on Air
//    IEnumerator AirBigAttack()
//    {
//        if (!isGrounded)
//        {
//            LaserSound();
//            isBasicAttackOnCooldown = true;
//            attackReady = false;
//            animator.SetBool("AttackReady", false);
//            animator.SetBool("PlayerAttacking", true);
//            Debug.Log("Air Attack executed");

//            // Stop falling down and movement
//            myRigidbody.velocity = Vector2.zero;

//            // Temporarily disable gravity
//            myRigidbody.gravityScale = 0;

//            // Determine the direction to push the character
//            float pushDirection = playerScript.isFacingLeft ? 1f : -1f;
//            myRigidbody.AddForce(new Vector2(pushDirection * 5f, 0), ForceMode2D.Impulse);

//            playerScript.enabled = false;

//            // Wait for the attack animation to finish
//            yield return new WaitForSeconds(3.05f);
//            StartCoroutine(ResetSound());


//            // Re-enable gravity
//            myRigidbody.gravityScale = 1;

//            animator.SetBool("PlayerAttacking", false);
//            playerScript.enabled = true;
//            isBasicAttackOnCooldown = false;
//        }
//    }
//    // Basic Attack on the Ground
//    IEnumerator GroundBasic()
//    {
//        groundAttackSound.SetActive(true);

//        playerScript.movementSpeed = playerScript.reducedSpeed;
//        isBasicAttackOnCooldown = true;
//        animator.SetTrigger("Attack");
//        animator.SetBool("BasicAttack", true);
//        yield return new WaitForSeconds(0.2f);
//        animator.SetBool("BasicAttack", false);
//        StartCoroutine(AttackCoolDown());
//        playerScript.movementSpeed = 15f;

//        StartCoroutine(ResetSound());
//    }

//    IEnumerator AirBasic()
//    {
//        airAttackSound.SetActive(true);

//        isBasicAttackOnCooldown = true;
//        animator.SetBool("BasicAttack", true);
//        yield return new WaitForSeconds(0.2f);
//        animator.SetBool("BasicAttack", false);
//        StartCoroutine(AttackCoolDown());

//        StartCoroutine(ResetSound());
//    }

//    void LaserSound()
//    {
//        laserSound.SetActive(true);
//    }

//    void ChargeSound()
//    {
//        chargeSound.SetActive(true);

//    }

//    IEnumerator ResetSound()
//    {
//        yield return new WaitForSeconds(0.2f);
//        chargeSound.SetActive(false);
//        laserSound.SetActive(false);
//        groundAttackSound.SetActive(false);
//        airAttackSound.SetActive(false);
//    }

//    IEnumerator AttackCoolDown()
//    {
//        yield return new WaitForSeconds(0.2f);
//        isBasicAttackOnCooldown = false;
//    }

//    void ResetCharging()
//    {
//        chargeSound.SetActive(false);
//        isCharging = false;
//        animator.SetBool("ChargingAttack", false);
//        Debug.Log("Charging reset");
//        playerScript.enabled = true;
        
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        isGrounded = true;
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        isGrounded = false;
//    }
//}
