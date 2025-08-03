using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class eliteSlime : enemy
{
    [SerializeField] private GameObject smallSlime;
    public override void die()
    {
        globalManager.instance.checkEnemy();
        Instantiate(globalManager.instance.coinPrefab, transform.position, Quaternion.identity);
        Instantiate(smallSlime,transform.position, Quaternion.identity);
        Instantiate(smallSlime, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
