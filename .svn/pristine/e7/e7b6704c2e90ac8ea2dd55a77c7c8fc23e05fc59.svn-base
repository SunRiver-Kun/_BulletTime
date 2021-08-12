using System;
using UnityEngine;

public class SoildWood : MonoBehaviour
{
    [Min(0f),Tooltip("转动角度")]
    public float WoodRotationAngle = 10;
    [Min(0f),Tooltip("站立时间")]
    public float StandTime = 1f;
    [Min(0f),Tooltip("复位时间")]
    public float ResetTime = 5f;
    public DG.Tweening.DOTweenAnimation dotween;
    float m_Timer = 0f;
    bool Touch = false; // 接触
    bool ResetCd = false; // 是否为待复位状态
    float my_ResetTimer = 0f;
    BoxCollider2D[] boxcollider;
    Rigidbody2D rgbody;
    Vector3 size;
    Vector3 InitPosition; // 记录初始位置
    PlaceHolder m_holder;
    void Start()
    {
        size = transform.GetComponent<Renderer>().bounds.size;
        InitPosition = transform.position;
        boxcollider = gameObject.GetComponents<BoxCollider2D>();
        rgbody = gameObject.GetComponent<Rigidbody2D>();
        m_holder = GetComponent<PlaceHolder>();
        if(m_holder!=null) { m_holder.holdTime = ResetTime; }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.HasTag(STRING.Tag.Player) && !ResetCd)
        {
            Touch = true;
            if(other.transform.position.y > transform.position.y) // 保证玩家碰撞时是在木板上而不是下面
            {
                float distanceX = other.transform.position.x - transform.position.x;
                if(distanceX < 0 && Math.Abs(distanceX) > size.x/6)
                {
                    ChangeRotation(-1);
                }
                else if(distanceX > 0 && Math.Abs(distanceX) > size.x/6)
                {
                    ChangeRotation(1);
                }
                else
                {
                    ChangeRotation(0);
                }
            }
        }
    }
    // private void OnTriggerExit2D(Collider2D other) {
    //     if(other.gameObject.HasTag(STRING.Tag.Player))
    //     {
    //         Touch = false;
    //         m_Timer = 0f;
    //         ChangeRotation(0);
    //     }
    // }
    private void ChangeRotation(int scale) // -1代表在左边，0代表在中间，1代表在右边
    {
        if(!BulletTime.isBulletTime) // 子弹时间时不移动
            transform.localRotation = Quaternion.Euler(0,0,scale*(-WoodRotationAngle));
    }
    private void ChangeWoodState(int x = 0)
    {
        // 持续站立一秒后
        if(x == 0)
        {
            ResetCd = true;
            boxcollider[0].isTrigger = true;
            dotween.DOPause();
            rgbody.bodyType = RigidbodyType2D.Dynamic;
            m_holder?.StartPlace();
        }
        // 复位
        else
        {
            ResetCd = false;
            my_ResetTimer = 0f;
            transform.position = InitPosition;
            rgbody.bodyType = RigidbodyType2D.Kinematic;
            boxcollider[0].isTrigger = false;
            rgbody.velocity = Vector2.zero;
            dotween.DORestart(false);
            m_holder?.StopPlace();
        }

    }
    private void Update() {
        if(Touch)
        {
            if(!BulletTime.isBulletTime)m_Timer += Time.deltaTime;
            if(m_Timer >= StandTime)
            {
                m_Timer = 0f;
                Touch = false;
                ChangeRotation(0);
                ChangeWoodState(0);
            }
        }
        else if(ResetCd)
        {
            if(!BulletTime.isBulletTime)my_ResetTimer += Time.deltaTime;
            if(my_ResetTimer >= ResetTime)
            {
                ChangeWoodState(1);
            }
        }
        if(BulletTime.isBulletTime)
        {
            if(ResetCd) // 子弹时间且处于待复位状态,恢复可站立
            {
                boxcollider[0].isTrigger = false;
                rgbody.bodyType = RigidbodyType2D.Kinematic;
                rgbody.velocity = Vector2.zero;
            }
        }
        else if(ResetCd)
        {
            dotween.DOPause(); // 子弹时间结束后在其他脚本会重新打开，因此需要关闭
            boxcollider[0].isTrigger = true;  // 取消站立
            rgbody.bodyType = RigidbodyType2D.Dynamic;  // 继续下落
        }
    }
}
