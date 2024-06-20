using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour
{
    public GameObject footstep;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) && isGrounded)
        {
            FootSteps();
        } else if(Input.GetKey(KeyCode.LeftArrow) && isGrounded) 
        {
            FootSteps();
        } else
        {
            StopFootSteps();
        }
    }

    void FootSteps()
    {
        footstep.SetActive(true);
    }

    void StopFootSteps()
    {
        footstep.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       isGrounded= false;
    }
}
