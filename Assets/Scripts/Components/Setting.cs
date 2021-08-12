using UnityEngine;

public class Setting<T> : MonoBehaviour
{
    [HideInInspector]
    public static T instance;
    protected static bool inited = false;
    public virtual void Load(){}
    public virtual void Save(){}
    
    protected virtual void Awake()
    {
        if(inited) 
            Destroy(gameObject);
        else 
        {
            inited =  true;
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
            Load();
            print("Load "+typeof(T).Name);
        }
    }
}
