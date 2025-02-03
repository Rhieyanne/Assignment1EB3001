using UnityEngine;

public class ArrivalBehavior : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f;
    public float slowingRadius = 3f;

    void Update()
    {
        if (target == null) return;

        
        Vector2 selfPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);

        Vector2 direction = targetPos - selfPos;
        float distance = direction.magnitude;

        float currentSpeed = (distance <= slowingRadius) ?
            maxSpeed * (distance / slowingRadius) :
            maxSpeed;

        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}