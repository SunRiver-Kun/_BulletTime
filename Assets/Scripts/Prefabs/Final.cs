using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public new AudioSource audio;
    private bool run = false;
    void Awake()
    {
        // var objects = FindObjectsOfType<BulletCommon>();
        // foreach (var v in objects)
        // {
        //     v.ShootInStart = false;
        // }
        if(audio!=null)
        {
            audio.playOnAwake = false;
            audio.Stop();
        }
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(run) { return; }
        if(INPUT.horizontal.value!=0f || INPUT.jump.value)
        {
            run = true;
            if(audio!=null) { audio.Play(); }
            // foreach (var v in FindObjectsOfType<BulletCommon>())
            // {
            //     v.Shoot();
            // }
             Time.timeScale = 1f;
        }
    }
}
