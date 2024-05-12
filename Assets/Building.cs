using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
public float health = 100f; // La salud del edificio

    public void TakeDamage(float damage)
    {
        // Reduce la salud del edificio
        health -= damage;

        // Si la salud del edificio llega a cero, el edificio es destruido
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

