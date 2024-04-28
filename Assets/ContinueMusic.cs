using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMusic : MonoBehaviour
{
            private static ContinueMusic instance = null;
    public static ContinueMusic Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource; // Referencia al componente AudioSource

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



    // Método para detener la música
    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Método para reanudar la música
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Método para alternar la música
    public void ToggleMusic()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                StopMusic();
            }
            else
            {
                // Solo reproduce la música si la escena actual es "MainMenu"
                if (SceneManager.GetActiveScene().name == "MainMenu")
                {
                    PlayMusic();
                }
            }
        }
    }
}