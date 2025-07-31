using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class globalManager : MonoBehaviour
{
    [SerializeField] private AstarPath astarPath;
    public int coin;
    private static globalManager _instance;
    public static globalManager instance
    {
        get
        {
            if (_instance == null || !_instance)
            {
                _instance = FindObjectOfType<globalManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null || !_instance)
        {
            _instance = this as globalManager;
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
    public void calculateRoute()
    {
        astarPath.Scan();
    }
}
