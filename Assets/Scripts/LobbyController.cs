using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button playBtn, quitBtn;
    void Start()
    {
        playBtn.onClick.AddListener(LoadGameScene);        
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
