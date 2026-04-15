using HslCommunication.ModBus;
using HslCommunication;


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class TestModbus : MonoBehaviour
{

   /* ModbusTcpNet netClient = new ModbusTcpNet("192.168.2.3", 502);

    public int m_variable;
    public int writeNumber;

    public Light testLight;

    bool settrigger;
    public GameObject lightBox;

    // Update is called once per frame
    void Update()
    {
        if (netClient.ConnectServer().IsSuccess)
        {
            Debug.Log("succesed");

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                m_variable++;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                m_variable--;
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                int testint = netClient.ReadInt32("2").Content;

                Debug.Log(testint);

                testLight.intensity = testint;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                writeNumber += m_variable;

                netClient.Write("2", writeNumber);

                Debug.Log("write");

                int testint2 = netClient.ReadInt32("2").Content;

                testLight.intensity = testint2;

                //netClient.WriteCoil("0", true);

                //Debug.Log(testint2);

            }
            if (Input.GetMouseButtonDown(0))
            {
                settrigger = !settrigger;
            }

            bool coilState = netClient.ReadCoil("10").Content;
            if (coilState != settrigger)
            {
                netClient.WriteCoil("10", settrigger);
                lightBox.SetActive(settrigger);
                Debug.Log("resend");
            }



        }


    }*/
}
