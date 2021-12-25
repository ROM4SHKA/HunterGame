using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BulletCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    private int currentBulletNum, BulletNum;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        if (gun)
        {
            BulletNum = gun.GetComponent<Gun>().numberOfBullet;
        }
    }
    void Update()
    {
        if(gun)
        currentBulletNum = gun.GetComponent<Gun>().currentNumberofbullet;
        text.text = currentBulletNum.ToString() + " / " + BulletNum.ToString();
    }
}
