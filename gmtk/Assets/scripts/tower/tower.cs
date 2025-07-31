using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum towerType
{
    gun,arher,furnace
}
public class tower : MonoBehaviour
{
    [SerializeField] protected float attackCool;
    protected float attackTimer;
    [SerializeField] protected int attackPower;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Rigidbody2D rb;
    private bool isDrag;
    private Vector3 screenPoint;
    private Vector3 offset;
    protected virtual void Start()
    {
        attackTimer = 0;
    }
    protected virtual void Update()
    {
        attackTimer += Time.deltaTime;
    }
    protected virtual void attack()
    {
        
    }
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDrag = true;
    }
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, 100 * Time.deltaTime));
    }
    private void OnMouseUp()
    {
        isDrag = false;
        normalizePos();
        globalManager.instance.calculateRoute();
    }
    private void normalizePos()
    {
        int posX = Mathf.RoundToInt(transform.position.x);
        int posY = Mathf.RoundToInt(transform.position.y);
        transform.position = new Vector3(posX, posY);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!isDrag) return;
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
            globalManager.instance.calculateRoute();
        }
    }
}