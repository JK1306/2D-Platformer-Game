using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadEndController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colObj){
        if(colObj.GetComponent<PlayerController>() != null && colObj.GetComponent<PlayerController>().isAlive){
            PlayerController playerObj = colObj.GetComponent<PlayerController>();
            playerObj.ReducePlayerHealth(true);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }else{
            Destroy(colObj.gameObject);
        }
    }
}
