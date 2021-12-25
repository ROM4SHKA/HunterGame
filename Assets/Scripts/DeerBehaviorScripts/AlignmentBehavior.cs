using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Deer/Behavior/Alignment")]
public class AlignmentBehavior : DeerBehavior
{
    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        var newcontext = context.Where(x => x.gameObject.tag == "Deer").ToList();
        if (newcontext.Count == 0)
            return agent.transform.up;


        Vector2 alignmentMove = Vector2.zero;
        foreach (var item in newcontext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= newcontext.Count;

        return alignmentMove;
    }
}
