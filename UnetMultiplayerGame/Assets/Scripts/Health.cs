using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]public int currentHealth = maxHealth;
    public Slider healthSlider;
    public bool destroyOnDeath = false;

    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int damege)
    {
        if (isServer == false)//血量的处理只在服务器端执行
            return;

        currentHealth -= damege;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(this.gameObject);
                return;
            }

            currentHealth = maxHealth;
            Debug.Log("DIE");
            RpcRespawn();
        }
    }

	public void recover(int blood)
	{
		if (isServer == false)//血量的处理只在服务器端执行
			return;

		if (currentHealth < maxHealth)
		{
            currentHealth += blood;
		}
	}

    void OnChangeHealth(int health)
    {
        healthSlider.value = health / (float)maxHealth;
    }

    [ClientRpc]//远程过程调用，服务器控制改变客户端的信息
    void RpcRespawn()
    {
        if (isLocalPlayer == false)
            return;

        Vector3 spawnPos = Vector3.zero;
        if(spawnPoints != null && spawnPoints.Length > 0)
            spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        transform.position = spawnPos;
    }
}
