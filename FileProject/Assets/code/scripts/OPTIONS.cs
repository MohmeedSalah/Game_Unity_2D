using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OPTIONS : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void Volume(float v){
       audioMixer.SetFloat("v",v);
    }
    public void Full(bool c){
        Screen.fullScreen=c;
    }
}
