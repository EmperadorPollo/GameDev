using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance; // Instancia estática para permitir el acceso desde otros scripts

    private float totalMoney = 0f; // La cantidad total de dinero en la economía
    public TMPro.TextMeshProUGUI moneyText;

    void Awake()
    {
        // Asegura que solo haya una instancia de EconomyManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hace que el objeto no se destruya al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(float amount)
    {
        totalMoney += amount;
        Debug.Log("Dinero añadido! Total ahora: " + totalMoney);
        moneyText.text = "Money: " + totalMoney.ToString();
    }

    public float GetTotalMoney()
    {
        return totalMoney;
    }
}