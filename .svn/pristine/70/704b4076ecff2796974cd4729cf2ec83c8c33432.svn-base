using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [Tooltip("BulidSetting里的index")]
    public int index;
    [Tooltip("自动跳到下一关")]
    public bool autoIndex = true;
    [Tooltip("是否到触发器自动切换场景")]
    public bool autoChangeScene = true;
    [Tooltip("是否马上切换场景")]
    public bool isImmediately = false;
    public void GoToNextScene()
    {
        if(index!=0){SceneData.StoreSceneInfo(index);}
        if(isImmediately)
        {
            GLOBAL.instance.LoadSceneImmediately(index);
        }
        else 
        {
            GLOBAL.instance.LoadScene(index);
        }
    }

    void Start()
    {
        if(autoIndex)
        {
            var scene = SceneManager.GetActiveScene();
            if(scene!=null)
            {
                index = scene.buildIndex + 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.HasTag("Player"))
        {
            if(autoChangeScene)
            {
                GoToNextScene();
            }
        }
    }
}
