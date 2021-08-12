using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    [Tooltip("子弹预设物")]
    public BulletCommon bulletPrefab;
    [Min(0.1f),Tooltip("发射频率(秒/个)")]
    public float frequency = 3f;
    [Tooltip("是否无限弹药")]
    public bool isInfinite = true;
    [Tooltip("当不是无限子弹时，这个就是弹药数，发完就完了")]
    public int size = 0;
    [Tooltip("相对于当前物体的位置和旋转")]
    public Vector3 deltaPosition, deltaRotation;
    public bool emitInStart = true;
    public bool useBulletTime = true;

    private float m_time;

    //static 用
    private Vector3 m_pos;
    private Quaternion m_qt;

    void Emit()
    {
        if(isInfinite || (size-- > 0))
        {
            if(gameObject.isStatic)
            {
                GameObject.Instantiate(bulletPrefab,m_pos,m_qt);
            }
            else
            {
                GameObject.Instantiate(bulletPrefab,transform.position+deltaPosition,transform.rotation*Quaternion.Euler(deltaRotation));
            }
        }
    }

    void Start()
    {
        m_pos = transform.position + deltaPosition;
        m_qt = transform.rotation * Quaternion.Euler(deltaRotation);
        if(emitInStart) { Emit(); }
    }

    void Update()
    {
        if(useBulletTime && BulletTime.isBulletTime) { return; }
        m_time += Time.deltaTime;
        if(m_time > frequency)
        {
            m_time = 0f;
            Emit();
        }
        
    }
}
