using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class buildTowerHandle : MonoBehaviour
{
    [SerializeField] private LayerMask trackLayer;
    private GameObject towerToBuild;
    private int cost;
    public void setUp(GameObject towerToBuild, int cost)
    {
        this.towerToBuild = towerToBuild;
        this.cost = cost;
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, trackLayer))
            {
                buildTower(hit.transform);
                globalManager.instance.calculateRoute();
                Destroy(gameObject);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
    private void buildTower(Transform buildPos)
    {
        GameObject newTower = Instantiate(towerToBuild, buildPos.position,Quaternion.identity);
        buildPos.GetComponent<track>().addTower(newTower);
    }
}