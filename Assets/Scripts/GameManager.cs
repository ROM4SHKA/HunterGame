using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    // Deer createing from another Script
    [SerializeField]
    private int rabbits;
    [SerializeField]
    private int wolves;

    [SerializeField]
    private GameObject wolf;
    [SerializeField]
    private GameObject rabbit;
  
    void Start()
    {
        if (wolf)
        {
            for (int i = 0; i < wolves; i++)
            {
                CreateWolf();
            }
        }     
        if (rabbit)
        {
            for (int i = 0; i < rabbits; i++)
            {
                CreateRabbit();
            }
        }

    }

    void CreateWolf()
    {
        Instantiate(wolf, new Vector3(Random.Range(-FieldMetrics.WitdhField, FieldMetrics.WitdhField), Random.Range(-FieldMetrics.HeightField, FieldMetrics.HeightField), 0), Quaternion.identity);
    }
  
    void CreateRabbit()
    {
        Instantiate(rabbit, new Vector3(Random.Range(-FieldMetrics.WitdhField, FieldMetrics.WitdhField), Random.Range(-FieldMetrics.HeightField, FieldMetrics.HeightField), 0), Quaternion.identity);
    }
}
