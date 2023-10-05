using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    private AudioSource audioSource;

    //key为音频文件的路径，value为音频文件的缓存
    private Dictionary<string,AudioClip> dictAudio;
    private void Awake() {
        audioManager = this;
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //辅助函数：通过对Resources.Load二次封装来加载音频
    public AudioClip LoadAudio(string path){
        return (AudioClip)Resources.Load(path);
    }
 
    //辅助函数：利用LoadAudio获取音频，并将其缓存在dictAudio中避免重复加载
    private AudioClip GetAudio(string path){
        if(!dictAudio.ContainsKey(path)){
            dictAudio[path] = LoadAudio(path);
        }
        return dictAudio[path];
    }

    //播放音效
    public void PlaySound(string path,float volume = 1f){
        //PlayOneShot可以叠加播放
        this.audioSource.PlayOneShot(GetAudio(path),volume);
    }

    //用于一些特殊场景，例如根据距离物体的远近来调整声音大小
    public void PlaySound(AudioSource audioSource,string path,float volume = 1f){
        audioSource.PlayOneShot(GetAudio(path),volume);
    }
}
