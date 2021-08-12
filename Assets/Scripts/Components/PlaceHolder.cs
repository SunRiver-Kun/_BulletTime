using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    [Tooltip("占位物体，从预设物那里拖过来")]
    public GameObject placeHolder;
    [Min(0.01f),Tooltip("占位时间")]
    public float holdTime = 3f;
    [Tooltip("父类物体，为空则物体初始位置为挂载物体的位置和角度+下面相对的数值")]
    public Transform parent;
    [Tooltip("相对位置")]
    public Vector3 position;
    [Tooltip("相对角度(°)")]
    public Vector3 euler;

    public bool isHolding{get; private set;}
    public event Utility.Closure.FnType E_PlaceBegin,E_PlaceEnd;

    private float m_time;
    private Quaternion m_qt;
    public void StartPlace()
    {
        isHolding = true;
        if(parent!=null) 
        { 
            placeHolder.transform.position = parent.transform.position + position;
            placeHolder.transform.rotation = parent.transform.rotation;
            placeHolder.transform.rotation *= m_qt;     //相当于 ∠1 + ∠2
        }
        placeHolder.SetActive(true);
        if(E_PlaceBegin!=null) { E_PlaceBegin(gameObject); }
    }

    //时间到也会自己停止的
    public void StopPlace()
    {
        isHolding = false;
        m_time = 0f;
        placeHolder.SetActive(false);
                
        if(E_PlaceEnd!=null) { E_PlaceEnd(gameObject); }
    }

    void Start()
    {
        if(placeHolder==null)
        {
            Debug.LogWarning("PlaceHolder component: Place Holder is null");
        }
        else 
        {   
            m_qt = Quaternion.Euler(euler);
            placeHolder = GameObject.Instantiate(placeHolder, transform.position+position, transform.rotation*m_qt);
            placeHolder.SetActive(false);
        }
    }

    void Update() 
    {
        if(isHolding)
        {
            m_time += Time.deltaTime;
            if(m_time > holdTime)
            {
                StopPlace();
            }
        }
    }
}
