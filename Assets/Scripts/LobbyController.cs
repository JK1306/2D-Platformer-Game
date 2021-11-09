using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button playBtn, quitBtn;
    public GameObject levelPopUp;
    void Start()
    {
        playBtn.onClick.AddListener(LoadGameScene);        
    }

    private void LoadGameScene()
    {
        SoundManager.SoundInstace.Play(Sounds.MenuButton);
        levelPopUp.SetActive(true);
    }
}
