using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tower : MonoBehaviour
{
    [SerializeField] protected float attackCool;
    protected float attackTimer;
    [SerializeField] protected int attackPower;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected towerAnimControl animControl;
    protected bool isDrag;
    private Vector3 screenPoint;
    private Vector3 offset;
    protected bool isBuildEnd;
    protected virtual void Awake()
    {
        animControl.towerAttack += attackKeyFps;
        animControl.towerAttackEnd += attackEnd;
        animControl.towerBuild += buildEnd;
    }
    protected virtual void Start()
    {
        attackTimer = 0;
    }
    protected virtual void Update()
    {
        attackTimer += Time.deltaTime;
    }
    protected virtual void OnDestroy()
    {
        animControl.towerAttack -= attackKeyFps;
        animControl.towerAttackEnd -= attackEnd;
        animControl.towerBuild -= buildEnd;
    }
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDrag = true;
    }
    protected virtual void OnMouseDrag()
    {
        if (!isBuildEnd)
            return;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, 100 * Time.deltaTime));
    }
    private void OnMouseUp()
    {
        isDrag = false;
        normalizePos();
    }
    protected virtual void normalizePos()
    {
        int posX = Mathf.RoundToInt(transform.position.x);
        int posY = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(posX, posY);
    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        if (!isDrag||!isBuildEnd) return;
        if (collision.gameObject.tag == "tower")
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                Vector3 pushDirection = collision.contacts[0].normal;
                otherRb.AddForce(-pushDirection*.01f,ForceMode2D.Force);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isDrag)
        {
            rb.velocity = Vector3.zero;
            normalizePos();
        }
    }
    public virtual void buildEnd()
    {
        anim.SetBool("buildEnd", true);
        isBuildEnd = true;
    }
    public void attackEnd()
    {
        anim.SetBool("isAttack", false);
    }
    public virtual void attackKeyFps()
    {

    }
}