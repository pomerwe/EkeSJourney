using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject maria;
    Vector3 offset;

    void Start()
    {
        offset = maria.transform.position - transform.position;
    }
    void LateUpdate()
    {
        float desiredAngle = maria.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        var pos = maria.transform.position - (rotation * offset);
        pos.y = pos.y + 10;
        transform.position = pos;
        transform.LookAt(maria.transform);
    }


        // Update is called once per frame
        void Update()
    {
        
    }
}
