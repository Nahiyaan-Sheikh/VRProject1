using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 随机素材的发送
/// </summary>
public class RandomMaterial : MonoBehaviour
{
    /// <summary>
    /// 预制件
    /// </summary>
    public GameObject[] prefabs;
    /// <summary>
    /// 最小时间间隔
    /// </summary>
    public float time_Min;
    /// <summary>
    /// 最大时间间隔
    /// </summary>
    public float time_Max;

    /// <summary>
    /// 开始随机
    /// </summary>
    public void StartRandom()
    {
        if ( GetComponent<PhotonView>().IsMine)//自己有操控权限
        {
            Invoke("Create", Random.Range(time_Min, time_Max));
        }
    }
    /// <summary>
    /// 生成器
    /// </summary>
    public void Create()
    {
        CancelInvoke();
        PhotonNetwork.Instantiate(prefabs[Random.Range(0, prefabs.Length)].name, new Vector3(-30f, 0.12f, Random.Range(-4f, 4f)), Quaternion.identity);
        Invoke("Create", Random.Range(time_Min, time_Max));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
