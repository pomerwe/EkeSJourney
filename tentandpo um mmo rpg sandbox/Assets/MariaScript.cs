using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaScript : MonoBehaviour
{
    public int movementSpeed = 5;

    public GameObject hips;
    private Rigidbody rb;
    private Animator anim;
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
        }

        hips.transform.localPosition = new Vector3(-3.537999e-09f, 1.052462f, 0.01765147f);

        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        if(Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isRunningBack", true);
            var move = -1 * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, 0, move));
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isRunning", true);
            var move = 1 * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, 0, move));
        }

        

        if (Input.GetKey(KeyCode.A))
        {
            var move = -1 * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(move, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            var move = 1 * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(move, 0, 0));
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0,500, 0));
        }
    }
}
