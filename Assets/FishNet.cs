using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 渔网
/// </summary>
public class FishNet : MonoBehaviour
{
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Goods>()!=null&& photonView.IsMine)
        {
            other.GetComponent<Goods>().ShouJi();//收集到物品 删除该物品
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
