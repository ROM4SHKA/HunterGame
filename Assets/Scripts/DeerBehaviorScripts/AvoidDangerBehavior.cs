using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Deer/Behavior/AvoidDanger")]
public class AvoidDangerBehavior : DeerBehavior
{
    public float speedMultiplyer = 2f;  

    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        var newcontext = dangercontext;
        if (newcontext.Count == 0)
            return Vector2.zero;
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (var item in newcontext)
        {
            if (Vector2.SqrMagnitude(agent.transform.position - item.transform.position) < deer.checkDist)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        return avoidanceMove * speedMultiplyer;
    }
}
