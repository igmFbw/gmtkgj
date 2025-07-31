using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class archer : MonoBehaviour
{
    private const int attackPower = 30;
    private const int speed = 6;
    private Vector3 targetDir;
    private void Start()
    {
        Destroy(gameObject, 2);
    }
    private void Update()
    {
        transform.position += targetDir * Time.deltaTime * speed;
        transform.rotation = Quaternion.LookRotation(targetDir);
    }
    public void setTargetEnemy(Vector3 enemyDir)
    {
        targetDir = enemyDir;
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
