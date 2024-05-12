using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1f; // La velocidad a la que se mueve el enemigo
    public float damage = 10f; // El daño que el enemigo hace al edificio
    public GameObject building; // El edificio al que el enemigo está atacando


    void Update()
    {
        // Mueve el enemigo hacia el edificio
        transform.position = Vector3.MoveTowards(transform.position, building.transform.position, speed * Time.deltaTime);

        // Si el enemigo ha llegado al edificio, ataca
        if (transform.position == building.transform.position)
        {
            AttackBuilding();
        }
    }

    void AttackBuilding()
    {
        // Aquí es donde el enemigo ataca al edificio
        Building buildingScript = building.GetComponent<Building>();
        buildingScript.TakeDamage(damage);
    }
}

