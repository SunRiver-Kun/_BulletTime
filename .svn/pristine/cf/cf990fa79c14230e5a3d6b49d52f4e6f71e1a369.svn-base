using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public int CurrentScale;
    public float RotationTime = 0.2f;
    float m_RotationTimer = 0f;
    public Transform Second;
    public bool Rotation = false;
    private void Start() {
        CurrentScale = 0;
    }
    public void RotationControl(float Scale)
    {
        Second.localRotation = Quaternion.Euler(0,0,-90+Scale * (-120));
    }
    // 仅在子弹时间转动，每过1s后花0.2s转动1/3圈，如果期间使用冰刷新时间，可能会转不满整数秒，这时向下取整
    private void Update() {
        if(BulletTime.isBulletTime)
        {
            if(Math.Abs((BulletTime.leftBulletTime-Time.deltaTime) % 1) < Time.deltaTime)
                Rotation = true;
        }
        if(Rotation)
        {
            m_RotationTimer += Time.deltaTime;
            RotationControl(CurrentScale+m_RotationTimer/RotationTime);
            if(m_RotationTimer >= RotationTime)
            {
                CurrentScale = (CurrentScale + 1)%3;
                m_RotationTimer = 0f;
                Rotation = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            BulletTime.instance.StartBulletTime();
        }
    }
}
