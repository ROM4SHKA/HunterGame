using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Deer/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : DeerBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        var newcontext = context.Where(x => x.gameObject.tag == "Deer").ToList();
        if (newcontext.Count == 0)
            return Vector2.zero;


        Vector2 cohesionMove = Vector2.zero;

        foreach (var item in newcontext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= newcontext.Count;

        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
