using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public BulletCommon bullet;
    public GameObject continueButton;
    public Camera targetCamera;
    [Min(0.1f)]
    public float delayTime = 3f;
    public float sizeTo = 5f;
    private bool isStart;
    private float originsize;

    private float m_time = 0f;
    public void StartGame()
    {
        isStart = true;    
    }

    public void ContinueGame()
    {
        GLOBAL.instance.LoadSceneImmediately(SceneData.sceneIndex);
    }

    void Start() 
    {
        if(continueButton!=null) { continueButton.SetActive(!SceneData.isFirstPlay); }
        originsize = targetCamera.orthographicSize;
    }

    void Update()
    {
        if(isStart)
        {
            m_time = Mathf.Clamp01(m_time + Time.deltaTime/delayTime);
            targetCamera.orthographicSize = Mathf.Lerp(originsize,sizeTo,m_time);
            if(m_time==1f)
            {
                isStart = false;
                m_time = 0f;
                bullet.Shoot();
            }
        }
    }
}
