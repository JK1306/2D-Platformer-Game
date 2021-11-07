using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public GameObject LevelCompletedPanel;

    void OnTriggerEnter2D(Collider2D colObj){
        if(colObj.gameObject.GetComponent<PlayerController>() != null){
            SoundManager.SoundInstace.Play(Sounds.DoorOpen);
            LevelCompletedPanel.GetComponent<LevelCompleteController>().EnableGameObject();
            Scene activeScene = SceneManager.GetActiveScene();
            LevelManager.Instance.CompleteCurrentUnLockLevel(activeScene.name);
            colObj.gameObject.GetComponent<PlayerController>().enabled = false;
            // Scene currentScene = SceneManager.GetActiveScene();
            // SceneManager.LoadScene(currentScene.name);
        }
    }
}
