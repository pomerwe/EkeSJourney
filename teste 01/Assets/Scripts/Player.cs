using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool isDoubleJumping = false;
    public bool isDashing = false;

    public bool isFlipped = false;

    public bool isWallJumping = false;
    public bool attachedToWall = false;

    private float extraHeightGround = 0.02f;
    private float extraWidthWall = 0.025f;

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


        Debug.DrawRay(capsCollider.bounds.center, Vector2.down * (capsCollider.bounds.extents.y + extraHeightGround));
        Debug.DrawRay(capsCollider.bounds.center, Vector2.left * (capsCollider.bounds.extents.x + extraWidthWall));
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
                if (!isDoubleJumping)
                {
                    isDashing = true;
                    //cancelaDoublejump
                    isDoubleJumping = true;
                    dashPosition = new Vector2(transform.position.x - 4, transform.position.y);
                    dashDirection = Direction.Left;
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isDoubleJumping)
                {
                    isDashing = true;

                    //cancelaDoublejump
                    isDoubleJumping = true;
                    dashPosition = new Vector2(transform.position.x + 4, transform.position.y);
                    dashDirection = Direction.Right;
                }
            }
        }
        
        
        
    }

    void Move()
    {

        if(CanMove())
        {
            Vector2 movement = rig.velocity;
            movement.x = Input.GetAxis("Horizontal") * speed;
            rig.velocity = movement;


            if (Input.GetAxis("Horizontal") > 0f)
            {
                anim.SetBool("walk", true);
                Flip(Direction.Right);
            }
            if (Input.GetAxis("Horizontal") < 0f)
            {
                anim.SetBool("walk", true);
                Flip(Direction.Left);
            }
            if (Input.GetAxis("Horizontal") == 0f)
            {
                ResetXForce();
                rig.velocity = movement;
                anim.SetBool("walk", false);
            }
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!isjumping)
            {
                DoJump();
                isjumping = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(!isDoubleJumping)
                {
                    DoJump();
                    isDoubleJumping = true; 
                    anim.SetBool("double", true);
                }

            }
            
        }
    }

    private void DoJump()
    {
        //Quando for wallJump
        if (IsAttachedToWall() && !IsGrounded())
        {
            WallJump();
        }
        //Quando não tiver no wall Jump
        else
        {
            NormalJump();
        }
    }

    public void NormalJump()
    {
        ResetYForce();
        rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    public void WallJump()
    {
        ResetForces();
        isWallJumping = true;
        var xForce = !isFlipped ? -jumpForce : jumpForce;
        rig.AddForce(new Vector2(xForce * 0.62f, jumpForce), ForceMode2D.Impulse);

        anim.SetBool("wallJump", false);
        if (attachedToWall)
        {
            isDoubleJumping = true;
            attachedToWall = false;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastGround = (Physics2D.Raycast(capsCollider.bounds.center, Vector2.down, capsCollider.bounds.extents.y + extraHeightGround, groundLayer));
        if (raycastGround.collider != null)
        {
           return true;
        }
        return false;
    }

    private bool IsAttachedToWall()
    {
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(capsCollider.bounds.center, Vector2.left, capsCollider.bounds.extents.x + extraWidthWall, groundLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(capsCollider.bounds.center, Vector2.right, capsCollider.bounds.extents.x + extraWidthWall, groundLayer);

        if (raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
           return true;
        }
        return false;
    }

    private void WallFlipChecker()
    {
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(capsCollider.bounds.center, Vector2.left, capsCollider.bounds.extents.x + extraWidthWall, groundLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(capsCollider.bounds.center, Vector2.right, capsCollider.bounds.extents.x + extraWidthWall, groundLayer);

        if (raycastHitLeft.collider != null)
        { 
            if (raycastHitLeft.collider.gameObject.layer == 8)
            {
                Flip(Direction.Left);
            }
        }

        if (raycastHitRight.collider != null)
        {
            if (raycastHitRight.collider.gameObject.layer == 8)
            {
                Flip(Direction.Right);
            }
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        WallFlipChecker();

        WallJumpHandling();

        GroundedHandling();
    }

    void WallJumpHandling()
    {
        if (IsAttachedToWall())
        {
            isDashing = false;
            isDoubleJumping = false;
            attachedToWall = true;
            isjumping = true;

            anim.SetBool("wallJump", true);
            anim.SetBool("jump", true);
        }
    }

    void GroundedHandling()
    {
        if (IsGrounded())
        {
            isDashing = false;
            isjumping = false;
            isDoubleJumping = false;
            isWallJumping = false;
            attachedToWall = false;
            anim.SetBool("jump", false);
            anim.SetBool("double", false);
            anim.SetBool("wallJump", false);
            ResetYForce();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsAttachedToWall())
        {
            anim.SetBool("wallJump", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsAttachedToWall())
        {
            if(!IsGrounded())
            {
                anim.SetBool("jump", true);
                isjumping = true;
            }
            isWallJumping = false;
        }

        if (collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene("Lvl_1");
        }
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

    public bool CanMove()
    {
        return !isDashing &&
               !isWallJumping;
    }


    public void Flip(Direction dir)
    {
        if(dir == Direction.Left)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            isFlipped = true;
        }

        if(dir == Direction.Right)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            isFlipped = false;
        }
    }
}