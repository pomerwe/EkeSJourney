using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Left,Right,Up,Down
}

public class Player : MonoBehaviour
{
    public int score = 0;

    public float speed = 4;
    public float jumpForce = 8;
    public float dashLength = 5;
    public float dashSpeed = 15;

    public bool isjumping = false;
    public bool doublejump = false;
    public bool isDashing = false;
    
    public Direction dashDirection = Direction.Left;
    public Vector2 dashPosition = Vector2.zero;

    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody2D rig;
    private CapsuleCollider2D capsCollider;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Dash();
    }


    void Dash()
    {
        
        if (isDashing)
        {
            ResetYForce();
            var dashDir = dashDirection == Direction.Right ? Vector2.right : Vector2.left;
            rig.MovePosition(rig.position + dashDir * dashSpeed * 2 * Time.deltaTime);

            if(dashDirection == Direction.Right)
            {
                if(transform.position.x > dashPosition.x)
                {
                    isDashing = false;
                }
            }
            else
            {
                if (transform.position.x < dashPosition.x)
                {
                    isDashing = false;
                }
            }

            
        }



        if (isjumping)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!doublejump)
                {
                    isDashing = true;
                    //cancelaDoublejump
                    doublejump = true;
                    dashPosition = new Vector2(transform.position.x - 4, transform.position.y);
                    dashDirection = Direction.Left;
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!doublejump)
                {
                    isDashing = true;

                    //cancelaDoublejump
                    doublejump = true;
                    dashPosition = new Vector2(transform.position.x + 4, transform.position.y);
                    dashDirection = Direction.Right;
                }
            }
        }
        
        
        
    }

    void Move()
    {
        Vector2 movement = rig.velocity;
        movement.x = Input.GetAxis("Horizontal") * speed;
        rig.velocity = movement;
       

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            ResetXForce();
            rig.velocity = movement;
            anim.SetBool("walk", false);
        }



    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!isjumping)
            {
                ResetYForce();
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isjumping = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(!doublejump)
                {
                    ResetYForce();
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doublejump = true; 
                    anim.SetBool("double", true);
                }

            }
            
        }
    }


    private bool IsGrounded()
    {
        float extraHeighText = 0.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsCollider.bounds.center, Vector2.down, capsCollider.bounds.extents.y + extraHeighText, groundLayer);
        Color rayColor = Color.green;
        if (raycastHit.collider != null)
        {
           return true;
        }
        return false;
    }

    private bool IsAttachedToWall()
    {
        float extraHeighText = 0.01f;
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(capsCollider.bounds.center, Vector2.left, capsCollider.bounds.extents.y + extraHeighText, groundLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(capsCollider.bounds.center, Vector2.right, capsCollider.bounds.extents.y + extraHeighText, groundLayer);
        Color rayColor = Color.green;
        if (raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
           return true;
        }
        return false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(IsAttachedToWall())
        {
            isDashing = false;
        }
        if (IsGrounded())
        {
            isDashing = false;
            isjumping = false;
            doublejump = false;
            anim.SetBool("jump", false);
            anim.SetBool("double", false);
            ResetYForce();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnTriggerEnter2D(Collider2D  coll)
    {
        if (coll.gameObject.GetComponent<GenericItem>() != null)
        {
            coll.gameObject.GetComponent<Animator>().SetBool("Touched", true);
            score += coll.gameObject.GetComponent<GenericItem>().value;
        }
    }


    public void ResetYForce()
    {
        var velocity = rig.velocity;
        velocity.y = 0;
        rig.velocity = velocity;
    }

    public void ResetXForce()
    {
        var velocity = rig.velocity;
        velocity.x = 0;
        rig.velocity = velocity;
    }

    public void ResetForces()
    {
        ResetXForce();
        ResetYForce();
    }

}