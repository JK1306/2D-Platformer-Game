using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelManager : MonoBehaviour
{
    public string LevelName;
    Button levelBtn;
    // Start is called before the first frame update
    void Start()
    {
        levelBtn = gameObject.GetComponent<Button>();
        levelBtn.onClick.AddListener(ScitchLevel);
    }

    private void ScitchLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}
