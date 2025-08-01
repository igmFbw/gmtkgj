using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemy : MonoBehaviour
{
    [SerializeField] private int attackPower;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCool;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask heartLayer;
    [SerializeField] private SpriteRenderer sr;
    private Tween colorTween;
    private float attackTimer;
    [SerializeField] private int speed;
    private bool isMove;
    private int health;
    private void Start()
    {
        health = maxHealth;
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
        sr.color = Color.white;
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
    public void die()
    {
        Destroy(gameObject);
    }
    public void playAttack()
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
}