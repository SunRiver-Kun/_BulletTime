using System.Collections.Generic;
using UnityEngine;
using Utility;

//定时器
public class Timer : GlobalComponent<Timer>
{
    public class TimerData
    {
        public Closure closure;
        public float time;  //延迟时间
        public bool isUsed{
            get {return time<=0;}
        }
        public TimerData(float time,Closure closure)
        {
            this.time = time;
            this.closure = closure;
        }
    }

    public class PeriodData
    {
        public Closure closure;
        public float delaytime;
        public float time;
        public bool isEnd{
            get{return num==0;}
        }
        public int num;
        public PeriodData(float periodtime,float delaytime,Closure closure)
        {
            this.delaytime = delaytime;
            this.time = delaytime;
            this.num = (int)(periodtime/delaytime);
            this.closure = closure;
        }
        public void Cancel() {num = 0;}
    }

    //一次性定时任务
    public static void DoTaskInTime(float time,Closure closure)
    {
        TimerData timerdate = new TimerData(time,closure);
        m_time.Add(timerdate);
    }
    
    public static void DoTaskInTime(float time,Closure.FnType fn,GameObject inst)
    {
        DoTaskInTime(time,new Closure(inst,fn));
    }

    public static void DoTaskInTime<Value>(float time,Closure<Value>.FnType fn,GameObject inst,Value data)
    {
        DoTaskInTime(time,new Closure<Value>(inst,fn,data));
    }

    //阶段性定时任务，periodtime 总时长，delaytime 间隔。
    //timeline:  0  delaytime(调用)  2*delaytime(调用) ...  periodtime(超过结束任务)
    public static PeriodData DoPeriodTask(float periodtime,float delaytime,Closure closure)
    {
        PeriodData perioddata = new PeriodData(periodtime,delaytime,closure);
        m_periodtime.Add(perioddata);
        return perioddata;
    }

    public static PeriodData DoPeriodTask(float periodtime,float delaytime,Closure.FnType fn,GameObject inst)
    {
        return DoPeriodTask(periodtime,delaytime,new Closure(inst,fn));
    }

    public static PeriodData DoPeriodTask<Value>(float periodtime,float delaytime,Closure<Value>.FnType fn,GameObject inst,Value data)
    {
        return DoPeriodTask(periodtime,delaytime,new Closure<Value>(inst,fn,data));
    }

    static List<TimerData> m_time = new List<TimerData>();
    static List<PeriodData> m_periodtime = new List<PeriodData>();

    //List 循环删除需要倒叙
    void Update()
    {
         float dt = Time.deltaTime;
         for (int i = m_periodtime.Count-1; i >= 0; --i)
         {
            PeriodData data = m_periodtime[i];
            if(data.num<=0)
            {
                m_periodtime.RemoveAt(i);
            }
            else
            {
                data.time -= dt;
                if(data.time <= 0f)
                {
                    try{data.closure.Call();}  catch(System.Exception){}
                    data.time = data.delaytime;
                    --data.num;
                }
            }
         }
    }
    void LateUpdate()
    {
        float dt = Time.deltaTime;
        for(int i = m_time.Count-1 ; i >= 0; --i)
        {
            TimerData data = m_time[i];
            if(data.time > 0f)
            {
                data.time -= dt;
            }
           else
           {
               try{ data.closure.Call(); } catch(System.Exception){}
               m_time.RemoveAt(i);
           }
        }
    }

    public override void OnDestroy()
    {
        m_time?.Clear();
        m_periodtime?.Clear();
        base.OnDestroy();
    }
}
