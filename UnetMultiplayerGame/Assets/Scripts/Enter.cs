using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Enter : MonoBehaviour {
    public Button ok;
    public InputField m_name;
    public string playerName = "";
    private int count = 0;
    private string ip = "";
    private int port = 0;
	// Use this for initialization
	void Start () {
        ok.onClick.AddListener(() =>
        {
            playerName = m_name.text;
            NetworkClient netclient = GetComponent<NetworkManager>().StartClient();
            if (ip != null && port != 0)
                netclient.Connect(ip, port);
            if (netclient.isConnected)
            {
                ok.gameObject.SetActive(false);
                m_name.gameObject.SetActive(false);
            }
            else {
                netclient.Shutdown();
                netclient = GetComponent<NetworkManager>().StartHost();
                ip = netclient.serverIp;
                port = netclient.serverPort;
                Debug.Log(ip + "," + port);
                if (netclient.isConnected)
                {
                    ok.gameObject.SetActive(false);
                    m_name.gameObject.SetActive(false);
                }
            }
            
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
