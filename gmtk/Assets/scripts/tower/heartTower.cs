using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class heartTower : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Slider healthSlider;
    private int health;
    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    public void hurt(int damage)
    {
        health -= damage;
        sr.color = Color.white;
        sr.DOColor(Color.red, .3f).From();
        healthSlider.value = health;
        if (health <= 0)
        {
            anim.SetBool("isDie", true);
            Invoke("die", 1);
        }
    }
    public void die()
    {
        globalManager.instance.lose();
    }
}
