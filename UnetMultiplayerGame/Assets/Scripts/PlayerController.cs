using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
    private int rotateSpeed = 120;
    private int moveSpeed = 3;
    private int bulletSpeed = 10;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	private GameObject joystick;
	private GameObject easyButton;

	// Update is called once per frame
	void Update () {
        if (isLocalPlayer == false)
        {
            return;
        }

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
		joystick = GameObject.Find("playerStick");
		easyButton = GameObject.Find ("Fire");
		float h = joystick.GetComponent<EasyJoystick>().JoystickAxis.x;
		float v = joystick.GetComponent<EasyJoystick> ().JoystickAxis.y;

        transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Space) || easyButton.GetComponent<EasyButton> ().buttonState.ToString() == "Down")
        {
            CmdFire();
        }
	}

    public override void OnStartLocalPlayer()
    {
        //仅在创建时，改变localPlayer
        GetComponent<MeshRenderer>().material.color = Color.blue;
        Text playerName = transform.FindChild("Canvas").FindChild("playername").GetComponent<Text>();
        Enter enterHandler = GameObject.Find("NetworkManager").GetComponent<Enter>();
        
        //Debug.Log("playername" + playerName.text);
        //Debug.Log("playerName" + enterHandler.playerName);
        playerName.text = enterHandler.playerName;
    }

    
    [Command]//called in client ,run in server
    void CmdFire()
    {
        //在server控制bullet生成
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 2);

        //控制动画
        GetComponent<NetworkAnimator>().SetTrigger("Fire");

        //同步到各个客户端
        NetworkServer.Spawn(bullet);
    }
}
