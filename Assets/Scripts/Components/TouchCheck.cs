using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public GameObject Object;
    public enum TouchMethod{
        Trigger, Collision
    }
    public TouchMethod method;
    [Tooltip("物体是否已失活")]
    public bool active;
    [Tooltip("是否有接触到")]
    public bool isTouch; // 接触到
    [Tooltip("当接触到时是否需要失活")]
    public bool isDeadWhenTouch = true;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.prefabName() == Object.prefabName() && method == TouchMethod.Trigger)
        {
            isTouch = true;
            if(isDeadWhenTouch)
            {
                Object.SetActive(false);
                active = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.prefabName() == Object.prefabName() && method == TouchMethod.Collision)
        {
            isTouch = true;
            if(isDeadWhenTouch)
            {
                Object.SetActive(false);
                active = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.prefabName() == Object.prefabName())
        {
            isTouch = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.prefabName() == Object.prefabName())
        {
            isTouch = false;
        }
    }
    private void Start() {
        if(Object == null)
        {
            Debug.LogError("TouchCheck.cs is missing input object.");
        }
        active = Object.activeSelf;
    }

}
