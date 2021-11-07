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
                SoundManager.SoundInstace.Play(Sounds.ButtonClickFail);
                break;

            case LevelStatus.Open:
                SoundManager.SoundInstace.Play(Sounds.ButtonClickPass);
                SceneManager.LoadScene(LevelName);
                break;
            
            case LevelStatus.Completed:
                SoundManager.SoundInstace.Play(Sounds.ButtonClickPass);
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
}
