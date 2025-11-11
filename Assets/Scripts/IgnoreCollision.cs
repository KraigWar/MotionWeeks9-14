using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{

    public Collider2D ballCollider;
    public Collider2D wallCollider;

    void FixedUpdate()
    {
        //this is to get any objects to ignore one another---------------------------------

        Physics2D.IgnoreCollision(ballCollider, wallCollider);
        //------------------------------------------------------------------------


        //This is for using getIgnorecollision---------------------------------------------
        bool isIgnoring = Physics2D.GetIgnoreCollision(ballCollider, wallCollider);

        if (isIgnoring)
        {
            Debug.Log("im ignoring");

        } else {

            Debug.Log("I am no longer Ignoring");
        }
        //-----------------------------------------------

    }
    


    
    

    

}
