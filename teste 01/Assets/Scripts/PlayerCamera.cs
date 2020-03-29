using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;


    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;
    private float cameraSpeed = 0.15f;
    private float urgencySpeed = 8f;
    public bool urgencyCamera = false;

    //Mathf.Clamp(player.transform.position.x,xMin,xMax)
    public int currentY;
    public int currentX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Abs(transform.position.y - player.transform.position.y) > 2)
        {
            currentY = (int)player.transform.position.y + 2;
            if(Math.Abs(transform.position.y - player.transform.position.y) > 8.5)
            {
                urgencyCamera = true;
            }
            else
            {
                urgencyCamera = false;
            }
        }

        if (Math.Abs(transform.position.x - player.transform.position.x) > 1)
        {
            currentX = (int)player.transform.position.x;
        }

        var pos = transform.position;

        //if (currentX > (int)pos.x)
        //{
        //    pos.x += cameraSpeed;
        //}
        //else if (currentX < (int)pos.x)
        //{
        //    pos.x -= cameraSpeed;
        //}

        pos.x = player.transform.position.x + 1;
        if (currentY > (int)pos.y)
        {
            pos.y += !urgencyCamera ? cameraSpeed : cameraSpeed * urgencySpeed;
        }
        else if (currentY < (int)pos.y)
        {
            pos.y -= !urgencyCamera ? cameraSpeed : cameraSpeed * urgencySpeed;
        }
        transform.position = pos;
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    public void CameraReset()
    {
        var pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        transform.position = pos;
    }
}
