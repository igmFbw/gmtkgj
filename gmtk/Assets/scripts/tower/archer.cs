using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class archer : MonoBehaviour
{
    private const int attackPower = 30;
    private const int speed = 10;
    private Vector3 targetDir;
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    private void Update()
    {
        transform.position += targetDir * Time.deltaTime * speed;
    }
    public void setTargetEnemy(Vector3 enemyDir,Quaternion angle)
    {
        targetDir = enemyDir;
        transform.rotation = angle;
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
