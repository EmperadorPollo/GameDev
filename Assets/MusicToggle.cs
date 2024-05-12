using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour

{
    public Button toggleMusicButton;

    // Start is called before the first frame update
    void Start()
    {
        toggleMusicButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleMusic()
    {
        ContinueMusic.Instance.ToggleMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
