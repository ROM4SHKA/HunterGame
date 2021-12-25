using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Deer/Behavior/Avoidence")]
public class AvoidenceBehavior : DeerBehavior
{
    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        var newcontext = context.Where(x => x.gameObject.tag == "Deer").ToList();
        if (newcontext.Count == 0)
            return Vector2.zero;
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (var item in newcontext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < deer.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }

        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }
        return avoidanceMove;
    }
}
