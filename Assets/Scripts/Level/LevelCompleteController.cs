using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteController : MonoBehaviour
{
    public Button lobbyBtn, nxtBtn, restartBtn;
    // Start is called before the first frame update
    void Start()
    {
        lobbyBtn.onClick.AddListener(returnLobby);        
        restartBtn.onClick.AddListener(restartLevel);
        nxtBtn.onClick.AddListener(startNextLevel);
    }

    void returnLobby(){
        SceneManager.LoadScene("LobbyScene");
    }

    void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void startNextLevel(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        string nextSceneName = LevelManager.Instance.GetNextLevel(currentSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    void OnEnable(){
        Debug.Log("Level Complete is Enabled..");
        string currentSceneName = SceneManager.GetActiveScene().name;
        string nextSceneName = LevelManager.Instance.GetNextLevel(currentSceneName);
        if (nextSceneName == "None"){
            Debug.Log("Next Level is None");
            nxtBtn.gameObject.SetActive(false);
        }
    }

    public void EnableGameObject(){
        gameObject.SetActive(true);
    }
}
