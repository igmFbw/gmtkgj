using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shell : MonoBehaviour
{
    private const int attackPower = 50;
    private const int speed = 5;
    private Transform targetEnemy;
    private void Start()
    {
        Destroy(gameObject, 2);
    }
    private void Update()
    {
        if (targetEnemy == null)
            return;
        Vector3 targetDir = targetEnemy.position - transform.position;
        targetDir = targetDir.normalized;
        transform.position += targetDir * Time.deltaTime * speed;
        transform.rotation = Quaternion.LookRotation(targetDir);
    }
    public void setTargetEnemy(Transform _enemy)
    {
        targetEnemy = _enemy;
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