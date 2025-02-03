using UnityEngine;

public class FleeBehavior : MonoBehaviour
{
    public Transform hazard;
    public float speed = 5f;

    void Update()
    {
        if (hazard == null) return;

        Vector2 selfPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 hazardPos = new Vector2(hazard.position.x, hazard.position.y);

        Vector2 direction = (selfPos - hazardPos).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // 2D rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Edge check 
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0.05f || viewportPos.x > 0.95f || viewportPos.y < 0.05f || viewportPos.y > 0.95f)
        {
            speed = 0;
        }
    }
}