using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : Setting<AudioManager>
{
    [System.Serializable]
   public class Sound
   {
       [Header("音频片段")]
       public AudioClip m_AudioClip;
       [Header("混合器分组")]
       public AudioMixerGroup m_MixerGroup;
       [Header("音量比例")]
       public float m_Volume=1;
       [Header("开局播放")]
       public bool playOnAwake;
       [Header("循环播放")]
       public bool loop;
   }
//    static AudioManager instance;
   public List<Sound> sounds;
   Dictionary<string,AudioSource> dictAudioSource = new Dictionary<string,AudioSource>();
//    private void Awake() {
//        instance = this;
//        dictAudioSource = new Dictionary<string, AudioSource>();
//    }
   private void Start() {

       foreach (var sound in sounds)
       {
            GameObject obj = new GameObject(sound.m_AudioClip.name);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.m_AudioClip;
            source.outputAudioMixerGroup=sound.m_MixerGroup;
            source.volume = sound.m_Volume;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            if(source.playOnAwake)
            {
                source.Play();
            }
            dictAudioSource.Add(source.clip.name,source);
       }
   }
   //外部调用
   //AudioManager.PlayAudio("背景-过关1");
   //isWait表示是否等待当前音效播放完毕
   public static void PlayAudio(string AudioClipName,bool isWait=false)
   {
       if(!instance.dictAudioSource[AudioClipName])
       {
           Debug.LogError(AudioClipName+"不存在");
       }else
       {
           AudioSource tempAudioSource =  instance.dictAudioSource[AudioClipName];
           if(isWait)
           {
               if(!tempAudioSource.isPlaying)
               {
                   tempAudioSource.Play();
               }
           }else
           {
               tempAudioSource.Play();
           }
       }
   }
   //外部调用
   //AudioManager.StopAudio("背景-过关1");
   public static void StopAudio(string AudioClipName)
   {
       if(!instance.dictAudioSource[AudioClipName])
       {
           Debug.LogError(AudioClipName+"不存在");
       }else
       {
           instance.dictAudioSource[AudioClipName].Stop();
       }
   }

}
