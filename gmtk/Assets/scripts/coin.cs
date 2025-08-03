using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class coin : MonoBehaviour
{
    private void Start()
    {
        transform.DOMove(mapManager.instance.coinMovePos.position, 1).OnComplete(() =>
        {
            globalManager.instance.acquireCoin(10);
            Destroy(gameObject);
        });
    }
}
