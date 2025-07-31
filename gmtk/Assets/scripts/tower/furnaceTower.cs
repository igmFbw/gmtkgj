using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class furnaceTower : tower
{
    [SerializeField] private int attackDistance;
    private List<Transform> attackTargets;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        enemyDetect();
        if(attackTargets.Count > 0 )
        {
            attack();
        }
    }
    protected override void attack()
    {
        base.attack();
    }
    private void enemyDetect()
    {
        attackTargets = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance);
        foreach (var hit in colliders)
        {
            if (hit.tag == "enemy")
            {
                attackTargets.Add(hit.transform);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
