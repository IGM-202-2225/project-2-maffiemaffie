using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentCar : Agent
{
    [SerializeField]
    private Sprite anxietySprite;

    [SerializeField]
    private Sprite panicSprite;

    [SerializeField]
    private float separateScalar;

    [SerializeField]
    private float cohesionScalar;

    [SerializeField]
    private float alignScalar;

    [SerializeField]
    private float inBoundsScalar;

    [SerializeField]
    private float wanderScalar;

    [SerializeField]
    private float obstacleScalar;

    [SerializeField]
    private float panicScalar;

    private State state = State.Anxiety;

    public override void CalculateSteeringForces()
    {
        Vector3 force = Vector3.zero;
        if (state == State.Anxiety)
        {
            force += DoAnxietyShit();
        }
        else
        {
            force += PANIC_OH_MY_GOD_PANIC();
        }

        force += separateScalar * Separate(1f);
        force += inBoundsScalar * StayInBounds(0.5f);
        force += obstacleScalar * AvoidObstacles();

        physicsObject.ApplyForce(force);
    }

    private Vector3 DoAnxietyShit()
    {
        if (AgentManager.Instance.GetNeighbors(gameObject, 1f).Count > 0)
        {
            state = State.Panic;
            GetComponent<SpriteRenderer>().sprite = panicSprite;
        }

        foreach (GameObject ramp in AgentManager.Instance.ramps)
        {
            if ((ramp.transform.position - transform.position).sqrMagnitude < 0.01f)
            {
                state = State.Panic;
                GetComponent<SpriteRenderer>().sprite = panicSprite;
                break;
            }
        }

        Vector3 force = Vector3.zero;
        force += wanderScalar * Wander(1f);
        force += 0.2f * Seek(Vector3.zero);
        force += cohesionScalar * Cohesion(2f);
        force += alignScalar * Align(1f);

        return force;
    }

    private Vector3 PANIC_OH_MY_GOD_PANIC()
    {
        if (shouldStopPanicking())
        {
            state = State.Anxiety;
            GetComponent<SpriteRenderer>().sprite = anxietySprite;
        }

        return panicScalar * AvoidEveryoneElse();
    }

    private bool shouldStopPanicking()
    {
        const float boundsThreshold = 0.5f;
        if (AgentManager.Instance.bounds.Right - transform.position.x < boundsThreshold) return true;
        if (transform.position.x - AgentManager.Instance.bounds.Left < boundsThreshold) return true;
        if (AgentManager.Instance.bounds.Top - transform.position.y < boundsThreshold) return true;
        if (transform.position.y - AgentManager.Instance.bounds.Bottom < boundsThreshold) return true;
        if (AgentManager.Instance.GetNeighbors(gameObject, 2f).Count == 0) return true;
        return false;
    }

    private Vector3 AvoidEveryoneElse()
    {
        List<GameObject> foundCars = new();
        Vector3 totalForce = Vector3.zero;

        foreach (GameObject car in AgentManager.Instance.agents)
        {
            if (car == gameObject) continue;

            Vector3 AtoO = car.transform.position - transform.position;
            if (Vector3.Dot(AtoO, transform.right) > 1f) continue;
            float forwardDot = Vector3.Dot(AtoO, transform.forward);

            if (forwardDot >= 0) // in front
            {
                foundCars.Add(car);
            }

        }

        foreach (GameObject car in foundCars)
        {
            Vector3 AtoO = car.transform.position - transform.position;
            float rightDot = Vector3.Dot(AtoO, transform.right);

            if (AtoO.sqrMagnitude == 0) continue;

            if (rightDot >= 0) totalForce -= transform.right * maxSpeed / AtoO.sqrMagnitude;
            else totalForce += transform.right * maxSpeed / AtoO.sqrMagnitude;
        }

        if (foundCars.Count == 0) return Vector3.zero;
        return totalForce / foundCars.Count;
    }

    private enum State
    {
        Anxiety,
        Panic
    }
}
