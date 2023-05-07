using UnityEngine;

public class BoringCar : Agent
{
    [SerializeField]
    private Sprite freewaySprite;

    [SerializeField]
    private Sprite depressionSprite;

    [SerializeField]
    private float separateScalar;

    [SerializeField]
    private float cohesionScalar;

    [SerializeField]
    private float alignScalar;

    [SerializeField]
    private float inBoundsScalar;

    [SerializeField]
    private float seekScalar;

    [SerializeField]
    private float fleeScalar;

    [SerializeField]
    private float obstacleScalar;

    [SerializeField]
    private float rampCloseness;

    [SerializeField]
    private float rampFarness;

    [SerializeField]
    private float breakdownTime;

    private GameObject targetedRamp;
    private State state = State.Freeway;
    private float sinceDepressionStarted = 0f;

    void Start()
    {
        GameObject closest = AgentManager.Instance.ramps[0];
        foreach (GameObject ramp in AgentManager.Instance.ramps)
        {
            if ((ramp.transform.position - transform.position).sqrMagnitude <
                (closest.transform.position - transform.position).sqrMagnitude)
                closest = ramp;
        }
        targetedRamp = closest;
    }

    public override void CalculateSteeringForces()
    {
        Vector3 force = Vector3.zero;
        if (state == State.Freeway)
        {
            force += DoFreewayShit();
        } 
        else
        {
            force += DoHaveDepression();
        }

        force += separateScalar * Separate(1f);
        force += cohesionScalar * Cohesion(2f);
        force += alignScalar * Align(1f);
        force += inBoundsScalar * StayInBounds(0.5f);
        force += obstacleScalar * AvoidObstacles();

        physicsObject.ApplyForce(force);
    }

    private Vector3 DoFreewayShit()
    {
        if ((targetedRamp.transform.position - physicsObject.transform.position).sqrMagnitude < rampCloseness * rampCloseness)
        {
            state = State.Depression;
            GetComponent<SpriteRenderer>().sprite = depressionSprite;
            sinceDepressionStarted = 0;
        }

        return seekScalar * Seek(targetedRamp.transform.position);
    }

    private Vector3 DoHaveDepression()
    {
        sinceDepressionStarted += Time.deltaTime;
        if ((targetedRamp.transform.position - physicsObject.transform.position).sqrMagnitude > rampFarness * rampFarness
            || sinceDepressionStarted > breakdownTime)
        {
            state = State.Freeway;
            GetComponent<SpriteRenderer>().sprite = freewaySprite;

            while (true)
            {
                int index = Random.Range(0, AgentManager.Instance.ramps.Count);
                GameObject nextTarget = AgentManager.Instance.ramps[index];
                if (nextTarget == targetedRamp) continue;

                targetedRamp = nextTarget;
                break;
            }
        }
       
        return fleeScalar * Flee(targetedRamp.transform.position);
    }

    private enum State
    {
        Freeway,
        Depression
    }
}
