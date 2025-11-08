using UnityEngine;

public class TorqueTesting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float forcesApplied = 1f;


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AddTorqueImpulse(45);
        }
        if (Input.GetKey(KeyCode.D))
        {
            AddTorqueImpulse(45 * -1);
        }
    }

    public void AddTorqueImpulse(float angleDegree)
    {
        Rigidbody2D square = GetComponent<Rigidbody2D>();
        float impulse = (angleDegree * Mathf.Deg2Rad) * square.inertia;

        square.AddTorque(impulse * forcesApplied, ForceMode2D.Impulse);

        
    }
    

}
