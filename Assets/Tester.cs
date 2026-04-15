using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HslCommunication.ModBus;

using HslCommunication;
using System.Threading;

public class Tester : MonoBehaviour
{

    ModbusTcpNet netClient = new ModbusTcpNet("192.168.200.1", 502 );

    //int[] t;
    //short t2;
    public short[] y;

    Thread readThread;
    // Start is called before the first frame update
    void Start()
    {
        readThread = new Thread(new ThreadStart(ReadHoldingRegister));
        readThread.Start();
    }

    // Update is called once per frame
    void Update()
    {

        //t2 = netClient.ReadInt16("1", 20).Content;

        //int a = netClient.ReadInt32("1").Content;
        //OperateResult<ushort[]> t = netClient.ReadUInt16("1" , 10);
        //int t = netClient.ReadInt32("2").Content;


        //y = netClient.ReadInt16("1" , 10).Content;
        var readResult = netClient.ReadInt16("10");
        if (Input.GetMouseButtonDown(0))
        {
            netClient.Write("2", 5);
            
            //netClient.Write("D002", 2);
        }
        
    }
    public void ReadHoldingRegister()
    {
        while (true)
        {
            // Åª¨úHolding Register
            var readResult = netClient.ReadInt16("10");
            if (readResult.IsSuccess)
            {
                Debug.Log("Holding Register value: " + readResult.Content);
            }
            else
            {
                Debug.LogError("Failed to read Holding Register: " + readResult.Message);
            }

            // µ¥«Ý1¬í
            Thread.Sleep(100);
        }
    }

}

