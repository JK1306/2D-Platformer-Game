using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public GameObject LevelCompletedPanel;
    void OnCollisionEnter2D(Collision2D col){
        Debug.Log("Collision Occured");
    }

    void OnTriggerEnter2D(Collider2D colObj){
        if(colObj.gameObject.GetComponent<PlayerController>() != null){
            LevelCompletedPanel.GetComponent<LevelCompleteController>().EnableGameObject();
            Scene activeScene = SceneManager.GetActiveScene();
            LevelManager.Instance.CompleteCurrentUnLockLevel(activeScene.name);
            // Scene currentScene = SceneManager.GetActiveScene();
            // SceneManager.LoadScene(currentScene.name);
        }
    }
}
