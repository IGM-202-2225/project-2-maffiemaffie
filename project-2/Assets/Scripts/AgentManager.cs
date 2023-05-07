using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentManager : Singleton<AgentManager>
{
    protected AgentManager()
    {
    }

    [SerializeField] private GameObject boringCarPrefab;
    [SerializeField] private GameObject studentCarPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject rampPrefab;

    public List<GameObject> agents = new();
    public List<GameObject> obstacles = new();
    public List<GameObject> ramps = new();

    public (float Right, float Top, float Left, float Bottom) bounds;

    void Start()
    {
        // set bounds
        bounds.Top = Camera.main.orthographicSize;
        bounds.Bottom = -Camera.main.orthographicSize;
        bounds.Left = -Camera.main.orthographicSize * Camera.main.aspect;
        bounds.Right = Camera.main.orthographicSize * Camera.main.aspect;

        SpawnRamps();
        SpawnObstacle(5);
        SpawnAgent(boringCarPrefab, 50);
        SpawnAgent(studentCarPrefab, 12);
    }

    private void SpawnAgent(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(bounds.Left, bounds.Right),
                Random.Range(bounds.Bottom, bounds.Top));
            agents.Add(Instantiate(prefab, position, Quaternion.identity));
        }
    }

    private void SpawnObstacle(int count)
    {
        float bound = 0.75f;

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(bounds.Left * bound, bounds.Right * bound),
                Random.Range(bounds.Bottom * bound, bounds.Top * bound));
            obstacles.Add(Instantiate(obstaclePrefab, position, Quaternion.identity));
        }
    }

    void OnClick()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        SpawnObstacleAt(Camera.main.ScreenToWorldPoint(mousePos));
    }

    private void SpawnObstacleAt(Vector3 position)
    {
        obstacles.Add(Instantiate(obstaclePrefab, position, Quaternion.identity));
    }

    /*
    private void SpawnRamp(int count)
    {
        float bound = 1f;

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(bounds.Left * bound, bounds.Right * bound),
                Random.Range(bounds.Bottom * bound, bounds.Top * bound));
            ramps.Add(Instantiate(rampPrefab, position, Quaternion.identity));
        }
    }
    */

    private void SpawnRamps()
    {
        SpawnRampAt(bounds.Left * 0.75f, bounds.Left * 0.5f, bounds.Top * 0.5f, bounds.Top * 0.75f);
        SpawnRampAt(bounds.Right * 0.5f, bounds.Right * 0.75f, bounds.Top * 0.5f, bounds.Top * 0.75f);
        SpawnRampAt(bounds.Left * 0.75f, bounds.Left * 0.5f, bounds.Bottom * 0.75f, bounds.Bottom * 0.5f);
        SpawnRampAt(bounds.Right * 0.5f, bounds.Right * 0.75f, bounds.Bottom * 0.75f, bounds.Bottom * 0.5f);
        SpawnRampAt(bounds.Left * 0.25f, bounds.Right * 0.25f, bounds.Bottom * 0.25f, bounds.Top * 0.25f);
    }

    private void SpawnRampAt(float left, float right, float bottom, float top)
    {
        Vector3 position = new Vector3(
                Random.Range(left, right),
                Random.Range(bottom, top));
        ramps.Add(Instantiate(rampPrefab, position, Quaternion.identity));
    }

    public List<GameObject> GetNeighbors(GameObject agent, float closeness)
    {
        return agents.FindAll(thisAgent =>
        {
            if (thisAgent == agent) return false;
            return (thisAgent.transform.position - agent.transform.position).sqrMagnitude < closeness * closeness;
        });
    }
}