using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using HslCommunication;
using HslCommunication.ModBus;

public class lightClicking : MonoBehaviour
{

    Thread readThread;

    public short onoff;


    ModbusTcpNet netClient = new ModbusTcpNet("192.168.200.1", 502);

    public string coilNum;

    private void OnMouseDown()
    {   
        onoff = onoff == 0? (short)1 : (short)0;
        netClient.Write(coilNum, onoff);

    }

}
