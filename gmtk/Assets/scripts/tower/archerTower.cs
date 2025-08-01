using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class archerTower : tower
{
    [SerializeField] private archer archerPrefab;
    [SerializeField] private Transform bow;
    private Vector2 attackDir;
    private Quaternion targetAngle;
    [SerializeField] private float attackDistance;
    protected override void Start()
    {
        base.Start();
        attackDir = mapManager.instance.heart.transform.position - transform.position;
        attackDir = attackDir.normalized;
    }
    protected override void Update()
    {
        if (!isBuildEnd)
            return;
        base.Update();
        if (enemyDetect())
            attack();
    }
    private void OnDrawGizmos()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + attackDistance) ;
        Gizmos.DrawLine(transform.position, newPos);
    }
    private bool enemyDetect()
    {
        return Physics2D.Raycast(transform.position, attackDir, attackDistance,enemyLayer);
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
        archer newArcher = Instantiate(archerPrefab, transform.position, Quaternion.identity);
        newArcher.setTargetEnemy(attackDir, targetAngle);
    }
    public override void buildEnd()
    {
        base.buildEnd();
        calculateRotate(false);
    }
    public void calculateRotate(bool isLerp)
    {
        attackDir = transform.position - mapManager.instance.heart.transform.position;
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        angle += 90;
        targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        rotateBow(isLerp);
    }
    private void rotateBow(bool isLerp)
    {
        if (isLerp)
            bow.rotation = Quaternion.Slerp(bow.rotation, targetAngle, 10 * Time.deltaTime);
        else
            bow.rotation = targetAngle;
    }
    protected override void normalizePos()
    {
        base.normalizePos();
        calculateRotate(true);
    }
    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
        calculateRotate(false) ;
    }
    protected override void OnCollisionStay(Collision collision)
    {
        if (!isDrag) return;
        if (collision.gameObject.tag == "tower")
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                Vector3 pushDirection = collision.contacts[0].normal;
                otherRb.AddForce(-pushDirection * .01f, ForceMode2D.Force);
            }
            calculateRotate(false);
        }
    }
}