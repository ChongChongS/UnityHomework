using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    private GameObject player;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private int bulletSpeed = 10;
    private int rotateSpeed = 120;
    private int moveSpeed = 3;
    private bool shouldRandomMove = true;
    private float i = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
     
    // Update is called once per frame
    void Update()
    {
        randomMove();
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player.name);
        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 5.0f)
            {
                if(i++ % 50 == 0)   
                    turnAndFire();
            }
            else
                shouldRandomMove = true;
        }
    }

    //[Command]//called in client ,run in server
    void turnAndFire()
    {
        shouldRandomMove = false;

        transform.LookAt(player.transform);

        //在server控制bullet生成
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 1);

        //控制动画
        //GetComponent<NetworkAnimator>().SetTrigger("Fire");
        //同步到各个客户端
        NetworkServer.Spawn(bullet);
    }

    void randomMove() {
        int dir = Random.Range(0,10);
        int x = Random.Range(-1, 1);
        if (dir > 2)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up * x *rotateSpeed * Time.deltaTime);
        }
    }
}
