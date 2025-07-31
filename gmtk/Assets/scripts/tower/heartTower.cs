using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class heartTower : MonoBehaviour
{
    private int maxHealth;
    private int health;
    public void hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            die();
        }
    }
    public void die()
    {

    }
}
