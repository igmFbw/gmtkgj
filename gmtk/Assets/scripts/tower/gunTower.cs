using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class gunTower : tower
{
    [SerializeField] private int attackDistance;
    [SerializeField] private GameObject shellPrefab;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        Transform attackTarget = closestEnemyDetect();
        if (attackTarget != null)
            attack();
    }
    protected override void attack()
    {
        
    }
    private Transform closestEnemyDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance);
        float closest = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.tag == "enemy")
            {
                if (Vector2.Distance(transform.position, hit.transform.position) < closest)
                {
                    closestEnemy = hit.transform;
                    closest = Vector2.Distance(transform.position, hit.transform.position);
                }
            }
        }
        return closestEnemy;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
