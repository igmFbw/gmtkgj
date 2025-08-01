using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shell : MonoBehaviour
{
    private const int attackPower = 50;
    private const int speed = 10;
    private Transform targetEnemy;
    private Vector3 target;
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
    public void setTargetEnemy(Transform _enemy)
    {
        targetEnemy = _enemy;
        target = targetEnemy.position - transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        angle += 90;
        Quaternion targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetAngle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<enemy>().hurt(attackPower);
            Destroy(gameObject);
        }
    }
}