using System.Collections.Generic;
using UnityEngine;

public class HealthTrap2D : MonoBehaviour
{ 
    [Tooltip("血条的变化值，正加血，负减血")]
    public float healthDelta = -1f;
    [Tooltip("是连续的，还是一次性")]
    public bool isContinue = false;
    [Tooltip("选择一次性时，是否触发后就销毁物体")]
    public bool autoDestroy = false;

    [Min(0f),Tooltip("当是连续时，延迟为多少秒")]
    public float delay = 0f;
    [Tooltip("使用触发器/碰撞体")]
    public bool isTrigger =true;
    [Tooltip("目标预设物名")]
    public List<string> targetPrefabName = new List<string>(){STRING.Prefab.Black};  
    float m_time = 0f;
    bool m_canhurt = true;

    private void Hurt(GameObject inst)
    {
        if( targetPrefabName.Contains(inst.prefabName()))
        {
            //print(gameObject.name + " hurt " + inst.name);
            inst.GetComponent<Health>()?.Delta(healthDelta,gameObject);
            if(autoDestroy && !isContinue) { GameObject.Destroy(gameObject); }
        }
    }

    private void Update() 
    {
        if(isContinue && !m_canhurt)
        {
            m_time += Time.deltaTime;
            if(m_time > delay)
            {
                m_canhurt = true;
                m_time = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isTrigger && !isContinue)
            Hurt(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(isTrigger && isContinue && m_canhurt)
        {
            m_canhurt = false;
            Hurt(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!isTrigger && !isContinue)
            Hurt(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(!isTrigger && isContinue && m_canhurt)
        {
            m_canhurt = false;
            Hurt(other.gameObject);
        }
    }
}
