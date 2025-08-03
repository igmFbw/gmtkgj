using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mapManager : MonoBehaviour
{
    public heartTower heart;
    public Transform coinMovePos;
    public List<gunTower> gunTowerList;
    public List<furnaceTower> furnaceTowerList;
    public List<archerTower> archerTowerList;
    public bool gunTowerAttackCool;
    public bool furnaceTowerAttackCool;
    public bool archerTowerAttackCool;
    private static mapManager _instance;
    public static mapManager instance
    {
        get
        {
            if (_instance == null || !_instance)
            {
                _instance = FindObjectOfType<mapManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null || !_instance)
        {
            _instance = this as mapManager;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    public void addGunTower(gunTower newTower)
    {
        gunTowerList.Add(newTower);
    }
    public void addArcherTower(archerTower newTower)
    {
        archerTowerList.Add(newTower);
    }
    public void addFurnaceTower(furnaceTower newTower)
    {
        furnaceTowerList.Add(newTower);
    }
}
