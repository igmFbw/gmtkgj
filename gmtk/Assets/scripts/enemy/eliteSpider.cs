using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class eliteSpider : enemy
{
    [SerializeField] private GameObject explosionPrefab;
    public override void playAttack()
    {
        Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        attack();
        die();
    }
}
