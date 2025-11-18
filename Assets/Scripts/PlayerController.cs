using Unity.Properties;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Vector2 start, end;
    public float maxspeed = 0.5f;
    public float playeraccelerationtime = 2f;
    private Vector2 velocity = Vector2.zero;


    public FacingDirection direction;


    public BoxCollider2D col;
    public float distanceToGround = 1.5f;
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"),0);
        MovementUpdate(playerInput);
       
    }
    
    private void MovementUpdate(Vector2 playerInput)
    {
        
        float acceleration = maxspeed / playeraccelerationtime;

        Rigidbody2D player2D = GetComponent<Rigidbody2D>();

        
        player2D.linearVelocityX +=  acceleration * (Input.GetAxisRaw("Horizontal"));
        player2D.linearVelocityX = Mathf.Clamp(player2D.linearVelocityX, -maxspeed, maxspeed);

        

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


        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position * Vector2.down, distanceToGround);

        Debug.DrawRay(transform.position, Vector2.down, Color.green);
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
