using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body2D;


    [Header("Horizontal Movement")]
    public float maxspeed = 0.5f;
    public float playeraccelerationtime = 0.5f;
    public float decerationTime = 0.25f;
    public LayerMask raymask;
    public LayerMask wallmask;
    public float distanceToGround = 0.8f;
    public float distanceToWall = 0.75f;
    public float jumpAway = 50f;
    //private float rightWallVel = 1f;
    public float dashVel;
    public float dashTimer;
    public float dashForce = 10f;

    [Space(15)]

    [Header("Jump Movement")]
    //public float groundCheckDistance;
    private float gravity;
    public float apexHeight;
    public float apexTime;
    private float jumpVel;
    public float terminalSpeed = -10f;
    public float coyoteTime = 0f;


    public float timer = 0f;
    //bool that checks if you've jumped
    public bool hasJumped;
    private bool hasWallJumped;

    public FacingDirection direction;


   
    public enum FacingDirection
    {
        left, right
    }

    public enum CharacterState
    {
        Idle,Walking, Jumping,Dead
    }

   

    //FacingDirection directing = FacingDirection.right;

    void Start()
    {
        raymask = LayerMask.GetMask("groundLayer");
        

        body2D.gravityScale = 0;

    }

    void Update()
    {
        //if (directing == FacingDirection.right)
        //    rightWallVel = -1;
        //else rightWallVel = 1;

            gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        MovementUpdate(playerInput);


        //if (isWalled() == true)
        //{
        //    hasWallJumped = false;
        //    walljump();
        //}

    }

    private void FixedUpdate()
    {


        Rigidbody2D player2D = GetComponent<Rigidbody2D>();

        float acceleration = maxspeed / playeraccelerationtime;


        player2D.linearVelocityX += acceleration * (Input.GetAxisRaw("Horizontal")) * Time.deltaTime;
        player2D.linearVelocityX = Mathf.Clamp(player2D.linearVelocityX, -maxspeed, maxspeed);

        if(Input.GetAxisRaw("Horizontal") == 0){
            player2D.linearVelocityX = 0;
        }


        //Dash Code
        if(Input.GetKey(KeyCode.LeftShift) && dashTimer < 0.5 && IsGrounded() == false)
        {
            startDashing();
        }
        if (IsGrounded() && dashTimer > 0.5)
        {
            dashTimer = 0;
        }





        //Jump code
        if (hasJumped)
        {
            player2D.linearVelocityY = jumpVel; 
            hasJumped = false;

        }
        //walljump code/////////////
        if (hasWallJumped)
        {
            
            Vector2 jumpDir = new Vector2(jumpAway, jumpVel);
            player2D.linearVelocity = jumpDir;
            //player2D.linearVelocityY = jumpVel;
            //player2D.linearVelocityX += dashVel;
            
        }
        ////////////////////////////////
        if (IsGrounded() == false)
        {
            player2D.linearVelocityY += gravity * Time.deltaTime;
           
        }
        if (player2D.linearVelocityY < 0) {
            player2D.linearVelocityY = Mathf.Max(player2D.linearVelocityY, terminalSpeed);
        }
    }



    private void startDashing()
    {
        
        Rigidbody2D player2D = GetComponent<Rigidbody2D>();

        player2D.linearVelocityX +=  dashForce * (Input.GetAxisRaw("Horizontal"));
        dashTimer++;

        
    }


    //walljumping bool activation///////////
    private void walljump()
    {
       

        if (Input.GetKeyDown(KeyCode.Space) && hasWallJumped == false)
        {
            hasWallJumped = true;
         
        }
    }
    /////////////////////////////////////////////////////////////////

    private void MovementUpdate(Vector2 playerInput)
    {
        if (isWalled() && IsGrounded() == false)
            walljump();


        if (IsGrounded())
        JumpInput();

        if (IsGrounded() == false)
            timer += Time.deltaTime;


        if (timer < coyoteTime) 
            JumpInput();

        if(IsGrounded() && timer > 1)
            timer = 0;

       
    }


    private void JumpInput()
    {
        //check that you have jumped
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true; 
        }
    }


    public bool IsWalking()
    {
        if (Input.GetAxisRaw("Horizontal") !=0)
        {
            return true;
        } else {
            return false;
        }
    }
    public bool IsGrounded()
    {
      

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, raymask);

        Debug.DrawRay(transform.position, Vector2.down, Color.red);


        if (hit)
        {
            Debug.Log("IsGrounded");
            return true;
        }
        else 
        {
            Debug.Log("notGrounded");
            return false;
        }

  
    }

    public bool isWalled()
    {
        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, Vector2.left, distanceToWall, wallmask);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, Vector2.right, distanceToWall, wallmask);

        Debug.DrawRay(transform.position, Vector2.left, Color.red);


        if (wallHitLeft || wallHitRight)
        {
            Debug.Log("I Am On The Wall");
            return true;
        }
        else
        {
            Debug.Log("I am Not On The Wall");
            return false;
        }
    }


    public FacingDirection GetFacingDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = FacingDirection.left;
        }
        if (Input.GetAxisRaw("Horizontal") > 0){
            direction = FacingDirection.right;
        }

        return direction;
    }





}
