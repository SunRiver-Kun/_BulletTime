using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("门")]
    [Tooltip("门的预制体")]
    public Transform door;
    [Min(0f),Tooltip("门移动的距离")]
    public float doorMoveDistance=3f;
    [Min(0f),Tooltip("门移动的速度")]
    public float doorMoveSpeed=0.05f;
    [Header("按钮")]
    public Transform switchButton;
    [Tooltip("会触发开关的物体名")]
    public List<string> prefabNames = new List<string>(){"Black","Door"};
    Vector3 doorStartPosition;
    Vector3 doorEndPosition;
    Vector3 switchButtonStartPosition;
    Vector3 switchButtonEndPosition;
    bool isMoveUp;
    bool isEnterBulletTime;

    private void Awake() 
    {
        if(!door)
        {
            Debug.LogError("Door Is Empty");
        }
        doorStartPosition=door.transform.position;
        doorEndPosition=doorStartPosition+door.transform.up*doorMoveDistance;
        if(!switchButton)
        {
            Debug.LogError("SwitchButtin Is Empty");
        }
        switchButtonStartPosition=switchButton.position;
        switchButtonEndPosition=switchButton.position-new Vector3(0f,0.1f,0f);

    }
    private void FixedUpdate() 
    {
        if(isMoveUp)
        {
            if(Vector3.Distance(door.transform.position,doorEndPosition)>0.1&&!isEnterBulletTime)
            {
                door.transform.position += door.transform.up*doorMoveSpeed;
            }
                

        }else
        {
            if(Vector3.Distance(door.transform.position,doorStartPosition)>0.1&&!isEnterBulletTime)
            {
                door.transform.position -= door.transform.up*doorMoveSpeed;
            }
                
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(prefabNames.Contains(other.gameObject.prefabName()))
        {
            isMoveUp = true;
            switchButton.position = switchButtonEndPosition;
            AudioManager.PlayAudio("道具-门-开关-按下");
            
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(prefabNames.Contains(other.gameObject.prefabName()))
        {
            isMoveUp = false;
            switchButton.position = switchButtonStartPosition;
            AudioManager.PlayAudio("道具-门-开关-松开");
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(doorStartPosition,0.1f);
        Gizmos.DrawSphere(doorEndPosition,0.1f);
    }
    void Enter()
    {
        isEnterBulletTime=true;
    }
    void Leave()
    {
        isEnterBulletTime=false;
    }
    private void OnEnable() 
    {
        BulletTime.EnterBulletTime += Enter;
        BulletTime.LeaveBulletTime += Leave;
    }

    private void OnDisable() 
    {
        BulletTime.EnterBulletTime -= Enter;
        BulletTime.LeaveBulletTime -= Leave;
    }
}
