using HslCommunication.ModBus;
using HslCommunication;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting;

public class ModbusTCP : MonoBehaviour
{   
    public List<GameObject> lights = new List<GameObject>();

    ModbusTcpNet netClient = new ModbusTcpNet("192.168.200.1", 502);

    public bool[] readServer;

    bool ac;
    public GameObject airConditionerBlue;

    Thread readThread;

    public short[] ServerVariable;      //1-5是燈泡瓦數，6是冷氣開關，7是冷氣瓦數，8是冷氣狀態

    [Header("UI文字類")]
    public TextMeshProUGUI airConditionerText;
    public TextMeshProUGUI CO2Text;
    public TextMeshProUGUI PM2_5Text;
    public TextMeshProUGUI PM10Text;
    public TextMeshProUGUI TVOCText;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI humidityText;
    public TextMeshProUGUI humanNumText;

    public Image acButton;

    public GameObject textPanel;

    [Header("fanspeed")]
    public short fan;
    public List<Image> img;

    public GameObject fanctrl;

    void Start()
    {
        readThread = new Thread(new ThreadStart(ReadHoldingRegister));
        readThread.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ServerVariable == null)
        {
            readThread = new Thread(new ThreadStart(ReadHoldingRegister));
            readThread.Start();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            textPanel.SetActive(!textPanel.activeInHierarchy);
        }

        if(ServerVariable != null && ServerVariable.Length > 0)
        {
            ac = ServerVariable[1] == 0? false : true;

            airConditionerBlue.SetActive(ac);
            fanctrl.SetActive(ac);
            acController(ac);

            #region 1
            for (int i = 5; i < 10; i++)
            {
                if ((int)ServerVariable[i] == 1)
                {
                    readServer[i - 5] = true;
                }
                else if ((int)ServerVariable[i] == 0)
                {
                    readServer[i - 5] = false;
                }

                lights[i - 5].SetActive(readServer[i - 5]);


            }

            airConditionerText.text = "冷氣耗電量:" + ServerVariable[20].ToString() + "W, \n" +
                                      
                                      "設定溫度:" + ServerVariable[2].ToString() + " °C"; // 16 / 18

            CO2Text.text = "二氧化碳量:" + ServerVariable[10].ToString() + " (ppm)";
            PM2_5Text.text = "PM2.5量:" + ServerVariable[11].ToString() + " (ppm)";
            PM10Text.text = "PM10量:" + ServerVariable[12].ToString() + " (ppm)";
            TVOCText.text = "揮發性有機物量:" + (ServerVariable[17] / 10).ToString() + " (mg/m3)";
            temperatureText.text = "室內溫度:" + ((float)ServerVariable[18] / 100).ToString() + " °C";
            humidityText.text = "濕度:" + ((float)ServerVariable[19] /100 ).ToString() + " (%)";

            humanNumText.text = "目前人數: " + fan.ToString() + " 人";

            #endregion
        }

        switch (fan)
        {
            case 0:
                foreach(var i in img)
                {
                    i.color = Color.gray;
                }
                break;
            case 1:
                img[0].color = Color.cyan;
                img[1].color = Color.gray;
                img[2].color = Color.gray;
                break;
            case 2:
                img[0].color = Color.cyan;
                img[1].color = Color.cyan;
                img[2].color = Color.gray;
                break;
            case 3:
                foreach (var i in img)
                {
                    i.color = Color.cyan;
                }
                break;
        }
    }

    public void acController(bool onoff)
    {
        if (onoff)
        {
            airConditionerText.color = Color.yellow;
            acButton.color = Color.yellow;
        }
        else
        {
            airConditionerText.color = Color.white;
            acButton.color = Color.white;
        }
    }
    public void ReadHoldingRegister()
    {
        while (true)
        {
            // 讀取Holding Register
            ServerVariable = netClient.ReadInt16("0" , 21).Content;
  
            fan = ServerVariable[3];
            
            // 等待1秒
            Thread.Sleep(1000);
        }
    }

    public void ChangeFanSpeed(int input)
    {
        fan = (short)input;
        

       netClient.Write("3", (short)fan);

    }
}