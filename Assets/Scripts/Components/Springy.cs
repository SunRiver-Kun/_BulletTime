using System.Collections.Generic;
using UnityEngine;

//根据碰撞面的法线基于弹力
public class Springy : MonoBehaviour
{
    [Header("弹力")]
    [Min(0f),Tooltip("水平弹力")]
    public float horizontalForce = 10f;
    [Min(0f),Tooltip("垂直弹力")]
    public float verticalForce = 8f;
    
    [Header("其他")]
    [Tooltip("是否在子弹时间停止弹力检查")]
    public bool useBulletTime = true;
    public bool onlyPlayer = true;


    private Collider2D m_collider;
    private Dictionary<Rigidbody2D,Vector2> collisions = new Dictionary<Rigidbody2D, Vector2>();
    void AddForce(Rigidbody2D rigidbody,Vector2 force)
    {
        Black black;
        if(rigidbody.TryGetComponent<Black>(out black)) { black.StopRush(); black.ResetRushCount(); }
        rigidbody.AddForce(force,ForceMode2D.Impulse);
    }
    
    void Leave()
    {
        foreach (var v in collisions)
        {
            AddForce(v.Key,v.Value);
        }
        collisions.Clear();
    }

    void OnEnable() 
    {
        if(useBulletTime) { BulletTime.LeaveBulletTime += Leave; }
    }

    void Start()
    {
        m_collider = GetComponent<Collider2D>();
        if(m_collider==null || m_collider.isTrigger)
        {
            Debug.LogWarning("Springy component need a Collider to work.");
        }
    }

    void OnDisable() 
    {
        if(useBulletTime) { BulletTime.LeaveBulletTime -= Leave; }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(onlyPlayer && !other.gameObject.HasTag(STRING.Tag.Player)) {return;}
        Rigidbody2D rigidbody = other.gameObject.GetComponent<Rigidbody2D>();
        if(rigidbody==null) { return; }

        Vector2 force = -other.GetContact(0).normal.normalized;
        force.x *= horizontalForce;
        force.y *= verticalForce;


        if(useBulletTime && BulletTime.isBulletTime)
        {
            collisions.Add(rigidbody,force);
        }
        else
        {
            AddForce(rigidbody,force);
        }
           
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        Rigidbody2D rigidbody;
        if(other.gameObject.TryGetComponent<Rigidbody2D>(out rigidbody)) 
        { 
            collisions.Remove(rigidbody);
        }
    }
}
