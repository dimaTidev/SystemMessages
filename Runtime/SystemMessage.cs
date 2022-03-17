using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SystemMessage : MonoBehaviour//ASingleton<SystemMessage>
{
    static SystemMessage instance;

    [SerializeField] Animation anim = null;
    [SerializeField] Text text_msg = null;
    [SerializeField] Image icon = null;
    [SerializeField] Image fon = null;
    [SerializeField] List<MsgTypeIcon> icons = null;
    [System.Serializable]
    public class MsgTypeIcon
    {
        public MsgType msgType;
        public Sprite sprite;
        public Color color;
    }

    static SystemMessage Instance
    {
        get
        {
            if (!instance)
            {
                SystemMessage prefab = Resources.Load<SystemMessage>("Canvas_SystemMessage_alt");

                if (!prefab)
                {
                    prefab = Resources.Load<SystemMessage>("Canvas_SystemMessage");
                }

                if (!prefab)
                {
                    Debug.LogError("Error: can't find prefab at : Canvas_SystemMessage_alt and Canvas_SystemMessage");
                    return null;
                }
                else
                {
                    GameObject go = Instantiate(prefab.gameObject);
                    instance = go.GetComponent<SystemMessage>();
                }
            }  

            return instance;
        }
    }


    Queue<MsgData> listOfMsgs = new Queue<MsgData>();


   

    public class MsgData
    {
        public string text;
        public MsgType msgType;

        public MsgData(MsgType msgType, string text)
        {
            this.msgType = msgType;
            this.text = text;
        }
    }
    public enum MsgType { Error, Normal }

    public static void Show_Message(string msg, MsgType msgType = MsgType.Normal)
    {
        // Debug.Log("Queued msg: " + msg);
        if (!Instance)
            return;

        Instance.listOfMsgs.Enqueue(new MsgData(msgType, msg));
        Instance.ShowMsgs();
    }

    bool isShowinNow;

    void ShowMsgs()
    {
        if (isShowinNow)
            return;

        if(listOfMsgs != null && listOfMsgs.Count > 0)
        {
            Debug.Log("Show msg");
            MsgData data = listOfMsgs.Dequeue();
            isShowinNow = true;
            Fill_UI(data);
            if (anim)
                anim.PlayQueued("SystemMessage_Show");
            else
                Debug.Log("System message got: " + data.text);
        }
        else
        {
            Debug.Log("Destroy");
            instance = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Animation callback
    /// </summary>
    public void OnAnimationEnd_Callback()
    {
        Debug.Log("OnAnimationEnd_Callback");
        isShowinNow = false;
        ShowMsgs();
    }


    void Fill_UI(MsgData data)
    {
        if(icon && icons != null && icons.Count > 0)
        {
            MsgTypeIcon iconData = icons.FirstOrDefault(x => x.msgType == data.msgType);
            if (iconData != null && iconData != default)
            {
                icon.sprite = iconData.sprite;
                if (fon) fon.color = iconData.color;
            }
        }
        if (text_msg) text_msg.text = data.text;
    }
        
}
