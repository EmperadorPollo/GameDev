using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMusic : MonoBehaviour
{
    private static ContinueMusic instance = null;
    public static ContinueMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        } 
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
