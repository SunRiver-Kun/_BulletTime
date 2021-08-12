using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlackRushFx : MonoBehaviour
{
    [Min(0.1f),Tooltip("特效存在时间")]
    public float existTime = 1f;
    [Min(0f),Tooltip("颜色变化速率")]
    public float changeScale = 1f;
    [Tooltip("初始颜色")]
    public Color startColor;
    [Tooltip("最后颜色")]
    public Color endColor;
    public bool autoDestory = true;
    private float m_time;
    private SpriteRenderer m_render;

    void Start()
    {
        m_render = GetComponent<SpriteRenderer>();
        if(autoDestory) { Destroy(gameObject,existTime); }
    }

    void Update() 
    {
        float scale = Mathf.Clamp01(changeScale*m_time/existTime);
        m_render.color =  Color.Lerp(startColor,endColor,scale);
        m_time += Time.deltaTime;
    }

}
