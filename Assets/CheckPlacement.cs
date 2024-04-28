using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{

    private BuildingManager buildingManager;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        buildingManager.canPlace = true;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Object")&& !other.gameObject.name.Equals("TerrainDecoration")){
            buildingManager.canPlace = false;
           
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Object")&& !other.gameObject.name.Equals("TerrainDecoration")){
            buildingManager.canPlace = true;
        }
    }

}
