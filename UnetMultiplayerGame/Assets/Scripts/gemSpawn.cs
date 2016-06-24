using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gemSpawn : NetworkBehaviour{
    public GameObject gemPrefab;
    public int numberOfGems;

    //在server启动时调用
    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfGems; i++)
        {
            Vector3 position = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-20f, 20f));
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            GameObject gem = Instantiate(gemPrefab, position, rotation) as GameObject;
            NetworkServer.Spawn(gem);
        }
    }
}
