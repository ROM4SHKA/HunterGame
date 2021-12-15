using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Linq;
public class Deer : MonoBehaviour
{
    public DeerAgent agentPrefab;
    List<DeerAgent> agents = new List<DeerAgent>();
    public DeerBehavior behavior;

    [Range(3, 50)]
    public int startingAmount = 10;

    [Range(5, 15)]
    public float checkDist;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 20f)]
    public float Speed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    private void Start()
    {
        squareMaxSpeed = Speed * Speed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i =0;i< startingAmount; i++)
        {
            DeerAgent newAgent = Instantiate(agentPrefab,
                new Vector3(Random.Range(-FieldMetrics.WitdhField, FieldMetrics.WitdhField), Random.Range(-FieldMetrics.HeightField, FieldMetrics.HeightField), 0),
                 Quaternion.identity, transform);
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }
    void Update()
    {
        foreach(var agent in agents)
        {
            if (agent)
            {
                List<Transform> context = GetNearbyObjects(agent);
                List<Transform> contextWolves = GetNearbyEnemies(agent);     
                Vector2 move = behavior.CalculateMove(agent, context,contextWolves, this);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * Speed;
                }

                agent.Move(move);
            }
        }
    }
    List<Transform> GetNearbyObjects(DeerAgent agent)
    {
        List<Transform> context = new List<Transform>();
        var contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius).ToList<Collider2D>();

        foreach(var c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    List<Transform> GetNearbyEnemies(DeerAgent agent)
    {
        List<Transform> context = new List<Transform>();
        var contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, checkDist).ToList<Collider2D>();

        context = context.Where(x => x.gameObject.tag == "Wolf" || x.gameObject.tag == "Player").ToList();
        foreach (var c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
              
            }
        }
        return context;
    }
}
