using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
