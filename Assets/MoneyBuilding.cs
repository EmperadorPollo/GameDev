using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBuilding : MonoBehaviour
{
    public float moneyPerSecond; // La cantidad de dinero que el edificio produce por segundo
    private float timer = 0f; // Un temporizador para rastrear cuánto tiempo ha pasado

    void Update()
    {
        timer += Time.deltaTime; // Incrementa el temporizador por la cantidad de tiempo que ha pasado desde el último frame

        if (timer >= 1f) // Si ha pasado un segundo
        {
            timer -= 1f; // Restablece el temporizador
            AddMoney(moneyPerSecond); // Añade dinero
        }
    }

    void AddMoney(float amount)
    {
        // Aquí es donde añadirías el dinero a tu sistema de economía
        EconomyManager.Instance.AddMoney(amount);
    }
}