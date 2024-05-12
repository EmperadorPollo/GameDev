using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public bool isFirstBuildingPlaced = false; // Variable para rastrear si el primer edificio ha sido 
    public TMPro.TextMeshProUGUI buildingMessageText; // El texto que mostrará el mensaje del edificio

    public TMPro.TextMeshProUGUI placementMessageText;

    public TMPro.TextMeshProUGUI feedbackText;
    public TMPro.TextMeshProUGUI startMessageText;
    public GameObject[] objects;

    public GameObject pendingObject;
    [SerializeField] private Material[] materials;

    private UnityEngine.Vector3 pos;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    public bool canPlace = true;
    [SerializeField] private Toggle gridToggle;

    void Start()
    {
    startMessageText.text = "Buy the antena tower so we can contact the outsiders";
    StartCoroutine(ClearStartMessageAfterDelay(3f)); // Limpia el mensaje después de 3 segundos
    }

    IEnumerator ClearStartMessageAfterDelay(float delay)
    {
    yield return new WaitForSeconds(delay);
    startMessageText.text = "";
    }


    IEnumerator ClearFeedbackTextAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    feedbackText.text = "";
}

    IEnumerator ClearBuildingMessageAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    buildingMessageText.text = "";
}

    IEnumerator ClearPlacementMessageAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    placementMessageText.text = "";
}



    void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new UnityEngine.Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                );
            }
            else {pendingObject.transform.position = pos;}
            

            if (Input.GetMouseButtonDown(0))
        {
            if (canPlace)
            {
                PlaceObject();
            }
            else
            {
                placementMessageText.text = "Not enough space";
                StartCoroutine(ClearPlacementMessageAfterDelay(2f)); // Limpia el mensaje después de 2 segundos
            }
        }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            if (Input.GetMouseButtonDown(1))
            {
            CancelPlacement();
            }
            UpdateMaterials();
        }
    }

    public void CancelPlacement()
    {
    Destroy(pendingObject);
    pendingObject = null;
    }

    public void PlaceObject()
    {
        
         MoneyBuilding moneyBuilding = pendingObject.GetComponent<MoneyBuilding>();
        if (EconomyManager.Instance.GetTotalMoney() >= moneyBuilding.buildingCost)
    {
        EconomyManager.Instance.AddMoney(-moneyBuilding.buildingCost);
        pendingObject.GetComponent<MeshRenderer>().material = materials [2];
        pendingObject = null;

        // Verifica si es la primera vez que se coloca el edificio
        if (!isFirstBuildingPlaced && moneyBuilding.buildingType == "AntenaTower")
        {
            isFirstBuildingPlaced = true;
            buildingMessageText.text = "Now we will be able to communicate with the outsiders!Thanks for playing this demo!";
            StartCoroutine(ClearBuildingMessageAfterDelay(6f)); // Limpia el mensaje después de 3 segundos
        }
    }
    else
    {
        feedbackText.text = "You dont have enough money";
        StartCoroutine(ClearFeedbackTextAfterDelay(1f));
    }
    
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(UnityEngine.Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    void UpdateMaterials(){
        if (canPlace){
            pendingObject.GetComponent<MeshRenderer>().material = materials [0];
        }
        else{
            pendingObject.GetComponent<MeshRenderer>().material = materials [1];
        }
    }
    public void SelectObject (int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        }
        else {gridOn = false;}
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff> (gridSize/2))
        {
            pos += gridSize;
        }
        return pos;
    }
    
    
}
