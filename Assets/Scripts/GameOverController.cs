using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button restartBtn, quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        restartBtn.onClick.AddListener(RestartScene);
        quitBtn.onClick.AddListener(QuitSene);
    }

    // Update is called once per frame
    void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitSene(){
        Debug.Log("Not Implemented !!");
    }
}
