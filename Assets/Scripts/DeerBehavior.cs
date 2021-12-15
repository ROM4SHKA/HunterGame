using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeerBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(DeerAgent agent,List<Transform> context, List<Transform> dangercontext, Deer deer);        
}
