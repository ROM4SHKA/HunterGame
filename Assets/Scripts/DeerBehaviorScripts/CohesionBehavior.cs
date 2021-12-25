using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Deer/Behavior/Cohesion")]
public class CohesionBehavior : DeerBehavior
{
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
        cohesionMove /= context.Count;

        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }
}
