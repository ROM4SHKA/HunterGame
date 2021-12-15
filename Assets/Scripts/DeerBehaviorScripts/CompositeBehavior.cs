using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deer/Behavior/Composite")]
public class CompositeBehavior : DeerBehavior
{

    public DeerBehavior[] behaviors;
    public float[] weights;
    public override Vector2 CalculateMove(DeerAgent agent, List<Transform> context, List<Transform> dangercontext, Deer deer)
    {
        //inavailable data 
       if(weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }
        Vector2 move = Vector2.zero;

        //iterate behaviour

        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, dangercontext, deer) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }
        return move;
    }
}

