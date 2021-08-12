using System.IO;
using System.Collections.Generic;
using UnityEngine;

//通用的函数/结构体/类
namespace Utility
{ 
    [System.Serializable]
    public class Pair<T1,T2>
    {
        public T1 first;
        public T2 second;
        public Pair(T1 first,T2 second) {this.first = first; this.second = second;}
    }


    //至少有参数GameObject
    [System.Serializable]
    public class Closure
    {
        public delegate void FnType(GameObject inst);
        public GameObject inst;
        public FnType fn;
        public Closure(GameObject inst,FnType fn)
        {
            this.inst = inst;
            this.fn = fn;
        }
        public virtual void Call()
        {
            fn(inst);
        }
    }

    [System.Serializable]
    public class Closure<Value> : Closure
    {
        public new delegate void FnType(GameObject inst,Value data);
        public Closure(GameObject inst,FnType fn,Value data) : base(inst,(GameObject o)=>{ fn(o,data); }) {}
    }

    public static class UColor
    {
        public static void ChangeColor(Pair<SpriteRenderer,Color>[] data)
        {
            if(data==null) { return; }
            for (int i = 0; i < data.Length; ++i)
            {
                var v = data[i];
                if(v.first==null) { continue; }
                v.first.color = v.second;
            }
        }
        public static void ChangeColor(List<Pair<SpriteRenderer,Color>> data) 
        { 
            if(data==null) {return;}
            for (int i = 0; i < data.Count; ++i)
            {
                var v = data[i];
                if(v.first==null) { continue; }
                v.first.color = v.second;
            }
        }
    }
    public static class UFile
    {
        public static FileStream OpenFile(string filepath)
        {
            FileStream stream;
            if(!File.Exists(filepath)) 
            { 
                var path = new FileInfo(filepath);
                if(!path.Directory.Exists)  { path.Directory.Create(); }
                stream = path.Create();
            }
            else
            {
                stream = File.OpenWrite(filepath);
            }
            return stream;
        }
    }

    //扩展  要先 using Utility.     然后 this.RequireTrigger2D(...)
    static partial class __Component
    {
        //如果改物体没有触发器就警告或抛出异常
        public static void RequireTrigger2D(this Component component,string message = null,bool isFatal = false)
        {
            bool ok = false;
            foreach(var collider in component.GetComponents<Collider2D>() )
            {
                if(collider.isTrigger && collider.isActiveAndEnabled)
                {
                    ok = true;
                    break;
                }
            }   
            if(!ok) 
            {
                message = message ?? "Some components touched to " + component.name + " need a trigger2D.";
                if(isFatal)
                    throw new System.Exception(message);
                else
                    Debug.LogWarning(message);
            }
        }
    }
}
