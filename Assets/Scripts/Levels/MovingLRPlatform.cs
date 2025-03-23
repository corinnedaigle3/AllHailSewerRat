using UnityEngine;

public class MovingLRPlatform : MonoBehaviour
{
    public float speed = 3f;
    public float moveRange = 10f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originalPosition = transform.position;
        SetNewTarget();
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        float randomDistance = Random.Range(1f, moveRange);
        targetPosition = originalPosition + Vector3.forward * (Random.value > 0.5f ? randomDistance : -randomDistance);
    }
}
