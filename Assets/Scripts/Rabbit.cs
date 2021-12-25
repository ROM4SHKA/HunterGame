using Assets.Scripts;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool isScared;
    private Vector2 runVector;
    private Vector2 moveSpot;
    private float newTargetDist = 10;
    private float timeScared = 2;
    private float leftTime = 0;
    [SerializeField]
    private float checkDist;
    private Rigidbody2D rb;
    private float timeforChangePos = 0f;
    void Start()
    {
        isScared = false;
        CreateNewTarget();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (leftTime <= 0)
        {
            isScared = false;          
        }
        else
        {
            timeforChangePos = 0f;
            leftTime -= 0.01f;
        }
        if (timeforChangePos > 0) { timeforChangePos -= Time.deltaTime; }
        else { CreateNewTarget(); timeforChangePos = 5; }
        if (!isScared)
        {
            Patrol();         
        }
        else
        {
            RunAway();
        }
    }
   
    void Patrol()
    {
        var moveVector = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
        transform.position = new Vector2(moveVector.x, moveVector.y);
        if (Vector2.Distance(transform.position, moveSpot) < 0.3f)
        {
            CreateNewTarget();
        }
        
    }
    private void FixedUpdate()
    {
        CheckHazard();
    }
    void RunAway()
    {
        var moveVector = Vector2.MoveTowards(transform.position, runVector * 4, speed * 4 * Time.deltaTime);
        transform.position = new Vector2(moveVector.x, moveVector.y);
        
    }
    private void CheckHazard()
    {
        
        var hazard = Physics2D.OverlapCircleAll(transform.position, checkDist).ToList<Collider2D>();
        hazard = hazard.Where(x => x.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList();
        
        if (hazard.Count > 0)
        {
            Vector3 sum = new Vector3();
            foreach (var i in hazard)
            {
                Vector3 direction = new Vector3();
                if (i.gameObject.tag == "Level")
                {
                     direction = i.transform.position - transform.position;
                }
                else
                {
                    direction = transform.position - i.transform.position;
                }                            
                sum += direction;
            }
            runVector = sum;
            isScared = true;
            leftTime = timeScared;
        }
    }
    void CreateNewTarget()
    {
        float newPointx, newPointy;
        newPointx = transform.position.x + Random.Range(-newTargetDist, newTargetDist);
        newPointy = transform.position.y + Random.Range(-newTargetDist, newTargetDist);
        if(Mathf.Abs(newPointx) > FieldMetrics.WitdhField || Mathf.Abs(newPointy) > FieldMetrics.HeightField)
        {
            CreateNewTarget();
        }
        moveSpot = new Vector2(newPointx, newPointy);
    }
   
}
