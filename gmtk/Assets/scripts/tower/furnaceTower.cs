using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class furnaceTower : tower
{
    [SerializeField] private float attackDistance;
    private List<Transform> attackTargets;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        if (!isBuildEnd)
            return;
        base.Update();
        enemyDetect();
        if(attackTargets.Count > 0 )
        {
            attack();
        }
    }
    private void attack()
    {
        if (attackTimer < attackCool)
        {
            return;
        }
        attackTimer = 0;
        anim.SetBool("isAttack", true);
    }
    public override void attackKeyFps()
    {
        foreach(var item in attackTargets)
        {
            item.GetComponent<enemy>().hurt(attackPower);
        }
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
