using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaScript : MonoBehaviour
{
    public int movementSpeed = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        if(Input.GetKey(KeyCode.S))
        {
            var move = -1 * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, 0, move));
        }

        if (Input.GetKey(KeyCode.W))
        {
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
            rb.AddForce(new Vector3(0, 90, 0));
        }
    }
}
