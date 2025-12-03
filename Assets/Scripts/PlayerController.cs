using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body2D;


    [Header("Horizontal Movement")]
    public float maxspeed = 0.5f;
    public float playeraccelerationtime = 0.5f;
    public float decerationTime = 0.25f;
    private float acceleration;
    private float deceleration;
    private Vector2 velocity = Vector2.zero;
    public LayerMask raymask;
    public float distanceToGround = 1.5f;
    public float distancetoWall = 1.5f;
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
    private bool jumpPressed = false;

    public FacingDirection direction;


   
    public enum FacingDirection
    {
        left, right
    }

    public enum CharacterState
    {
        Idle,Walking, Jumping,Dead
    }

    private CharacterState state = CharacterState.Idle;

    void Start()
    {
        raymask = LayerMask.GetMask("groundLayer");
        

        body2D.gravityScale = 0;

    }

    void Update()
    {
        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        MovementUpdate(playerInput);

        //playerInput = new()
        //{
        //    x = Input.GetAxisRaw("Horizontal"),
        //    y = Input.GetButtonDown("Jump") ? 1 : 0
        //};

        //if(playerInput.y == 1) jumpPressed = true;


    }

    private void FixedUpdate()
    {

        // MovementUpdate();

        Rigidbody2D player2D = GetComponent<Rigidbody2D>();

        float acceleration = maxspeed / playeraccelerationtime;


        player2D.linearVelocityX += acceleration * (Input.GetAxisRaw("Horizontal")) * Time.deltaTime;
        player2D.linearVelocityX = Mathf.Clamp(player2D.linearVelocityX, -maxspeed, maxspeed);

        if(Input.GetAxisRaw("Horizontal") == 0){
            player2D.linearVelocityX = 0;
        }




        //Jump code
        if (hasJumped)
        {
            player2D.linearVelocityY = jumpVel; 
            hasJumped = false;

        }

        if (IsGrounded() == false)
        {
            player2D.linearVelocityY += gravity * Time.deltaTime;
           
        }
        if (player2D.linearVelocityY < 0) {
            player2D.linearVelocityY = Mathf.Max(player2D.linearVelocityY, terminalSpeed);
        }

        
        

    }




    private void ProcessWalkInput()
    {
        //if(playerInput.x != 0)
        //{
        //    if (Mathf.Sign(playerInput.x) != Mathf.Sign(velocity.x)) velocity.x *= 1;
        //    velocity.x += PlayerInput.x * acceleration * Time.fixedDeltaTime;

        //    velocity.x += PlayerInput.x * acceleration * Time.fixedDeltaTime;
        //    velocity.x = Mathf.Clamp(velocity.x, -maxspeed, maxspeed);
        //}
        //else if (Mathf.Abs(velocity.x) > 0.005f)
        //{
        //    velocity.x += -Mathf.Sign(velocity.x
        //}


    }

    private void ProcessJumpInput()
    {

    }


    private void MovementUpdate(Vector2 playerInput)
    {
        if(IsGrounded())
        JumpInput(playerInput);

        if (IsGrounded() == false)
            timer += Time.deltaTime;


        if (timer < coyoteTime) 
            JumpInput(playerInput);

        if(IsGrounded() && timer > 1)
            timer = 0;



        /////////
        //ProcessWalkInput();
       // ProcessJumpInput();
      //  body2D.linearVelocity = velocity;
    }


    private void JumpInput(Vector2 playerInput)
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
        //RaycastHit2D hit = Physics2D.Linecast(start + (Vector2)transform.position, end + (Vector2)transform.position);

        //Debug.DrawLine(new Vector2(transform.position.x - col.size.x / 2, transform.position.y - col.size.y / 2), new Vector2(transform.position.x + col.size.x / 2, transform.position.y - col.size.y / 2));
        //RaycastHit2D hit = Physics2D.Linecast(new Vector2(transform.position.x - col.size.x / 2, transform.position.y - col.size.y / 2), new Vector2(transform.position.x + col.size.x / 2, transform.position.y - col.size.y / 2));

        //Collider2D overlap = Physics2D.OverlapCircle(transform.position, 8f, circlemask, 0f,0f);

        //Vector3 origin = transform.position + Vector3.down * groundCheckDistance;



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
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.left, distancetoWall, raymask);

        Debug.DrawRay(transform.position, Vector2.down, Color.red);


        if (wallHit)
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


    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    coyoteTime ++;
    //    if (coyoteTime > 10)
    //    {
    //        coyoteTime = 0;
    //    }
    //}





}
