using UnityEngine;

public class SeekBehavior : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float passDistance = 4f;

    private bool isPassingThrough = false;
    private Vector2 passDirection;

    void Update()
    {
        if (target == null) return;

        if (!isPassingThrough)
        {
            
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Rotate toward the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Check if character is very close to the target 
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget < 0.2f)
            {
                isPassingThrough = true;
                passDirection = direction; // Store the current direction
            }
        }
        else
        {
            // Move in the original direction for a short distance
            transform.Translate(passDirection * speed * Time.deltaTime, Space.World);

            // Check if the character has moved past the target by passDistance
            float distanceFromTarget = Vector2.Distance(transform.position, target.position);
            if (distanceFromTarget >= passDistance)
            {
                isPassingThrough = false; // Resume seeking
            }
        }

    
    }
}