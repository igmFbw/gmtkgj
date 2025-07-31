using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mapManager : MonoBehaviour
{
    public heartTower heart;
    private int[,] map;
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
        map = new int[18, 10];
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    
}
