using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shell : MonoBehaviour
{
    [SerializeField] private GameObject emissionPrefab;
    [SerializeField] private float emissionDistance;
    private float attackPower = 50;
    private const int speed = 10;
    private Transform targetEnemy;
    private Vector3 target;
    private bool isCold;
    private bool isMortar;
    private bool isBlast;
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    private void Update()
    {
        if (targetEnemy == null && target != Vector3.zero)
        {
            target = target.normalized;
            transform.position += target * Time.deltaTime * speed;
            return;
        }
        else if (targetEnemy == null)
            return;
        Vector3 targetDir = targetEnemy.position - transform.position;
        targetDir = targetDir.normalized;
        transform.position += targetDir * Time.deltaTime * speed;
    }
    public void setTargetEnemy(Transform _enemy,float power)
    {
        targetEnemy = _enemy;
        attackPower = power;
        target = targetEnemy.position - transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        angle += 90;
        Quaternion targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetAngle;
    }
    public void setSkill(bool cold,bool mortar,bool blast)
    {
        isCold = cold;
        isMortar = mortar;
        isBlast = blast;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            float damage =  attackPower;
            if(isMortar)
                damage += damage * .2f;
            enemy target = collision.GetComponent<enemy>();
            target.hurt(Mathf.RoundToInt(damage));
            if(isCold)
                target.loseSpeed();
            if (isBlast)
            {
                int newDamage = Mathf.RoundToInt(damage * 0.3f);
                Instantiate(emissionPrefab, transform.position, Quaternion.identity);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, emissionDistance);
                foreach (var hit in colliders)
                {
                    if (hit.tag == "enemy")
                    {
                        hit.GetComponent<enemy>().hurt(newDamage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, emissionDistance);
    }
}