using UnityEngine;

public class MovingLeftRight : MonoBehaviour
{

    public GameObject leftBall;
    public float forceAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movement(forceAmount);
        }
          
    }

    private void movement(float forceAmount)
    {
        Rigidbody2D leftball2D = leftBall.GetComponent<Rigidbody2D>();

        leftball2D.AddForce(Vector2.right * forceAmount, ForceMode2D.Force);
    }
}
