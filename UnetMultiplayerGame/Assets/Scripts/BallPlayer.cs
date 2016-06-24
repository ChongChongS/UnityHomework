using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallPlayer : NetworkBehaviour {
    const int nudgeAmount = 33;

    public enum NudgeDir
    { 
        up,
        down,
        left,
        right,
        jump
    }

	// Update is called once per frame
    [ClientCallback] //run on clients
	void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            CmdNudge(NudgeDir.jump);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CmdNudge(NudgeDir.left);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            CmdNudge(NudgeDir.right);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            CmdNudge(NudgeDir.up);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            CmdNudge(NudgeDir.down);
        }
	}

    [Command]//客户端调用，服务器端执行； 用于同步每个用户自己控制的对象。
    public void CmdNudge(NudgeDir direction)
    {
        switch (direction)
        { 
            case NudgeDir.left:
                GetComponent<Rigidbody>().AddForce(new Vector3(-nudgeAmount, 0, 0));
                break;
            case NudgeDir.right:
                GetComponent<Rigidbody>().AddForce(new Vector3(nudgeAmount, 0, 0));
                break;
            case NudgeDir.up:
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, nudgeAmount));
                break;
            case NudgeDir.down:
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -nudgeAmount));
                break;
            case NudgeDir.jump:
                GetComponent<Rigidbody>().AddForce(new Vector3(0, nudgeAmount, 0));
                break;
        }
    }
}
