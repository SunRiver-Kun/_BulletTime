using System;
using UnityEngine;

//注意：GameObject.Destroy()  并不会自动剔除加入的event的函数。
//所以要记得在 OnDestroy() 中 -= 剔除加入的函数
public class BulletTime : GlobalComponent<BulletTime>
{
    // [Min(0f),Tooltip("每间隔多少秒进入子弹时间")]
    // public float delayTime = 3f;
    [Min(0.1f),Tooltip("子弹时间为多少秒")]
    public float bulletTime = 3f;
    [Min(0.1f),Tooltip("技能冷却时间")]
    public float cd = 3f;
    public static bool isBulletTime { get { return leftBulletTime > 0f;} }
    public static bool canUseSkill {get { return leftCDTime <= 0f; } }
    
    //[0, ?]
    public static float leftBulletTime {get; private set;}
    //[0, cd]
    public static float leftCDTime {get; private set;}
    
    public delegate void EmptyFn();
    
    public static event EmptyFn EnterBulletTime,LeaveBulletTime,CDEnd,ReSetBulletTime,AddBulletTime;

    public static void EndBulletTime()
    {
        if(!isBulletTime) { return; };
        leftBulletTime = 0f;
        LeaveBulletTime?.Invoke();
        print("End Bullet Time");
    }
    //获得冰块时，调用此函数，可再次使用技能
    public static void ResetSkill()
    {
        leftCDTime = 0f;
        CDEnd?.Invoke();
        print("Reset Skill CD");
    }

    //只有CD到了才会调用成功，下面是使用方法,在其他脚本中输入
    //BulletTime.instance.StartBulletTime();
    public void StartBulletTime(bool reset = true)
    {
        if(canUseSkill)
        {
            leftCDTime = cd;
            if(isBulletTime)
            {
                leftBulletTime = reset ? bulletTime : leftBulletTime+bulletTime;
                print((reset ? "Reset" : "Add") + " BulletTime. Now BulletTime is "+leftBulletTime);
                if(reset) { ReSetBulletTime?.Invoke(); }
                else { AddBulletTime?.Invoke(); }
            }
            else
            {
                leftBulletTime = bulletTime;
                if(EnterBulletTime!=null) EnterBulletTime();
                print("Enter BulletTime");
            }
        }
    }

    void Update()
    {
        if(isBulletTime)
        {   //子弹时间定时取消
            leftBulletTime = Mathf.Max(leftBulletTime-Time.deltaTime,0f);
            if(leftBulletTime == 0f )
            {
                if(LeaveBulletTime!=null) LeaveBulletTime();
                print("Leave BulletTime");
            }
        }
        else if(!canUseSkill)   // if(!isBulletTime && !canUseSkill)
        {   //非子弹时间，回复cd
            leftCDTime = Mathf.Max(leftCDTime-Time.deltaTime,0f);
            if(leftCDTime == 0f )
            {
                if(CDEnd!=null) CDEnd();
                print("BulletTime CD end");
            }
        }
    }

    public override void OnDestroy() 
    {
        leftBulletTime = leftCDTime = 0f;
        EnterBulletTime = LeaveBulletTime = CDEnd = ReSetBulletTime = AddBulletTime = null;
        base.OnDestroy();
    }
}
