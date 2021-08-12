using UnityEngine;

//常量表
namespace CONSTANT
{   //C# enum无法隐形转换为int。
    //Shift + Alt + ↓，复制当前行，并粘贴到下一行
    //public const int|float
    //public static readonly int|float
    public static class Layer
    {   //Layer的数值与Layer层的数量和位置有关，不过在运行后会固定。所以不能直接xxx=0
        public static readonly int Default = LayerMask.GetMask("Default");   //1
        public static readonly int TransparentFX = LayerMask.GetMask("TransparentFX"); //2
        public static readonly int IgnoreRaycast = LayerMask.GetMask("Ignore Raycast");  //4
        public static readonly int Water = LayerMask.GetMask("Water");   //从这里开始不能直接xxx=0
        public static readonly int UI = LayerMask.GetMask("UI");
    }

    public static class SortingOrder{
        //Backobject
        public const int Box = 10000;
        public const int SpotLight = 10001;
    }
}
