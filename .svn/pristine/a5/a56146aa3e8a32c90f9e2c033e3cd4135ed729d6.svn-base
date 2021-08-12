using UnityEngine;
using Utility;

//用于状态机参数的Enter初始化，或Exit清理
public class ParamsCleaner : StateMachineBehaviour
{
    [Tooltip("是Exit时调用，还是Enter时调用")]
    public bool isExit = true;
    //一旦public，就不是null。不用担心循环出错
    public Pair<string,bool>[] bools;
    public Pair<string,float>[] floats;
    public Pair<string,int>[] ints;
    public string[] triggers;

    private void Clear(Animator animator)
    {
        foreach (var v in bools)
        {
            animator.SetBool(v.first,v.second);
        }

        foreach (var v in floats)
        {
            animator.SetFloat(v.first,v.second);
        }

        foreach (var v in ints)
        {
            animator.SetInteger(v.first,v.second);
        }

        foreach (var v in triggers)
        {
            animator.SetTrigger(v);
        }
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isExit) 
            Clear(animator);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(isExit)
            Clear(animator);
    }
}
