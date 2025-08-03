using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class gunTower : tower
{
    [SerializeField] private int attackDistance;
    [SerializeField] private shell shellPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform shellBornPos;
    [HideInInspector] public bool isOverLoad;
    [HideInInspector] public bool isCold;
    [HideInInspector] public bool isBlast;
    [HideInInspector] public bool isMortar;
    private int overLoadCount;
    private Transform attackTarget;
    protected override void Start()
    {
        base.Start();
        mapManager.instance.addGunTower(this);
    }
    protected override void Update()
    {

        if (!isBuildEnd)
            return;
        base.Update();
        if (closestEnemyDetect() != null)//放置炮台被瞬间挤出范围导致丢失打到一半丢失目标
        {
            attackTarget = closestEnemyDetect();
            Vector2 direction = attackTarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            muzzle.transform.rotation = Quaternion.Slerp(muzzle.transform.rotation, targetRotation, 10 * Time.deltaTime);
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
        if (attackTarget == null)
            return;
        audioPlayer.Play();
        shell newShell = Instantiate(shellPrefab, shellBornPos.transform.position, Quaternion.identity);
        newShell.setTargetEnemy(attackTarget,attackPower);
        newShell.setSkill(isCold, isMortar, isBlast);
        if(isOverLoad)
        {
            if (overLoadCount >= 10)
                return;
            attackPower += attackPower * 0.01f;
        }
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