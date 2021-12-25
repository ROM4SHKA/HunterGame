using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;
public class Wolf : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float healh = 100;
    private float currentHealth;
    private bool patrol;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float angryDist = 10;
    private Vector2 moveSpot;
    private void Start()
    {
        currentHealth = healh;      
        moveSpot = new Vector2(Random.Range(-FieldMetrics.WitdhField, FieldMetrics.WitdhField), Random.Range(-FieldMetrics.HeightField, FieldMetrics.HeightField));     
    }
    void Update()
    {
        if (target==null)
        { patrol = true; }
        else { patrol = false; }
        
        if (patrol)
        {
            Patrol();            
        }
        else
        {
            Angry();
        }  
    }

    private void FixedUpdate()
    {
        Hungry();
        CheckFood();
    }
    private void Angry()
    {
        if (!patrol && target)
        {
            var moveVector = Vector2.MoveTowards(transform.position, target.transform.position, speed * 1.2f * Time.deltaTime);
            transform.position = new Vector2(moveVector.x, moveVector.y);
        }
    }
    private void Patrol()
    {
        
        var moveVector = Vector2.MoveTowards(transform.position, moveSpot, speed * 0.6f * Time.deltaTime);
        
        transform.position = new Vector2(moveVector.x, moveVector.y);
        if (Vector2.Distance(transform.position, moveSpot)<0.3f)
        {
            moveSpot = new Vector2(Random.Range(-FieldMetrics.WitdhField, FieldMetrics.WitdhField), Random.Range(-FieldMetrics.HeightField, FieldMetrics.HeightField));
        }
    }
    private void CheckFood()
    {
        var food = Physics2D.OverlapCircleAll(transform.position, angryDist).ToList<Collider2D>();

        food = food.Where(x => x.tag != "Wolf").ToList();
        food = food.Where(x => x.tag != "Level").ToList();    
        if (food.Count > 0)
        {
            float minDist = 100;
            GameObject t = null;
            foreach (var i in food)
            {           
                if (i.gameObject.tag == "Player" || i.gameObject.tag == "Rabbit" || i.gameObject.tag == "Deer")
                {
                    var dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(i.gameObject.transform.position.x, i.gameObject.transform.position.y));
                    if (dist <= minDist &&
                        dist >0.5f)
                    {
                        minDist = dist;                    
                        t = i.gameObject;
                    }
                }
            }
            target = t;
        }
        else
        {          
            target = null;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Player" || tag == "Rabbit" || tag == "Deer")
        {
            Destroy(collision.gameObject);
            currentHealth = healh;
        }
    }
    void Hungry()
    {
        if (currentHealth <= 0) Destroy(gameObject);
        currentHealth -= 0.01f;
    }
}
