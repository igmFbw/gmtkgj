using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class archerTower : tower
{
    [SerializeField] private GameObject archerPrefab;
    private Vector2 attackDir;
    protected override void Start()
    {
        base.Start();
        attackDir = mapManager.instance.heart.transform.position - transform.position;
        attackDir = attackDir.normalized;
    }
    protected override void Update()
    {
        base.Update();
        attack();
    }
    protected override void attack()
    {
        if (attackTimer < attackCool)
        {
            return;
        }
        attackTimer = 0;
        
    }
}
