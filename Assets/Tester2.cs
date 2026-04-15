using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System.Net;


public class Tester2 : MonoBehaviour
{
    // 定義PLC相關變數
    SiemensS7Net plc;
    string ipAddress = "192.168.200.1";
    int port = 502;


    // Start is called before the first frame update
    void Start()
    {
        // 初始化PLC對象
        plc = new SiemensS7Net(SiemensPLCS.S1200, ipAddress);

        // 連接PLC
        var connectResult = plc.ConnectServer();
        if (!connectResult.IsSuccess)
        {
            Debug.LogError("Failed to connect to PLC: " + connectResult.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 讀取Holding Register
        var readResult = plc.ReadInt16("2");
        if (readResult.IsSuccess)
        {
            Debug.Log("Holding Register value: " + readResult.Content);
        }
        else
        {
            Debug.LogError("Failed to read Holding Register: " + readResult.Message);
        }

        // 寫入Holding Register
        if (Input.GetMouseButtonDown(0))
        {
            short writeValue = 123;
            var writeResult = plc.Write("D002", writeValue);
            if (writeResult.IsSuccess)
            {
                Debug.Log("Write Holding Register success");
            }
            else
            {
                Debug.LogError("Failed to write Holding Register: " + writeResult.Message);
            }
        }

    }
}
