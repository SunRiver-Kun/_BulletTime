using System.Collections.Generic;
using UnityEngine;
using Utility;
public class BulletCommon : MonoBehaviour
{
    public enum MoveMethod{
        Transform, Rigidbody2D, None
    }
    

    [Tooltip("是否一初始化就发射")]
    public bool ShootInStart = true;
    [Tooltip("子弹速度")]
    public float speed = 1f;
    [Tooltip("是否启用子弹时间控制")]
    public bool useBulletTime = true;
    
    public MoveMethod method = MoveMethod.Transform;

    [Tooltip("是否在碰撞到物体就销毁")]
    public bool destroyWhenHit = true;
    [Tooltip("启用碰撞销毁时，碰撞而不销毁的目标标签")]
    public List<string> ignorePrefabName= new List<string>();

    public event Closure<Collision2D>.FnType E_Hit;


    private Transform m_transform;
    private Rigidbody2D m_rigidbody2D;
    private Vector3 m_direction;
    private bool m_canmove = true;

    private Vector2 m_rigidbodySpeed = new Vector2();
    private bool m_added = false;

    public void Shoot()
    {
        if(method==MoveMethod.Transform)
        {
            m_canmove = true;
        }
        else 
        {
            m_rigidbody2D.velocity = new Vector2(m_direction.x,m_direction.y)*speed;
            m_rigidbodySpeed = m_rigidbody2D.velocity;
        }
        if(useBulletTime && !m_added)
        {
            m_added = true;
            BulletTime.EnterBulletTime += Enter;
            BulletTime.LeaveBulletTime += Leave;
        }
    }

    private void Enter()
    {
        m_canmove = false;
        if(method==MoveMethod.Rigidbody2D && m_rigidbody2D!=null)
        {
            m_rigidbodySpeed = m_rigidbody2D.velocity;
            m_rigidbody2D.velocity = new Vector2(0,0);
        }
    }

    private void Leave()
    {
        m_canmove = true;
        if(method==MoveMethod.Rigidbody2D && m_rigidbody2D!=null)
        {
            m_rigidbody2D.velocity = m_rigidbodySpeed;
        }
    }

    private void Start() 
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        m_direction = new Vector3( Mathf.Cos(angle), Mathf.Sin(angle) );
        m_canmove = ShootInStart;

        if(method==MoveMethod.Transform)
            m_transform = transform;
        else if(method==MoveMethod.Rigidbody2D)
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            if(m_rigidbody2D==null) throw new System.NullReferenceException("This bullet no rigidbody2D, but method is Rigidbody2D!");
            if(ShootInStart)
            {
                m_rigidbody2D.velocity = new Vector2(m_direction.x,m_direction.y)*speed;
                m_rigidbodySpeed = m_rigidbody2D.velocity;
            }
        }
            
        if(useBulletTime &&  ShootInStart)
        {
            m_added = true;
            BulletTime.EnterBulletTime += Enter;
            BulletTime.LeaveBulletTime += Leave;
        }
    }

    private void Update() 
    {
        if(m_canmove && method==MoveMethod.Transform)
        {
            m_transform.position += m_direction*speed*Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(useBulletTime && BulletTime.isBulletTime || ignorePrefabName.Contains(other.gameObject.prefabName())) {return;}
        //print("hit");
        if(E_Hit!=null) { E_Hit(gameObject,other); }
        if(destroyWhenHit)
            GameObject.Destroy(gameObject);
    }

    private void OnDestroy() 
    {
        if(useBulletTime)
        {
            BulletTime.EnterBulletTime -= Enter;
            BulletTime.LeaveBulletTime -= Leave;
        }
    }
}