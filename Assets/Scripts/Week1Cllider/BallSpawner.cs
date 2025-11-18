using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ballPrefab;
    public int ballSpawnCount = 100;
    public float ballSpawnInterval = 0.3f;
    public bool randomColours = true;

    IEnumerator Start()
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {
            GameObject ball = Instantiate(ballPrefab,transform.position, Quaternion.identity,transform);

            Rigidbody2D body2d = ball.GetComponent<Rigidbody2D>();
            body2d.AddForce(Random.insideUnitCircle.normalized, ForceMode2D.Impulse);

            if (randomColours)
                ball.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            
            yield return new WaitForSeconds(ballSpawnInterval);
        }
    }
    
}
