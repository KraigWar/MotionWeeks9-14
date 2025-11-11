using UnityEngine;

public class LineCast : MonoBehaviour
{

    public Transform post1;
    public Transform post2;

    void FixedUpdate()
    {
        

        if (Physics2D.Linecast(post1.position, post2.position))
        {
            Debug.Log("GOOOOALLLLLLL");
        }
    }
}
