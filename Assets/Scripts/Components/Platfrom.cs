using UnityEngine;

public class Platfrom : MonoBehaviour
{
    public DG.Tweening.DOTweenAnimation dotween;
    [Tooltip("需要给空物体来平均父物体的Scale")]
    public Transform child;
    
    Transform playerFather;
    void Start()
    {
        dotween = dotween ?? GetComponent<DG.Tweening.DOTweenAnimation>();  //等价于 dotween = ( dotween!=null ? dotween : ...)
        if(dotween==null) {throw new System.NullReferenceException("Platform component needs to assign dotween.");}
        child = child ?? transform;
        child.localScale = new Vector3(1f/transform.localScale.x, 1f/transform.localScale.y, 1f/transform.localScale.z);

        bool ok = false;
        foreach(var collider in GetComponents<Collider2D>() )
        {
            if(collider.isTrigger)
            {
                ok = true;
                break;
            }
        }
        if(!ok) { Debug.LogWarning("Platfrom component needs a trigger"); }

        BulletTime.EnterBulletTime += ()=>{
            dotween.DOPause();
        };
        BulletTime.LeaveBulletTime += ()=>{
            dotween.DOPlay();
        };
    }
   private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(STRING.Tag.Player))
        {
            other.transform.SetParent(child);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag(STRING.Tag.Player))
        {
            other.transform.SetParent(null);
        }
    }
}
