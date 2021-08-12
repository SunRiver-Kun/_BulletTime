using System.Collections.Generic;
using UnityEngine;

public class CollisionFx : MonoBehaviour
{
    public enum PlayState{Enter,Leave}
    public GameObject fx;
    public List<PlayState> state = new List<PlayState>(){PlayState.Enter,PlayState.Leave};
    public List<string> targetPrefab = new List<string>(){STRING.Prefab.Black};
    public List<Vector2> directions = new List<Vector2>(){Vector2.up};

    private ContactPoint2D m_leaveData;

    private bool IsDirecion(Vector2 normal)
    {
        normal = -normal;
        return directions.Contains(normal);
    }

    void Start()
    {
        foreach (var v in GetComponents<Collider2D>())
        {
            if(!v.isTrigger) { return; }            
        }
        Debug.LogWarning("Collision Fx component needs a collider2D");
        for (int i = 0; i < directions.Count; ++i)
        {
            directions[i].Normalize();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(fx==null || !state.Contains(PlayState.Enter)) { return; }
        var contact = other.contacts[0];
        if(targetPrefab.Contains(other.gameObject.prefabName()) && IsDirecion(contact.normal) )
        {
            GameObject.Instantiate(fx,contact.point,Quaternion.FromToRotation(Vector3.up,-contact.normal));
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(state.Contains(PlayState.Leave))
        {
            m_leaveData = other.contacts[0];
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(fx==null || !state.Contains(PlayState.Leave)) { return; }
        if(targetPrefab.Contains(other.gameObject.prefabName()) && IsDirecion(m_leaveData.normal) )
        {
            GameObject.Instantiate(fx,m_leaveData.point,Quaternion.FromToRotation(Vector3.up,-m_leaveData.normal));
        }
    }

}
