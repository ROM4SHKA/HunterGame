using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpace : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
