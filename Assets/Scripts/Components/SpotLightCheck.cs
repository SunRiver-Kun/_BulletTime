using UnityEngine;

public class SpotLightCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(BulletTime.isBulletTime){return;}
        else
        {
            if(other.gameObject.HasTag(STRING.Tag.Player) && ! other.gameObject.HasTag(STRING.LightTag.InBox))
            {
                Health health = other.gameObject.GetComponent<Health>();
                if(health != null)
                {
                    health.Delta(-health.maxHealth,gameObject);
                }
            }
        }
    }
}
