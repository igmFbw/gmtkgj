using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class archer : MonoBehaviour
{
    private float attackPower;
    private const int speed = 10;
    private Vector3 targetDir;
    private bool isCold;
    private bool isPenetrate;
    private bool isFirstAttack;
    private void Start()
    {
        isFirstAttack = true;
        Destroy(gameObject, 1);
    }
    private void Update()
    {
        transform.position += targetDir * Time.deltaTime * speed;
    }
    public void setTargetEnemy(Vector3 enemyDir,Quaternion angle,float power)
    {
        targetDir = enemyDir;
        transform.rotation = angle;
        attackPower = power;
    }
    public void setSkill(bool cold,bool penetrate)
    {
        isCold = cold;
        isPenetrate = penetrate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            float damage = attackPower;
            if (isFirstAttack)
                isFirstAttack = false;
            else
                damage = damage * .2f;
            enemy target = collision.GetComponent<enemy>();
            target.hurt(Mathf.RoundToInt(damage));
            if(isCold)
                target.loseSpeed();
            if (!isPenetrate)
                Destroy(gameObject);
        }
    }
}
