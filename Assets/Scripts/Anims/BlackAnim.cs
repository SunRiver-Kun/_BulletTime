using UnityEngine;
using Utility;

//管理所有的Black动画，并提供相应接口。
[RequireComponent(typeof(Animator))]
public class BlackAnim : MonoBehaviour
{
    public Animator anim { get; private set; }

    public bool isGrounded{     //这个请在Black脚本里修改
        get { return anim.GetBool("isGrounded"); }
        set { anim.SetBool("isGrounded",value); }
    }

    public void Move(bool move = true)
    {
        anim.SetBool("Run", move);
    }

    public void Rush(bool rush = true)
    {
        anim.SetBool("Rush",rush);
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }

    public void ChangeAllColor(Color color,bool ignoreeyes = true)
    {   //参数直接用，Color.black; Color.white;
        var renders = GetComponentsInChildren<SpriteRenderer>(true);
        for(int i=0; i<renders.Length; ++i)
        {
            if(ignoreeyes)
            {
                string name = renders[i].gameObject.name;
                if(name.EndsWith("eye")) {continue;}
            }
            renders[i].color = color;
        }
    }

    public void ChangeAllLayer(string sortingLayerName,bool ignoreeyes = true)
    {
        var renders = GetComponentsInChildren<SpriteRenderer>(true);
        for(int i=0; i<renders.Length; ++i)
        {
            if(ignoreeyes)
            {
                string name = renders[i].gameObject.name;
                if(name.EndsWith("eye")) {continue;}
            }
            renders[i].sortingLayerName = sortingLayerName;
        }
    }


    void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
