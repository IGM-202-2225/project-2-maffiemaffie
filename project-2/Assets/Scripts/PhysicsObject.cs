using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    private Vector3 position = Vector3.zero;
    public Vector3 Direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    public Vector3 Velocity { get { return velocity; } }

    private Vector3 acceleration = Vector3.zero;

    public bool UseFriction;
    public bool UseGravity;

    public float GravityStrength;
    public float FrictionCoeff;
    public float radius;


    [SerializeField]
    float mass = 1f;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        Direction = velocity.normalized;

        transform.position = position;

        acceleration = Vector3.zero;
        transform.rotation = Quaternion.Euler(new(0f, 0f, -90 + Mathf.Rad2Deg * Mathf.Atan2(Direction.y, Direction.x)));
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        ApplyForce(friction);
    }

    public void ApplyGravity(Vector3 force)
    {
        acceleration += force;
    }

    public void BounceX()
    {
        velocity = new Vector3(-velocity.x, velocity.y);
    }

    public void BounceY()
    {
        velocity = new Vector3(velocity.x, -velocity.y);
    }
}
