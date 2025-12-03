using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Camera))]
public class FollowCamera2D : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5;
    [SerializeField] private Tilemap tilemap;

    private Vector3 offset;

    private float leftCameraBoundray;
    private float rightCameraBoundary;
    private float bottomCameraBoundary;


    void Start()
    {
        offset = transform.position - target.position;
        calculateBounds();
    }

    private void calculateBounds()
    {
        tilemap.CompressBounds();

        Camera cam = GetComponent<Camera>();

        float orthoSize = cam.orthographicSize;
        Vector3 viewportHalfSize = new(orthoSize * cam.aspect, orthoSize);


        Vector3Int min = tilemap.cellBounds.min;
        Vector3Int max = tilemap.cellBounds.max;

        leftCameraBoundray = min.x + viewportHalfSize.x;
        rightCameraBoundary = max.x - viewportHalfSize.x;
        bottomCameraBoundary = min.y + viewportHalfSize.y;
    }



    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 steppedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

        steppedPosition.x = Mathf.Clamp(steppedPosition.x, leftCameraBoundray, rightCameraBoundary);
        steppedPosition.y = Mathf.Clamp(steppedPosition.y, bottomCameraBoundary, steppedPosition.y);

        transform.position = steppedPosition;
    }
}
