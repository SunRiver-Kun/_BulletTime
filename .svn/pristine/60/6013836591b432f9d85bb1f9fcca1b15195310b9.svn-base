using System.Security.Cryptography.X509Certificates;
using System.Text;
using System;
using System.IO;
using UnityEngine;
public class INPUT : Setting<INPUT>
{
    public enum KeyType{ Down,Up,All,None }

    [System.Serializable]
    public class AxisData
    {
        [Tooltip("轴的名字")]
        public string name;
        // [Tooltip("轴的描述")]
        // public string nameDes;
        [Tooltip("正键，按下轴值增加")]
        public KeyCode positiveKey;
        // [Tooltip("正键的描述")]
        // public string positiveDes;
        [Tooltip("反键，按下轴值减少")]
        public KeyCode negativeKey;
        // [Tooltip("反键的描述")]
        // public string negativeDes;
        [Min(0f),Tooltip("不按键时值回复到0的速率")]
        public float gravity;
        [Min(0f),Tooltip("按键时值增加的速率")]
        public float sensitivity;
        [Tooltip("是否按下反向键时先把值设为0")]
        public bool snap;
        [Tooltip("当前值")]
        public float value;
        //正反键是否反向
        public bool invert{
            get { return m_invert; }
            set { 
                if(m_invert!=value){
                    m_invert = value;
                    KeyCode temp = positiveKey;  
                    positiveKey = negativeKey; 
                    negativeKey = temp; 
                }
            }
        }
        private bool m_invert;

        public static implicit operator float(AxisData data) { return data.value; }

        public AxisData(string name,KeyCode positiveKey, KeyCode negativeKey = KeyCode.None)
        {
            this.name = name;
            this.positiveKey = positiveKey;
            this.negativeKey = negativeKey;
            this.gravity = 0.1f;
            this.sensitivity = 0.2f;
        }

        public void Update()
        {
            if(Input.GetKey(positiveKey))
            {
                if(snap && value<0f) { value = 0f; }
                value = Mathf.Clamp01(value+sensitivity);
            }
            else if(Input.GetKey(negativeKey))
            {
                if(snap  && value > 0f) { value = 0f; }
                value = Mathf.Clamp(value-sensitivity,-1f,0f);
            }
            else 
            {
                if(value < 0f) 
                { 
                    value = Mathf.Clamp(value+gravity,-1f,0f);
                }
                else if(value > 0f)
                {
                    value = Mathf.Clamp01(value-gravity);
                }
            }
        }

        public void Load(BinaryReader reader)
        {
            if(reader==null) { return; }
            try
            {
                name = reader.ReadString();
                positiveKey = (KeyCode)reader.ReadInt32();
                negativeKey = (KeyCode)reader.ReadInt32();
                gravity = reader.ReadSingle();
                sensitivity = reader.ReadSingle();
            }
            catch(Exception e) 
            {
                ReSetKey();
                Debug.LogWarning("Load InputData Error: "+e.Message);
            }
        }

        public void Save(BinaryWriter writer)
        {
            if(writer==null) { return; }
            try
            {
                writer.Write(name);
                writer.Write((int)positiveKey);
                writer.Write((int)negativeKey);
                writer.Write(gravity);
                writer.Write(sensitivity);
            }
            catch(Exception e) 
            {
                Debug.LogWarning("Save InputData Error: "+e.Message);
            }
        }
    }

    [System.Serializable]
    public class KeyData
    {
        public string name;
        public KeyCode code;
        public KeyType type;
        public bool value;
        public static implicit operator bool(KeyData data) { return data.value; }
        public KeyData(string name,KeyCode code,KeyType type = KeyType.Down) { this.name = name; this.code = code; this.type = type; }
        public void Update() 
        { 
            switch (type)
            {
                case KeyType.Down: value = Input.GetKeyDown(code); break;
                case KeyType.All: value = Input.GetKey(code); break;
                case KeyType.Up: value = Input.GetKeyUp(code); break;
                default: break;
            }
        }

        public void Load(BinaryReader reader)
        {
            if(reader==null) { return; }
            try
            {
                name = reader.ReadString();
                code = (KeyCode)reader.ReadInt32();
                type = (KeyType)reader.ReadInt32();
            }
            catch(Exception e) 
            {
                ReSetKey();
                Debug.LogWarning("Load InputData Error: "+e.Message);
            }
        }

        public void Save(BinaryWriter writer)
        {
            if(writer==null) { return; }
            try
            {
                writer.Write(name);
                writer.Write((int)code);
                writer.Write((int)type);
            }
            catch(Exception e) 
            {
                Debug.LogWarning("Save InputData Error: "+e.Message);
            }
        }
    }

    public static AxisData horizontal = new AxisData("Horizontal",KeyCode.RightArrow,KeyCode.LeftArrow);
    public static AxisData vertical = new AxisData("Vertical",KeyCode.UpArrow,KeyCode.DownArrow);
    public static KeyData jump = new KeyData("Jump",KeyCode.C,KeyType.Down);
    public static KeyData rush = new KeyData("Rush",KeyCode.X,KeyType.Down);
    public static KeyData climb = new KeyData("Climb",KeyCode.Z,KeyType.All);
    public static KeyData bulletTime = new KeyData("BulletTime",KeyCode.Space,KeyType.Down);
    public static KeyData esc = new KeyData("Esc",KeyCode.Escape,KeyType.Down);

    public static bool isLocked;

    public static void ReSetValue()
    {
        horizontal.value = vertical.value;
        jump.value = rush.value = climb.value = bulletTime.value = esc.value = false;
    }

    public static void ReSetKey()
    {
        horizontal.positiveKey = KeyCode.RightArrow;
        horizontal.negativeKey = KeyCode.LeftArrow;
        vertical.positiveKey = KeyCode.UpArrow;
        vertical.negativeKey = KeyCode.DownArrow;
        jump.code = KeyCode.C;
        rush.code = KeyCode.X;
        climb.code = KeyCode.Z;
        bulletTime.code = KeyCode.Space;
        esc.code = KeyCode.Escape;
    }

    public static string GetKeyString(KeyCode code)
    {
        return Enum.GetName(typeof(KeyCode),code);
    }

    public static KeyCode GetAnyKeyDown()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return keyCode;
                }
            }
        }
        return KeyCode.None;
    }

    public override void Load()
    {
        if(File.Exists(STRING.Path.InputData))
        {
            FileStream stream = File.OpenRead(STRING.Path.InputData);
            var reader = new BinaryReader(stream,Encoding.UTF8);
            horizontal.Load(reader);
            vertical.Load(reader);
            jump.Load(reader);
            rush.Load(reader);
            climb.Load(reader);
            bulletTime.Load(reader);
            esc.Load(reader);
            reader.Close();
        }
    }

    public override void Save()
    {
        FileStream stream = Utility.UFile.OpenFile(STRING.Path.InputData);
        var writer = new BinaryWriter(stream,Encoding.UTF8);
        horizontal.Save(writer);
        vertical.Save(writer);
        jump.Save(writer);
        rush.Save(writer);
        climb.Save(writer);
        bulletTime.Save(writer);
        esc.Save(writer);
        writer.Close();
    }

    void Update() 
    {
        if(isLocked) { return; }
        horizontal.Update();
        vertical.Update();
        jump.Update();
        rush.Update();
        climb.Update();
        bulletTime.Update();
        esc.Update();
    }
}
