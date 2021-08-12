using System.Collections.Generic;
using UnityEngine;

//相当于一个tags存储器。用于辨识物体

public class Prefab : MonoBehaviour
{
    public string prefab{get => prefabName;}

    [SerializeField]
    private string prefabName = "Unknown";    //未知的会根据GameObject名初始化
    [SerializeField]
    private List<string> tags = new List<string>();
    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public void AddTag(string tag)
    {
        if(!tags.Contains(tag))
            tags.Add(tag);
    }

    public void RemoveTag(string tag)
    {   //即使删除不存在的，也不会报错
        tags.Remove(tag);
    }

    void Start()
    {
        if( prefabName=="Unknown" )
        {
            prefabName = gameObject.name.Split(' ','(')?[0];
        }
        //print(prefabName);
    }
}

//GameObject对Prefab的扩展，如果物体没有Prefab组件会自动加上
static partial class __GameObject
{
    //这两个会自己添加Prefab
    public static void AddTag(this GameObject inst,string tag)
    {
        Prefab prefab = inst.GetComponent<Prefab>();
        if(prefab==null) { prefab = inst.AddComponent<Prefab>();}
        prefab.AddTag(tag);
    }

    public static void RemoveTag(this GameObject inst,string tag)
    {
        Prefab prefab = inst.GetComponent<Prefab>();
        if(prefab==null)
        {
            inst.AddComponent<Prefab>();
            return;
        }
        prefab.RemoveTag(tag);
    }

    //下面是不会自己添加Prefab的
    public static bool HasTag(this GameObject inst,string tag)
    {
        if(inst.tag.Equals(tag)) 
        {
            return true;
        }
        else
        {
            Prefab prefab = inst.GetComponent<Prefab>();
            return prefab!=null ? prefab.HasTag(tag) : false; 
        }
    }

    public static Prefab prefab(this GameObject inst)
    {
        return inst.GetComponent<Prefab>();
    }

    public static string prefabName(this GameObject inst)
    {
       Prefab prefab = inst.GetComponent<Prefab>();
       return prefab!=null? prefab.prefab : inst.name.Split(' ','(')?[0] ;
    }
}
