﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button restartBtn, lobbyBtn;
    // Start is called before the first frame update
    void Start()
    {
        restartBtn.onClick.AddListener(RestartScene);
        lobbyBtn.onClick.AddListener(ReturnLobbySene);
    }

    public void EnableGameObject(){
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReturnLobbySene(){
        SceneManager.LoadScene("LobbyScene");
    }
}
