using UnityEngine;

//全局组件，单一实例、无法禁止。挂给GLOBAL预设物
public class GlobalComponent<T> : MonoBehaviour
{
    [HideInInspector]
    public static T instance;
    protected static bool inited = false;
    public virtual void Awake()
    {
        if(inited)
            throw new System.Exception(typeof(T).FullName+" component can only be bound by one gameobject!!");
        else
        {
            inited = true;
            instance = GetComponent<T>();
        }

    }

    public virtual void OnDisable()
    {
        this.enabled = true;
        Debug.LogWarning(typeof(T).FullName + " is GlobalComponent. It can't be disabled");
    }

    public virtual void OnDestroy() 
    {
        inited = false;
    }
}
