using UnityEngine;
public class BoxCheck : MonoBehaviour
{
    public Rigidbody2D body;
    void Enter()
    {
        if(body==null){return;}
        body.bodyType = RigidbodyType2D.Static;
    }

    void Leave()
    {
        if(body==null){return;}
        body.bodyType = RigidbodyType2D.Dynamic;
    }

    void Start()
    {
        if(body!=null)
        {
            body.bodyType = RigidbodyType2D.Dynamic;
            BulletTime.EnterBulletTime += Enter;
            BulletTime.LeaveBulletTime += Leave;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.HasTag(STRING.Tag.Player))
        {
            other.gameObject.AddTag(STRING.LightTag.InBox);
            other.GetComponent<BlackAnim>()?.ChangeAllLayer(STRING.SortLayer.Backobject,true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.HasTag(STRING.Tag.Player))
        {
            other.gameObject.RemoveTag(STRING.LightTag.InBox);
            other.GetComponent<BlackAnim>()?.ChangeAllLayer(STRING.SortLayer.Default,true);
        }
    }
}
