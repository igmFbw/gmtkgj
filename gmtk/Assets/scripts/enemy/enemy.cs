using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemy : MonoBehaviour
{
    [SerializeField] protected int attackPower;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float attackCool;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected LayerMask heartLayer;
    [SerializeField] protected SpriteRenderer sr;
    protected Color initColor;
    protected Tween colorTween;
    protected float attackTimer;
    [SerializeField] private float speed;
    private float initSpeed;
    protected bool isMove;
    protected int health;
    private bool isCold;
    private void Start()
    {
        initSpeed = speed;
        health = maxHealth;
        initColor = sr.color;
        if (transform.position.x > 8.5)
            sr.flipX = true;
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
        attackDetect();
        move();
    }
    public void hurt(int damage)
    {
        health -= damage;
        sr.color = initColor;
        colorTween = sr.DOColor(Color.red, .3f).From();
        if(health <= 0)
        {
            anim.SetBool("isDie", true);
            health = 0;
        }
    }
    private void attackDetect()
    {
        Collider2D heart = Physics2D.OverlapCircle(transform.position, attackDistance, heartLayer);
        if(heart != null)
        {
            isMove = true;
            if(attackTimer > attackCool)
            {
                attackTimer = 0;
                playAttack();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    private void move()
    {
        if (isMove)
            return;
        transform.position = Vector3.MoveTowards(transform.position,mapManager.instance.heart.transform.position,Time.deltaTime);
    }
    public virtual void die()
    {
        Instantiate(globalManager.instance.coinPrefab, transform.position, Quaternion.identity);
        globalManager.instance.checkEnemy();
        Destroy(gameObject);
    }
    public virtual void playAttack()
    {
        anim.SetBool("isAttack", true);
    }
    public void attack()
    {
        mapManager.instance.heart.hurt(attackPower);
    }
    public void attackEnd()
    {
        anim.SetBool("isAttack", false);
    }
    private void OnDestroy()
    {
        colorTween.Kill();
    }
    public void setLayer(int n)
    {
        sr.sortingOrder = n;
    }
    public void loseSpeed()
    {
        if (isCold)
            return;
        isCold = true;
        StartCoroutine(speedDown());
    }
    private IEnumerator speedDown()
    {
        speed = speed * .7f;
        yield return new WaitForSeconds(3);
        speed = initSpeed;
        isCold = false;
    }
}