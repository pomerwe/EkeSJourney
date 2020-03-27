using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformScript : MonoBehaviour
{
    Vector2 startPosition;

    public Rigidbody2D rig;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null )
        {
            rig.isKinematic = false;
            Invoke("RemoveRigidbody", 5);
        }
        if(collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }


    void RemoveRigidbody()
    {
        transform.eulerAngles = Vector3.zero;
        rig.velocity = Vector2.zero;
        rig = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        rig.isKinematic = true;
        rig.angularVelocity = 0;
    }
}
