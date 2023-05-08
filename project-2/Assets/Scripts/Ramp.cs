using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    private void Awake()
    {
        AgentManager.Instance.ramps.Add(gameObject);
    }
}
