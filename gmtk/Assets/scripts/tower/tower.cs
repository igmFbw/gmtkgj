using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum towerType
{
    archer,furnace,gun
}
public class tower : MonoBehaviour
{
    [SerializeField] private GameObject attackRange;
    [SerializeField] protected float attackCool;
    protected float attackTimer;
    [SerializeField] protected float attackPower;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected towerAnimControl animControl;
    [SerializeField] protected LayerMask trackLayer;
    [SerializeField] protected AudioSource audioPlayer;
    [SerializeField] protected AudioClip attackClip;
    [SerializeField] protected AudioClip buildClip;
    public towerType type;
    protected bool isDrag;
    private Vector3 screenPoint;
    private Vector3 offset;
    protected bool isBuildEnd;
    private track towerPos;
    protected virtual void Awake()
    {
        animControl.towerAttack += attackKeyFps;
        animControl.towerAttackEnd += attackEnd;
        animControl.towerBuild += buildEnd;
    }
    protected virtual void Start()
    {
        attackTimer = 0;
        audioPlayer.clip = buildClip;
        audioPlayer.Play();
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
        rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, 100 * Time.deltaTime));    }
    private void OnMouseUp()
    {
        isDrag = false;
        normalizePos();
    }
    protected virtual void normalizePos()
    {
        towerPos.removeTower();
        int posX = Mathf.RoundToInt(transform.position.x);
        int posY = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(posX, posY);
        Vector3 dir = new Vector3(posX,posY, 0);
        RaycastHit2D hit = Physics2D.Raycast(dir, Vector2.zero, Mathf.Infinity, trackLayer);
        if (hit)
        {
            track newTrack = hit.transform.GetComponent<track>();
            newTrack.addTower(gameObject);
            towerPos = newTrack;
        }
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
        audioPlayer.Stop();
        audioPlayer.clip = attackClip;
    }
    public void attackEnd()
    {
        anim.SetBool("isAttack", false);
    }
    public virtual void attackKeyFps()
    {

    }
    public void setPos(track pos)
    {
        towerPos = pos;
    }
    public void addAttackPower(int value)
    {
        attackPower += value;
    }
    public void addVelocity(float value)
    {
        attackCool -= value;
        if(attackCool <= 0.35f)
        {
            attackCool = 0.35f;
            if (type == towerType.archer)
                mapManager.instance.archerTowerAttackCool = true;
            else if (type == towerType.gun)
                mapManager.instance.gunTowerAttackCool = true;
            else 
                mapManager.instance.furnaceTowerAttackCool = true;
        }
    }
    private void OnMouseEnter()
    {
        attackRange.SetActive(true);
    }
    private void OnMouseExit()
    {
        attackRange.SetActive(false);
    }
}