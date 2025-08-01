using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class towerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject towerPefab;
    [SerializeField] private buildTowerHandle buildTowerHandlePrefab;
    [SerializeField] private int cost;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (globalManager.instance.isBuilding)
            return;
        globalManager.instance.isBuilding = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        buildTowerHandle buildHandle = Instantiate(buildTowerHandlePrefab, mouseWorldPos, Quaternion.identity);
        buildHandle.setUp(towerPefab, cost);
    }
}