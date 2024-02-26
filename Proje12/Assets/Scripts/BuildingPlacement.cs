using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;
    private BuildingPreset curBuildingPreset;
    private float indicatorUpdateTime = 0.005f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;
    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacement();
            CancelToggleBulldoze();
        }   
        if(Time.time-lastUpdateTime>=indicatorUpdateTime)//her 0.05f saniyede kontrol eder
        {
            curIndicatorPos = Selector.instance.GetCurrentTilePosition();
            if(currentlyPlacing)
            {
                placementIndicator.transform.position = curIndicatorPos;
            }
            else if(currentlyBulldozering)
            {
                bulldozerIndicator.transform.position = curIndicatorPos;
            }
            if(Input.GetMouseButtonDown(0) && currentlyPlacing)
            {
                PlaceBuilding();
                lastUpdateTime = Time.time;
            }
            else if(Input.GetMouseButtonDown(0) && currentlyBulldozering)
            {
                Bulldoze();
                lastUpdateTime = Time.time;
            }
        }
    }
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        // if(City.instance.money<preset.cost)
        // {
        //     return;
        // }
        CancelToggleBulldoze();
        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);
    }
    void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }
    public void ToggleBulldoze()
    {
        CancelBuildingPlacement();
        currentlyBulldozering = !currentlyBulldozering;
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }
    void CancelToggleBulldoze()
    {
        currentlyBulldozering = false;
        bulldozerIndicator.SetActive(false);
    }
    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());
        CancelBuildingPlacement();
    }
    void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x=>x.transform.position == curIndicatorPos);
        if(buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
