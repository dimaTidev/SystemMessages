using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SystemMessage : MonoBehaviour
{
    [ContextMenu("Send system msg")]
    void SendMessage()
    {
            SystemMessage.Log("Error: First", SystemMessage.MsgType.Error);
            SystemMessage.Log("Msg: Second");
    }
}
