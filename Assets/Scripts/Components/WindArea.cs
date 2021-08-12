using System.Collections.Generic;
using UnityEngine;

//AreaEffector2D的扩充
[RequireComponent(typeof(AreaEffector2D))]
public class WindArea : MonoBehaviour
{
    [Header("水平移动速度加成(+)")]
    public float leftMoveSpeedExtern;
    public float rightMoveSpeedExtern;
    [Header("冲刺速度加成(+)")]
    public float leftRushSpeedExtern;
    public float rightRushSpeedExtern, upRushSpeedExtern, downRushSpeedExtern;
    [Header("区域力")]
    public float horizontalForce;
    public float verticalForce;
    [Tooltip("考虑到刚体的重力比例而设置的")]
    public float verticalScale = 1f;
    [Tooltip("阻尼，避免加速太快")]
    public float drag = 2f;
    public LayerMask layerMask;
    private AreaEffector2D m_effect;

    public bool useBulletTime = true;

    private List<Black> m_tagets = new List<Black>();

    void EnterBT()
    {
        for (int i = m_tagets.Count - 1; i >= 0 ; --i)
        {
            Black black = m_tagets[i];
            black.leftMoveSpeedExtern -= leftMoveSpeedExtern;
            black.rightMoveSpeedExtern -= rightMoveSpeedExtern;

            black.leftRushSpeedExtern -= leftRushSpeedExtern;
            black.rightRushSpeedExtern -= rightRushSpeedExtern;
            black.upRushSpeedExtern -= upRushSpeedExtern;
            black.downRushSpeedExtern -= downRushSpeedExtern;
            m_tagets.RemoveAt(i);
        }
    }


    void Start()
    {
        float vForce = verticalForce * verticalScale;
        m_effect = GetComponent<AreaEffector2D>();
        m_effect.drag = drag;
        m_effect.forceMagnitude = Mathf.Sqrt(horizontalForce*horizontalForce + vForce*vForce);
        m_effect.forceAngle = Mathf.Atan2(vForce,horizontalForce) * Mathf.Rad2Deg;
        m_effect.useColliderMask = true;
        m_effect.colliderMask = layerMask;

        if(useBulletTime) { BulletTime.EnterBulletTime += EnterBT; }

        bool ok = false;
        foreach (var v in GetComponents<Collider2D>())
        {
            if(v.isTrigger && v.usedByEffector)
            {
                ok = true;
                break;
            }
        }
        if(!ok) { Debug.LogWarning("WindArea component needs a trigger2D and it's used by effector."); }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(useBulletTime && BulletTime.isBulletTime) {return;}
        Black black = other.GetComponent<Black>();
        if(black!=null)
        {
            if(m_tagets.Contains(black)) {return;}
            m_tagets.Add(black);

            black.leftMoveSpeedExtern += leftMoveSpeedExtern;
            black.rightMoveSpeedExtern += rightMoveSpeedExtern;

            black.leftRushSpeedExtern += leftRushSpeedExtern;
            black.rightRushSpeedExtern += rightRushSpeedExtern;
            black.upRushSpeedExtern += upRushSpeedExtern;
            black.downRushSpeedExtern += downRushSpeedExtern;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(useBulletTime && BulletTime.isBulletTime) {return;}
        Black black = other.GetComponent<Black>();
        if(black==null || !m_tagets.Contains(black)) {return;}
        m_tagets.Remove(black);
        black.leftMoveSpeedExtern -= leftMoveSpeedExtern;
        black.rightMoveSpeedExtern -= rightMoveSpeedExtern;

        black.leftRushSpeedExtern -= leftRushSpeedExtern;
        black.rightRushSpeedExtern -= rightRushSpeedExtern;
        black.upRushSpeedExtern -= upRushSpeedExtern;
        black.downRushSpeedExtern -= downRushSpeedExtern;
    }
}
