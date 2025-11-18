using Unity.Properties;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxspeed = 0.5f;
    public float playeraccelerationtime = 2f;
    private Vector2 velocity = Vector2.zero;

    public LayerMask raymask;
    public float distanceToGround = 1.5f;

    //public float groundCheckDistance;
    private float gravity;
    public float apexHeight;
    public float apexTime;
    private float jumpVel;
    //bool that checks if you've jumped
    public bool hasJumped;


    public FacingDirection direction;


   
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        raymask = LayerMask.GetMask("groundLayer");

       
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"),0);
        MovementUpdate(playerInput);
       
    }

    private void FixedUpdate()
    {

        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        float acceleration = maxspeed / playeraccelerationtime;

        Rigidbody2D player2D = GetComponent<Rigidbody2D>();


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
            player2D.linearVelocityY = Mathf.Max(player2D.linearVelocityY, gravity); 
        }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if(IsGrounded())
        JumpInput(playerInput);


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
