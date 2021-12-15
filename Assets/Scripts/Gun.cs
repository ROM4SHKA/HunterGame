using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shotPoint;

    private float shotTimeDelay;
    [SerializeField]
    private float timeStartShooting;
    [SerializeField]
    public int numberOfBullet;
    public int currentNumberofbullet;
    private void Awake()
    {
        currentNumberofbullet = numberOfBullet;
    }
void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + offset);

        if (shotTimeDelay <= 0 && currentNumberofbullet > 0 )
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate<GameObject>(bullet, shotPoint.position, shotPoint.rotation);
                shotTimeDelay = timeStartShooting;
                currentNumberofbullet--;
            }
        }
        else
        {
            shotTimeDelay -= Time.deltaTime;
        }
    }
}
