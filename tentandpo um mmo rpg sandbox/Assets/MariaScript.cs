using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaScript : MonoBehaviour
{
    public int movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            var pos = transform.position;
            pos.z += 1 * movementSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.W))
        {
            var pos = transform.position;
            pos.z -= 1 * movementSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.A))
        {
            var pos = transform.position;
            pos.x += 1 * movementSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.D))
        {
            var pos = transform.position;
            pos.x -= 1 * movementSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
