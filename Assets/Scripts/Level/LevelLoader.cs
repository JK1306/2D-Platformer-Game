using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    public string LevelName;
    Button levelBtn;
    // Start is called before the first frame update
    void Start()
    {
        levelBtn = gameObject.GetComponent<Button>();
        levelBtn.onClick.AddListener(SwitchLevel);
    }

    private void SwitchLevel()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
        switch(levelStatus){
            case LevelStatus.Locked:
                break;

            case LevelStatus.Open:
                SceneManager.LoadScene(LevelName);
                break;
            
            case LevelStatus.Completed:
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
}
