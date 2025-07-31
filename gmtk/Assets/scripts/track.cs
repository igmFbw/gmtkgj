using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class track : MonoBehaviour
{
    public bool isOccupied;
    private GameObject tower;
    public void addTower(GameObject newTower)
    {
        isOccupied = true;
        tower = newTower;
    }
    public void removeTower()
    {
        isOccupied = false;
        Destroy(tower);
    }
}
