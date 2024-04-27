using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
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
            

            if (Input.GetMouseButtonDown(0)&& canPlace)
            {
                PlaceObject();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            UpdateMaterials();
        }
    }

    public void PlaceObject()
    {

    // Ajusta la posición del objeto para que se coloque con la base en el suelo
    float objectHeight = pendingObject.GetComponent<Collider>().bounds.size.y;
    pendingObject.transform.position = new UnityEngine.Vector3(pendingObject.transform.position.x, 
    pendingObject.transform.position.y + objectHeight / 2, pendingObject.transform.position.z);
        
        pendingObject.GetComponent<MeshRenderer>().material = materials [2];
        pendingObject = null;
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
