using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SystemMessage : MonoBehaviour
{
    [ContextMenu("Send system msg")]
    void SendMessage()
    {
            SystemMessage.Show_Message("Error: First", SystemMessage.MsgType.Error);
            SystemMessage.Show_Message("Msg: Second");
    }
}
