using UnityEngine;
public class Health : MonoBehaviour
{
    [Min(0f)]
    public float maxHealth = 1f;
    [Min(0f)]
    public float currentHealth = 1f;
    [Min(0f),Tooltip("如果God Time大于0，物体在受伤后会有GodTime秒的无敌时间")]
    public float godTime = 0f;
    [Tooltip("是否启动上帝模式，如果God Time大于0，则只持续God Time秒")]
    public bool isGod = false;
    public bool isDead{
        get {return currentHealth <= 0;}
    }

    private float m_time = 0f;

    public class DeltaData{
        public GameObject source;
        public float delta;
        public DeltaData(float delta=0f,GameObject source=null){this.delta=delta; this.source=source;}
    }

    public event Utility.Closure.FnType E_Die;
    public event Utility.Closure.FnType E_ReSpawn;
    public event Utility.Closure<DeltaData>.FnType E_Attacked;
    public event Utility.Closure<DeltaData>.FnType E_Treated;  //被治疗


    public void Delta(float delta,GameObject source=null)
    {
        //上帝模式无法delta，死人无法再扣血
        if( isGod || (isDead&&delta<0f) ) {return;}

        currentHealth = Mathf.Clamp(currentHealth+delta,0f,maxHealth);
        if(delta>0f)
        {
            if(E_Treated!=null) { E_Treated(gameObject,new DeltaData(delta,source)); }
        }
        else if(delta<0f)
        {
            if(godTime>0f)
            {
                m_time = godTime;
                isGod = true; //启动Updata计时
            }
            if(E_Attacked!=null) { E_Attacked(gameObject,new DeltaData(delta,source)); }
            if(isDead && E_Die!=null) { E_Die(gameObject); }
        }
    }
    public void ReSpawn()
    {
        currentHealth = maxHealth;
        if(E_ReSpawn!=null) E_ReSpawn(gameObject);
    }

    void Update()
    {
        if(isGod)
        {
            m_time -= Time.deltaTime;
            if(m_time < 0f)
            {
                isGod = false;
            }
        }
    }

}
