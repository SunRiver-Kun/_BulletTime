using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ice : MonoBehaviour
{
    public bool isBroken{get; private set;}
    Animator m_anim;
    PlaceHolder m_holder;

    public void Trigger(bool isBroken)
    {
        this.isBroken = isBroken;
        m_anim.SetBool("isBroken",isBroken);
    }

    public void Trigger()
    {
        isBroken = !isBroken;
        m_anim.SetBool("isBroken",isBroken);
    }

    void Start() 
    {
        m_anim = GetComponent<Animator>();
        m_holder = GetComponent<PlaceHolder>();
        
        Trigger(isBroken);
        if(m_holder!=null)
        {
            m_holder.E_PlaceEnd += (GameObject inst)=>{ Trigger(false); isBroken = false; };
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(isBroken) { return; }
        if(other.gameObject.HasTag(STRING.Tag.Player))
        {
            if(BulletTime.isBulletTime)
            {
                BulletTime.ResetSkill();
                BulletTime.instance.StartBulletTime(true);
            }
            else
            {
                Black black = other.gameObject.GetComponent<Black>();
                if(black != null)
                {
                    black.ResetRushCount();
                }
            }
            Trigger(true);
            if(m_holder!=null) { m_holder.StartPlace(); }
        }
    }
}
