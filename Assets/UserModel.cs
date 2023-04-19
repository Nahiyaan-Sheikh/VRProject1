using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用户模型
/// </summary>
public class UserModel : MonoBehaviour
{
    public PhotonView photonView;
    /// <summary>
    /// 左右手
    /// </summary>
    public GameObject l, r;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = UserPlayer.initialize.head.transform.position;
            transform.eulerAngles = UserPlayer.initialize.head.transform.eulerAngles;

            l. transform.position = UserPlayer.initialize.l.transform.position;
            l.transform.eulerAngles = UserPlayer.initialize.l.transform.eulerAngles;

            r.transform.position = UserPlayer.initialize.r.transform.position;
            r.transform.eulerAngles = UserPlayer.initialize.r.transform.eulerAngles;
        }
    }
}
