using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public string[] levels;
    public static LevelManager Instance{
        get{
            return instance;
        }
    }

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            // PlayerPrefs.DeleteAll();
        }else{
            Destroy(gameObject);
        }
    }

    void Start(){
        if(GetLevelStatus(levels[0]) == LevelStatus.Locked){
            SetLevelStatus(levels[0], LevelStatus.Open);
        }
    }

    public void CompleteCurrentUnLockLevel(string levelName){
        Scene currentScene = SceneManager.GetSceneByName(levelName);
        int currentBuildIndex = Array.IndexOf(levels, levelName);
        int nextSceneIndex = currentBuildIndex+1;
        if( nextSceneIndex != levels.Length){
            SetLevelStatus(levels[nextSceneIndex], LevelStatus.Open);
        }
        // Debug.Log(currentScene.name);
        // Debug.Log(currentBuild);
        // Debug.Log(levelName);
        // Debug.Log("Next Scene : "+SceneManager.GetSceneByBuildIndex(currentBuild+1).name);
        // Debug.Log(SceneUtility.GetScenePathByBuildIndex(currentBuild+1));
    }

    public string GetNextLevel(string levelName){
        int currentBuildIndex = Array.IndexOf(levels, levelName);
        int nextSceneIndex = currentBuildIndex+1;
        if( nextSceneIndex != levels.Length){
            return levels[nextSceneIndex];
        }else{
            return "None";
        }
    }

    public void SetLevelStatus(string levelName, LevelStatus lvlStatus){
        PlayerPrefs.SetInt(levelName, (int)lvlStatus);
    }

    public LevelStatus GetLevelStatus(string levelName){
        Debug.Log("Level "+levelName+" is : "+(LevelStatus)PlayerPrefs.GetInt(levelName, 0));
        return (LevelStatus)PlayerPrefs.GetInt(levelName, 0);
    }
}
