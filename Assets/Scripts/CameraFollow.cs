using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public Vector3 minValues, maxValues;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(
              Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
              Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
              Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z)
              );

        transform.position = Vector3.Lerp(transform.position, boundPosition, FollowSpeed * Time.deltaTime);
    }
}
