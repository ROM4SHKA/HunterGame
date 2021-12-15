using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deer/Behavior/StayOnScreenBehavior")]
public class StayOnScreenBehavior : DeerBehavior
{
    public Vector2 center;
    public float radius = 34f;
    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        Vector2 centerOffset = Vector2.zero;
        float xPos =  center.x - agent.transform.position.x;
        float yPos = center.y - agent.transform.position.y;
        centerOffset = new Vector2(xPos, yPos);
        float t = centerOffset.magnitude / radius;
        if(t< 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t * 1.3f;
    }
}
