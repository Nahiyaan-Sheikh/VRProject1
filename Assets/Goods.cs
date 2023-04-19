using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 收集的商品
/// </summary>
public class Goods : MonoBehaviour
{
    public float s_Min, s_Max;

    float s = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            s = Random.Range(s_Min, s_Max);
        }
        Invoke("Ent", 60);
    }
    public void Ent()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);// 删除该物品
        }
    }
    /// <summary>
    /// 收集
    /// </summary>
    public void ShouJi()
    {
        GetComponent<PhotonView>().RPC("ShouJi_SV", RpcTarget.All, null);
    }
    [PunRPC]
    public void ShouJi_SV()
    {
        Ent();
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            transform.position += new Vector3(s, 0, 0) * Time.deltaTime;
        }
    }
}
