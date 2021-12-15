using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifetime;
    [SerializeField]
    private float collisionRad;
    [SerializeField]
    private LayerMask solid;

    private void Update()
    {
        if (lifetime < 0) { Destroy(gameObject); }
        else { lifetime -= Time.deltaTime; }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, collisionRad, solid);
        if (hitInfo.collider)
        {
            tag = hitInfo.collider.gameObject.tag;
            if (tag == "Wolf" || tag =="Rabbit" || tag =="Deer")
            {
                Destroy(hitInfo.collider.gameObject);                
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

}
