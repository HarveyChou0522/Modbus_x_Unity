using HslCommunication.ModBus;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class AirConditionerController : MonoBehaviour
{

    ModbusTcpNet netClient = new ModbusTcpNet("192.168.200.1", 502);

    public GameObject objOn;
    public GameObject objOff;

    public int onoff;

    public ModbusTCP _modbus;

    Thread readThread;
    bool serverConnect;






    private void OnMouseDown()
    {
        onoff = (onoff == 0)? 1:0;
        netClient.Write("1", (short)onoff);

        readThread = new Thread(new ThreadStart(ReadHoldingRegister));
        readThread.Start();


    }

    public void ReadHoldingRegister()
    {
        while(onoff != (short)netClient.ReadInt16("1").Content)
        {
            netClient.Write("1", (short)onoff);
        }

    }

    public void UiControl()
    {

        onoff = (onoff == 0) ? 1 : 0;
        netClient.Write("1", (short)onoff);

        readThread = new Thread(new ThreadStart(ReadHoldingRegister));
        readThread.Start();

    }


}
