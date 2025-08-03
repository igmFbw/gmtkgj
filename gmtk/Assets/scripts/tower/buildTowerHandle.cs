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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            if (isClickUI())
                return;
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(dir,Vector2.zero,Mathf.Infinity,trackLayer);
            if(hit)
            {
                buildTower(hit.transform);
                globalManager.instance.costCoin(cost);
                Destroy(gameObject);
            }
            else
            {
                globalManager.instance.setTip("Please place the defense tower on the track");
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
    private void buildTower(Transform buildPos)
    {
        track buildTrack = buildPos.GetComponent<track>();
        if (buildTrack.isOccupied)
        {
            globalManager.instance.setTip("There is already a defense tower at this location");
            return;
        }
        GameObject newTower = Instantiate(towerToBuild, buildPos.position,Quaternion.identity);
        buildTrack.addTower(newTower);
        newTower.GetComponent<tower>().setPos(buildTrack);
    }
    private bool isClickUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
    private void OnDestroy()
    {
        globalManager.instance.isBuilding = false;
    }
}