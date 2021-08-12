using UnityEngine;
using Utility;
public class SimpleBulletTimeSwitch : MonoBehaviour
{
    [System.Serializable]
    public class ParticleSystemBTController
    {
       public ParticleSystem particleSystem;
       [Tooltip("enabled: true进入播放离开停止生成/暂停, false相反")]
       public bool enabled;
       [Tooltip("true: 在停止运动 flase: 停止生成")]
       public bool isPause;
    }


    [Tooltip("bool: true进入子弹时间播放，离开暂停。false进入子弹时间暂停，离开播放")]
    public Pair<DG.Tweening.DOTweenAnimation,bool>[] tweens;

    [Tooltip("bool: true进入子弹时间生成，离开停止生成。false进入子弹时间停止生成，离开开始生成")]
    public ParticleSystemBTController[] fxs;

    [Tooltip("bool: true进入子弹时间启用，离开禁用。false进入子弹时间禁用，离开启用")]
    public Pair<Behaviour,bool>[] components;

    [Tooltip("bool: true进入子弹时间启用，离开禁用。false进入子弹时间禁用，离开启用")]
    public Pair<GameObject,bool>[] gameobjects;

    private void Enter()
    {
        foreach (var v in tweens)
        {
            if(v.second)
                v.first.DOPlay();
            else
                v.first.DOPause();
        }

        foreach (var v in fxs)
        {
            if(v.enabled)
                v.particleSystem.Play();
            else
            {
                if(v.isPause)
                    v.particleSystem.Pause();
                else
                    v.particleSystem.Stop();
            }
        }

        foreach (var v in components)
        {
            v.first.enabled = v.second;
        }

        foreach (var v in gameobjects)
        {
            v.first.SetActive(v.second);
        }

    }

    private void Leave()
    {
        foreach (var v in tweens)
        {
            if(v.second)
                v.first.DOPause();
            else
                v.first.DOPlay();
        }

        foreach (var v in fxs)
        {
            if(v.enabled)
            {
                if(v.isPause)
                    v.particleSystem.Pause();
                else
                    v.particleSystem.Stop();
            }
            else
                v.particleSystem.Play();
        }

        foreach (var v in components)
        {
            v.first.enabled = !v.second;
        }

        foreach (var v in gameobjects)
        {
            v.first.SetActive(!v.second);
        }
    }

    bool m_init = false;
    private void OnEnable()
    {
        if(!m_init)
        {
            m_init = true;
            BulletTime.EnterBulletTime += Enter;
            BulletTime.LeaveBulletTime += Leave;
        }
    }

    private void OnDisable()
    {
        if(m_init)
        {
            m_init = false;
            BulletTime.EnterBulletTime -= Enter;
            BulletTime.LeaveBulletTime -= Leave;
        }
    }

    private void OnDestroy()
    {
        if(m_init)
        {
            m_init = false;
            BulletTime.EnterBulletTime -= Enter;
            BulletTime.LeaveBulletTime -= Leave;
        }
    }
}
