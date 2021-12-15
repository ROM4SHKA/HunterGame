using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeerAgent: MonoBehaviour
{
    Deer agentDeer;
    public Deer AgentFlock { get { return agentDeer; } }

    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

   
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Deer deer)
    {
        agentDeer = deer;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
