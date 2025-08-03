using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class furnaceTower : tower
{
    [SerializeField] private float attackDistance;
    [SerializeField] private GameObject furnaceCircle;
    private List<Transform> attackTargets;
    public bool isSingle;
    public bool hance;
    protected override void Start()
    {
        base.Start();
        mapManager.instance.addFurnaceTower(this);
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
        audioPlayer.Play();
        Instantiate(furnaceCircle, transform.position, Quaternion.identity);
    }
    public override void attackKeyFps()
    {
        float damage = attackPower;
        if (hance)
            damage += damage * .2f;
        if (attackTargets.Count == 1)
            damage += damage * .5f;
        foreach(var item in attackTargets)
        {
            item.GetComponent<enemy>().hurt(Mathf.RoundToInt(damage));
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
