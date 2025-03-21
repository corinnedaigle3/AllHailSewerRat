using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private onTrigger trigger;
    private Vector3 originalPosition;
    private Vector3 upPosition;
    private Vector3 downPosition;
    public float speed = 5f;
    private bool isMoving = false;

    void Start()
    {
        originalPosition = transform.position;
        upPosition = originalPosition + Vector3.up * 30;
        downPosition = originalPosition + Vector3.down * 30;

        trigger = GetComponent<onTrigger>();

    }

    void Update()
    {
        if (!isMoving)
        {
            if (trigger != null && trigger.OnUp)
            {
                StartCoroutine(MovePlatform(upPosition, originalPosition));
            }
            else if (trigger != null && trigger.OnDown)
            {
                StartCoroutine(MovePlatform(downPosition, originalPosition));
            }
        }
    }

    IEnumerator MovePlatform(Vector3 targetPosition, Vector3 returnPosition)
    {
        isMoving = true;
        yield return StartCoroutine(MoveTo(targetPosition));
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(MoveTo(returnPosition));
        isMoving = false;
    }

    IEnumerator MoveTo(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
