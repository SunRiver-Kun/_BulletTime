using System;
using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{   
    public enum KeyType{
        Up,Down,Left,Right,Rush,Jump,Climb,BulletTime,Reset,None
    }

    public KeyType type;
    private static InputButton target = null;
    private Text m_text;

    public void ResetKey()
    {
        INPUT.ReSetKey();
    }

    public void SaveData()
    {
        INPUT.instance.Save();
    }

    public void EnterEditor()
    {
        target = this;
        INPUT.isLocked = true;
    }

    void Start()
    {
        m_text = gameObject.GetComponentInChildren<Text>();
        if(m_text==null) { Debug.LogWarning(gameObject.name + " needs a child with Text component !"); }
    }

    void Update()
    {
        if(InputButton.target==this)
        {
            KeyCode code = INPUT.GetAnyKeyDown();
            if(code==KeyCode.Mouse0 || code==KeyCode.Mouse1 || code==KeyCode.Mouse2 || code==KeyCode.Mouse3 ) { return; }
            if(code!=KeyCode.None)
            {
                switch (type)
                {
                    case KeyType.Up :  
                        INPUT.vertical.positiveKey = code;
                    break;

                    case KeyType.Down :  
                        INPUT.vertical.negativeKey = code;
                    break;

                    case KeyType.Left :  
                        INPUT.horizontal.negativeKey = code;
                    break;

                    case KeyType.Right :  
                        INPUT.horizontal.positiveKey = code;
                    break;

                    case KeyType.Rush :  
                        INPUT.rush.code = code;
                    break;

                    case KeyType.Jump :  
                        INPUT.jump.code = code;
                    break;

                    case KeyType.Climb :  
                        INPUT.climb.code = code;
                    break;

                    case KeyType.BulletTime:
                        INPUT.bulletTime.code = code;
                    break;

                    default: break;
                }
                InputButton.target = null;
            }
            else 
            {
                return;
            }
        }

        if(m_text==null) { return; }
        switch (type)
        {
            case KeyType.Up :  
                m_text.text = INPUT.GetKeyString(INPUT.vertical.positiveKey);
            break;

            case KeyType.Down :  
                m_text.text = INPUT.GetKeyString(INPUT.vertical.negativeKey);
            break;

            case KeyType.Left :  
                m_text.text = INPUT.GetKeyString(INPUT.horizontal.negativeKey);
            break;

            case KeyType.Right :  
                m_text.text = INPUT.GetKeyString(INPUT.horizontal.positiveKey);
            break;

            case KeyType.Rush :  
                m_text.text = INPUT.GetKeyString(INPUT.rush.code);
            break;

            case KeyType.Jump :  
                m_text.text = INPUT.GetKeyString(INPUT.jump.code);
            break;

            case KeyType.Climb :  
                m_text.text = INPUT.GetKeyString(INPUT.climb.code);
            break;

            case KeyType.BulletTime:
                m_text.text = INPUT.GetKeyString(INPUT.bulletTime.code);
            break;

            default: break;
        }
    }

    private void OnDisable() {
        INPUT.isLocked = false;
    }
}
