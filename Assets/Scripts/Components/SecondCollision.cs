using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用于处理秒针碰撞人的逻辑
public class SecondCollision : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.HasTag(STRING.Tag.Player))
        {
            Black black = other.gameObject.GetComponent<Black>();
            Clock clock = gameObject.GetComponentInParent<Clock>();
            if(black != null && clock != null)
            {
                if(clock.Rotation)
                    black.ClockCollision(clock.CurrentScale);
            }
        }
    }
}
