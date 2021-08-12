using UnityEngine;
using UnityEngine.UI;

public class LoadMusicData : MonoBehaviour
{
    public enum Type{BGM,SFX}
    public Type type;

    private void OnEnable() 
    {
        var slider =GetComponent<Slider>();
        if(slider==null){return;}
        slider.value = type==Type.BGM?AudioVolumeSetting.BGM:AudioVolumeSetting.SFX;
    }
}
