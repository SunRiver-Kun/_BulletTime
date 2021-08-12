using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeSetting : Setting<AudioVolumeSetting>
{
    public AudioMixer audioMixer = null;
    public static float BGM = 0f;
    public static float SFX = 0f;
    //不同的分组设置不同的音量，可以绑定在UI中
    public void SetBGMVolume(float volume)
    {
        if(audioMixer==null) { return; }
        BGM = volume;
        audioMixer.SetFloat("BGM",BGM);
    }
    public void SetSFXVolume(float volume)
    {
        if(audioMixer==null) {return;}
        SFX = volume;
        audioMixer.SetFloat("SFX",SFX);
    }

    public override void Load()
    {
        if(audioMixer==null) {Debug.LogWarning("AudioVolumeSetting component: audioMixer is null, can't load data"); return;}
        if(File.Exists(STRING.Path.MusicData))
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(STRING.Path.MusicData));
            SetBGMVolume(reader.ReadSingle());
            SetSFXVolume(reader.ReadSingle());
            reader.Close();
        }
    }

    public override void Save()
    {
        if(audioMixer==null) {Debug.LogWarning("AudioVolumeSetting component: audioMixer is null, can't save data");  return; }
        FileStream stream = Utility.UFile.OpenFile(STRING.Path.MusicData);
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(BGM);
        writer.Write(SFX);
        writer.Close();
    }

    private void OnDestroy() 
    {
        BGM = SFX = 0f;
    }
}
