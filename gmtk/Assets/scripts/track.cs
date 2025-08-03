using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class track : MonoBehaviour
{
    public bool isOccupied;
    private GameObject tower;
    [SerializeField] private BoxCollider2D col;
    public void addTower(GameObject newTower)
    {
        isOccupied = true;
        tower = newTower;
        col.enabled = false;
    }
    public void destroyTower()
    {
        removeTower();
        Destroy(tower);
    }
    public void removeTower()
    {
        isOccupied = false;
        col.enabled = true;
    }
}
