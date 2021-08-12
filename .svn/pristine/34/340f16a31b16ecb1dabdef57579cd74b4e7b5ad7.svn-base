using UnityEngine;
using Utility;

public class NumberShower : MonoBehaviour
{
    public SpriteRenderer render;
    [Tooltip("按1~n开始排列的图")]
    public Pair<Sprite,Color>[] bulletTimeImages;
    public Pair<Sprite,Color>[] cdImages;

    void Start()
    {
        if(render==null){ throw new System.NullReferenceException("NumberShower component need a sprite render"); }
    }

    void Update() 
    {
        if(BulletTime.isBulletTime)
        {
            if(bulletTimeImages.Length==0) {return;}
            var image = bulletTimeImages[Mathf.FloorToInt(BulletTime.leftBulletTime)%bulletTimeImages.Length];
            render.sprite = image.first;
            render.color = image.second;
        }
        else if(BulletTime.leftCDTime > 0f)
        {
            if(cdImages.Length==0) {return;}
            var image = cdImages[Mathf.FloorToInt(BulletTime.leftCDTime)%bulletTimeImages.Length];
            render.sprite = image.first;
            render.color = image.second;
        }
        else 
        {
            render.sprite = null;
        }
    }

}
