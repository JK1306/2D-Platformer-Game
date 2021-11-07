using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource gameBgm,gameSfx;
    public SoundType[] soundTypes;
    private static SoundManager soundInstace;
    public static SoundManager SoundInstace{
        get{
            return soundInstace;
        }
    }
    void Awake(){
        if(soundInstace == null){
            DontDestroyOnLoad(gameObject);
            soundInstace = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start(){
        PLayMusic(Sounds.BackGroundMusic);
    }

    public void PLayMusic(Sounds sound){
        SoundType audio = Array.Find(soundTypes, item => item.sounds == sound);
        if(audio != null){
            gameBgm.clip = audio.audioClip;
            gameBgm.Play();
        }else{
            Debug.Log("Sound not found "+audio.sounds);
        }
    }

    public void Play(Sounds sound){
        SoundType audio = Array.Find(soundTypes, item => item.sounds == sound);
        if(audio != null){
                gameSfx.PlayOneShot(audio.audioClip);
        }else{
            Debug.Log("Sound not found "+audio.sounds);
        }
    }

    void OnGUI(){
        gameBgm.volume = 0.2f;
        gameSfx.volume = 0.5f;
    }
}

[Serializable]
public class SoundType{
    public Sounds sounds;
    public AudioClip audioClip;
}

public enum Sounds{
    MenuButton,
    ButtonClickPass,
    ButtonClickFail,
    BackGroundMusic,
    KeyPickUp,
    PlayerRun,
    PlayerJumpUp,
    PlayerLandDown,
    PlayerEnemyHit,
    PlayerCriticalHit,
    ChomperWalk,
    ChomperAttack,
    DoorOpen
}