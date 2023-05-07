using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected PhysicsObject physicsObject;

    [SerializeField]
    protected float maxSpeed = 5f;

    private (float Right, float Top, float Left, float Bottom) bounds;
    // private List<Obstacle> foundObstacles;

    // Start is called before the first frame update
    void Start()
    {
        physicsObject = GetComponent<PhysicsObject>();

        // set bounds
        bounds.Top = Camera.main.orthographicSize;
        bounds.Bottom = -Camera.main.orthographicSize;
        bounds.Left = -Camera.main.orthographicSize * Camera.main.aspect;
        bounds.Right = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSteeringForces();
    }

    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        // Return seek steering force
        return seekingForce;
    }

    public Vector3 Flee(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = transform.position - targetPos;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        // Return seek steering force
        return fleeingForce;
    }

    public Vector3 Pursue(Agent target, float secondsAhead)
    {
        return Seek(target.CalculateFuturePosition(secondsAhead));
    }

    public Vector3 Evade(Agent target, float secondsAhead)
    {
        return Flee(target.CalculateFuturePosition(secondsAhead));
    }

    public Vector3 Wander(float time)
    {
        Vector3 wanderPos = CalculateFuturePosition(time);

        float angle = Random.Range(0, Mathf.PI * 2);
        wanderPos.x += 2 * Mathf.Cos(angle);
        wanderPos.y += 2 * Mathf.Sin(angle);

        return Seek(wanderPos);
    }

    public Vector3 StayInBounds(float time)
    {
        Vector3 futurePos = CalculateFuturePosition(time);
        if (isOutOfBounds(futurePos))
        {
            return Seek(Vector3.zero);
        }
        return Vector3.zero;
    }

    public Vector3 Separate(float closeness)
    {
        List<GameObject> neighbors = AgentManager.Instance.GetNeighbors(gameObject, closeness);

        Vector3 force = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            force += Flee(neighbor.transform.position) /
                     (transform.position - neighbor.transform.position).sqrMagnitude;
        } 
        return force;
    }

    public Vector3 Cohesion(float closeness)
    {
        List<GameObject> neighbors = AgentManager.Instance.GetNeighbors(gameObject, closeness);

        Vector3 center = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Count;

        return Seek(center);
    }

    public Vector3 Align(float closeness)
    {
        List<GameObject> neighbors = AgentManager.Instance.GetNeighbors(gameObject, closeness);

        Vector3 direction = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            direction += neighbor.transform.forward;
        }

        return direction.normalized - physicsObject.Velocity;
    }

    private bool isOutOfBounds(Vector3 point)
    {
        if (point.x > bounds.Right) return true;
        if (point.x < bounds.Left) return true;
        if (point.y > bounds.Top) return true;
        if (point.y < bounds.Bottom) return true;
        return false;
    }
    
    
    public Vector3 AvoidObstacles()
    {
        List<GameObject> foundObstacles = new();
        Vector3 totalForce = Vector3.zero;

        foreach(GameObject obstacle in AgentManager.Instance.obstacles)
        {
            Vector3 AtoO = obstacle.transform.position - transform.position;
            if (Vector3.Dot(AtoO, transform.right) > obstacle.GetComponent<Obstacle>().radius + 2f) continue;
            float forwardDot = Vector3.Dot(AtoO, transform.forward);

            if (forwardDot >= 0) // in front
            {
                foundObstacles.Add(obstacle);
            }

        }

        foreach (GameObject obstacle in AgentManager.Instance.obstacles)
        {
            Vector3 AtoO = obstacle.transform.position - transform.position;
            float rightDot = Vector3.Dot(AtoO, transform.right);

            if (rightDot >= 0) totalForce -= transform.right * maxSpeed / AtoO.sqrMagnitude;
            else totalForce += transform.right * maxSpeed / AtoO.sqrMagnitude;
        }

        if (foundObstacles.Count == 0) return Vector3.zero;
        return totalForce / foundObstacles.Count;
    }
    

    public Vector3 CalculateFuturePosition(float time)
    {
        return transform.position + (physicsObject.Velocity * time);
    }

    public abstract void CalculateSteeringForces();
}
