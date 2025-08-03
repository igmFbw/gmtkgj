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
    public bool isPenetrate;
    public bool isCold;
    public bool isDivision;
    protected override void Start()
    {
        base.Start();
        attackDir = mapManager.instance.heart.transform.position - transform.position;
        attackDir = attackDir.normalized;
        mapManager.instance.addArcherTower(this);
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
        audioPlayer.Play();
        archer newArcher = Instantiate(archerPrefab, transform.position, Quaternion.identity);
        newArcher.setTargetEnemy(attackDir, targetAngle,attackPower);
        newArcher.setSkill(isCold, isPenetrate);
        if (isDivision)
        {
            archer archerD1 = Instantiate(archerPrefab, transform.position, Quaternion.identity);
            archerD1.setTargetEnemy(calculateDivisionDir(30), calculateDivisionAngle(30), attackPower * .25f);
            archerD1.setSkill(isCold, isPenetrate);
            archer archerD2 = Instantiate(archerPrefab, transform.position, Quaternion.identity);
            archerD2.setTargetEnemy(calculateDivisionDir(-30), calculateDivisionAngle(-30), attackPower * .25f);
            archerD2.setSkill(isCold, isPenetrate);
        }
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
    public Quaternion calculateDivisionAngle(int num)
    {
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        angle += 90;
        angle += num;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public Vector3 calculateDivisionDir(int num)
    {
        Vector3 newDir = attackDir.normalized;
        return Quaternion.Euler(0, 0, num) * newDir;
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