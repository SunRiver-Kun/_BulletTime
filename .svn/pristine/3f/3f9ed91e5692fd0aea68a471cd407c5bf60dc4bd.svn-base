using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Image background;
    public Color color;
    public int index = 0;
    public bool autoIndex = true;
    private bool m_isrun = false;

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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(background!=null && !m_isrun)
        {
            m_isrun = true;
            background.color = color;
            GLOBAL.instance.LoadScene(index);
        }
    }
}
