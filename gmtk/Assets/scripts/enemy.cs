using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemy : MonoBehaviour
{
    [SerializeField] private int attackPower;
    [SerializeField] private int maxHealth;
    [SerializeField] private int speed;
    [SerializeField] private AIDestinationSetter aiSetter;
    private int health;
    private void Start()
    {
        health = maxHealth;
        aiSetter.target = mapManager.instance.heart.transform;
    }
    public void hurt(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            die();
        }
    }
    public void die()
    {

    }
}
