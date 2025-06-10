using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;


public class udp_receive_only : MonoBehaviour
{
    private IPEndPoint ipEndPoint;
    private UdpClient udpClient;
    private Thread receiveThread;
    private byte[] receiveByte;
    private string receiveData = ""; //輸出資料
    private bool IsThrowCoin = false; // 新增旗標
    MoneyBoxCollider MoneyBox;

    void Start()
    {
        ipEndPoint = new IPEndPoint(IPAddress.Any, 22222);
        Debug.LogFormat("ipEndPoint is {0}", ipEndPoint.ToString());
        udpClient = new UdpClient(ipEndPoint.Port);
        Debug.LogFormat("udpClient is {0}", udpClient.ToString());
        receiveThread = new Thread(ReceivingData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("server started");
        MoneyBox = FindFirstObjectByType<MoneyBoxCollider>();
    }
    
    void Update()
    {
        if (IsThrowCoin)
        {
            Debug.LogWarning("ThrowCoin"); 
            IsThrowCoin = false;
            MoneyBox.ThrowCoin();
        }
    }

    private void ReceivingData()
    {
        while (true)
        {
            receiveByte = udpClient.Receive(ref ipEndPoint);
            receiveData = System.Text.Encoding.UTF8.GetString(receiveByte);
            Debug.Log(receiveData);

            if (receiveData == "yes")
            {
                IsThrowCoin = true;
            }
        }
    }

   

    private void OnDisable()
    {
        udpClient.Close();
        receiveThread.Join();
        receiveThread.Abort();
    }
 
    private void OnApplicationQuit()
    {
        receiveThread.Abort();
    }
}
