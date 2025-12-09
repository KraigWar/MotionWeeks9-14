using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class FlippingSwitch : MonoBehaviour
{

    public Sprite newSprite;
    private SpriteRenderer mySprite;
    public LayerMask switchMask;
    public float flipDist = 0.5f;

    public Collider2D SwitchCollider;
    public Collider2D playerCollider;
    public bool fliped = false;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flipMe() && Input.GetKey(KeyCode.E))
        {

            Physics2D.IgnoreCollision(SwitchCollider, playerCollider, ignoreSwitch());
            print("I am ignoring");
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            fliped = true;
        }
    }

   public bool flipMe()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, flipDist, switchMask);

        Debug.DrawRay(transform.position, Vector2.left, Color.red);


        if (hit)
        {
            Debug.Log("flipped");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool ignoreSwitch()
    {
        if (fliped == true)
            return true;
        else
            fliped = false;
                return false;
        

    }
}
