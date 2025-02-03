using UnityEngine;

public class AvoidanceBehavior : MonoBehaviour
{
    private Transform target;
    private Transform obstacle;
    private float speed = 5f;
    private float avoidDistance = 2f;

    public void Initialize(Transform target, Transform obstacle)
    {
        this.target = target;
        this.obstacle = obstacle;
    }

    void Update()
    {
        if (target == null || obstacle == null) return;

        // 2D positions only
        Vector2 selfPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 obstaclePos = new Vector2(obstacle.position.x, obstacle.position.y);

        Vector2 directionToTarget = (targetPos - selfPos).normalized;
        Vector2 avoidDirection = (selfPos - obstaclePos).normalized;

        if (Vector2.Distance(selfPos, obstaclePos) < avoidDistance)
        {
            directionToTarget += avoidDirection * 2f;
        }

        transform.Translate(directionToTarget * speed * Time.deltaTime, Space.World);

        // 2D rotation
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}