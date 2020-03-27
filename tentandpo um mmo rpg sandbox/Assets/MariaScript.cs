using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaScript : MonoBehaviour
{
    public int movementSpeed = 5;

    private Rigidbody rb;
    private Animator anim;

    public bool frontSlash = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int xVel = (int)rb.velocity.x;
        int zVel = (int)rb.velocity.y;
        if (xVel == 0 && zVel == 0)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isRunningBack", false);
            anim.SetBool("isRunningBack", false);
            anim.SetBool("strafeLeft", false);
            anim.SetBool("strafeRight", false);
        }


        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        if(CanMove())
        {
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isRunningBack", true);
                var move = -1 * movementSpeed * Time.deltaTime;
                transform.Translate(new Vector3(0, 0, move));
            }
            else if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isRunning", true);
                var move = 1 * movementSpeed * Time.deltaTime;
                transform.Translate(new Vector3(0, 0, move));
            }



            if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("strafeLeft", true);
                }
                else
                {
                    anim.SetBool("strafeLeft", false);
                }

                var move = -1 * movementSpeed * Time.deltaTime;
                transform.Translate(new Vector3(move, 0, 0));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("strafeRight", true);
                }
                else
                {
                    anim.SetBool("strafeRight", false);
                }

                var move = 1 * movementSpeed * Time.deltaTime;
                transform.Translate(new Vector3(move, 0, 0));
            }
        }

        if(CanJump())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, 450, 0));
            }
        }


       




        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!frontSlash)
            {
                frontSlash = true;
                anim.SetBool("frontSlash", true);
            }
        }
    }


    bool CanMove()
    {
        return !frontSlash;
    }


    bool CanJump()
    {
        return !frontSlash;
    }
}
