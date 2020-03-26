using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject maria;
    Vector3 offset;
    float cameraDistance = 2f;
    float mouseSensibility = 2.45f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    bool mouseLocked = true;

    Vector2 lastMousePos = Vector2.zero;

    void Start()
    {
        offset = maria.transform.position - transform.position;
    }
    void LateUpdate()
    {

        pitch -= mouseSensibility/20 * Input.GetAxis("Mouse Y");
        if(pitch > 3.5)
        {
            pitch = 3.5f;
        }
        if(pitch < -3.5)
        {
            pitch = -3.5f;
        }


        float desiredAngle = maria.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        var pos = maria.transform.position - (rotation * offset/cameraDistance);
        pos.y = pos.y + 1;
        pos.y += pitch;
        //pos.x += -1;
        transform.position = pos;
        var lookMaria = maria.transform.position;
        //lookMaria.x -= 0.5f;
        lookMaria.y +=  1;
        transform.LookAt(lookMaria);

        Debug.Log(mouseSensibility);
    }


        // Update is called once per frame
    void Update()
    {
        if(mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        yaw += mouseSensibility * Input.GetAxis("Mouse X");
        
        maria.transform.eulerAngles = new Vector3(0, yaw, 0.0f);

        //SystemUtil.SetCursorPos(x, y);

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            if(cameraDistance  < 4)
            {
                cameraDistance += 0.2f;
            }
            else
            {
                cameraDistance = 4;
            }
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            if(cameraDistance > 1)
            {
                cameraDistance -= 0.2f;
            }
            else
            {
                cameraDistance = 1;
            }
        }

        if (Input.GetKey(KeyCode.Minus))
        {
            if (mouseSensibility > 1)
            {
                mouseSensibility -= 0.02f;
            }
            else
            {
                mouseSensibility = 1;
            }
        }
        if (Input.GetKey(KeyCode.Equals))
        {
            if (mouseSensibility < 10)
            {
                mouseSensibility += 0.02f;
            }
            else
            {
                mouseSensibility = 10;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            mouseLocked = false;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            mouseLocked = true;
        }
    }
}
