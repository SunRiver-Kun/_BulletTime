
//字符串表
namespace STRING
{
    //Shift + Alt + ↓，复制当前行，并粘贴到下一行
    //public const string
    //public static readonly string
    public static class Tag{
        public const string Untagged = "Untagged";
        public const string Player = "Player";
        public const string MainCamera = "MainCamera";
        public const string Menu = "Menu";
    }

    //component
    public static class LightTag{
        public const string InBox = "Light_Box";
    }


    public static class Layer{  //int LayerMask.GetMask(params string[] layerNames)
        public const string Default = "Default";
        public const string TransparentFX = "TransparentFX";
        public const string IgnoreRaycast = "Ignore Raycast";  //有给空格
        public const string Player = "Player";
        public const string Water = "Water";
        public const string UI = "UI";
        public const string Ground = "Ground";
    }

    public static class SortLayer{
        //与SortLayer那里一样的顺序，越往下越前面
        public const string Background = "Background";
        public const string Backobject = "Backobject";
        public const string Default = "Default";
        public const string Foreobject = "Foreobject";
        public const string Foreground = "Foreground";
    }

    public static class Prefab{
        public const string Black = "Black";
    }

    public static class Path{
        public const string InputData = "local/data1.dat";
        public const string MusicData = "local/data2.dat";
        public const string SceneData = "local/data3.dat";

    }
}
