using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class smallSlime : enemy
{
    public override void die()
    {
        Instantiate(globalManager.instance.coinPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
