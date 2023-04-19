using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// 链接服务器
/// </summary>
public class LinkManager : MonoBehaviourPunCallbacks
{
    public static LinkManager initialize;
    //1.链接服务器
    //2.获取房间列表
    //3.获取用户列表
    //4.根据房间名称加入房间
    private void Awake()
    {
        initialize = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    #region 可注册事件
    /// <summary>
    /// 成功链接服务器
    /// </summary>
    public Action OnConnectedToMaster_Event;
    /// <summary>
    /// 链接服务器失败或者断开链接
    /// </summary>
    public Action OnDisconnected_Event;
    /// <summary>
    /// 加入游戏大厅
    /// </summary>
    public Action OnJoinedLobby_Event;
    /// <summary>
    /// 加入房间成功时调用
    /// </summary>
    public Action OnJoinedRoom_Event;
    /// <summary>
    /// 加入房间失败时调用
    /// </summary>
    public Action OnJoinRandomFailed_Event; 
    #endregion
    #region 公开方法
    /// <summary>
    ///链接服务器
    /// </summary>
    public void Connect()
    {

        //// 是否链接服务器
        //if (PhotonNetwork.IsConnected)
        //{
        //    //  PhotonNetwork.JoinRandomRoom();
        //}
        //else
        //{
        //    print("链接光子服务器");
        //    //链接光子服务器
        //    PhotonNetwork.ConnectUsingSettings();
        //}
        if (PhotonNetwork.IsConnected)
        {
   
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

 

            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();

        }
    }
    /// <summary>
    /// 加入大厅(加入大厅之后可以获取房间列表)（成功链接服务器之后会自动加入一次大厅）
    /// </summary>
    public void JoinLobby()
    {
        // 若当前不在大厅，且状态为可以加入大厅时。让玩家进入大厅
        if (!PhotonNetwork.InLobby && PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }    /// <summary>
         /// 创建房间
         /// </summary>
         /// <param name="roonName">房间名称</param>
    public void CreatRoom(string roonName)
    {
        if (PhotonNetwork.IsConnected)
        {
            print("创建房间" + roonName);
            PhotonNetwork.CreateRoom(roonName);
        }
        else
        {
            print("还没链接服务器");
        }

    }
    /// <summary>
    /// 加入房间
    /// </summary>
    /// <param name="roonName">房间名称</param>
    public void JionRoom(string roonName)
    {
        if (PhotonNetwork.IsConnected)
        {
            print("加入房间" + roonName);
            PhotonNetwork.JoinRoom(roonName);
        }
        else
        {
            print("还没链接服务器");
        }
    }
    #endregion
    #region 状态回调
    /// <summary>
    /// 在与Photon建立连接并进行身份验证后调用
    /// </summary>
    //public override void OnConnectedToMaster()
    //{
    //    print("链接服务器成功");
    //    //获取房间列表
    //    if (OnConnectedToMaster_Event != null)
    //    {
    //        OnConnectedToMaster_Event();
    //    }
    //    JoinLobby();
 
    //}


    /// <summary>
    /// 从Photon服务器断开连接后调用。
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("链接服务失败或者掉线了");
        if (OnDisconnected_Event != null)
        {
            OnDisconnected_Event();
        }
        maxUI.SetActive(true);
        UserPlayer.initialize.r.GetComponent<XRRayInteractor>().enabled = true;
        UserPlayer.initialize.r.GetComponent<XRInteractorLineVisual>().enabled = true;
        UserPlayer.initialize.r.GetComponent<LineRenderer>().enabled = true;
    }
    /// <summary>
    /// Called after the connection to the master is established and authenticated
    /// </summary>
    public override void OnConnectedToMaster()
    {

            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();

    }

    /// <summary>
    /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
    /// </summary>
    /// <remarks>
    /// Most likely all rooms are full or no rooms are available. <br/>
    /// </remarks>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
       
     

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null );
    }
    /// <summary>
    /// 进入游戏大厅回调
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("进入游戏大厅");
        if (OnJoinedLobby_Event != null)
        {
            OnJoinedLobby_Event();
        }
        //加入随机房间
      //  PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// 加入大厅之后，房间列表跟新
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("房间列表跟新");
        if (roomList.Count != 0)
        {

            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo room = roomList[i];
                print("房间名称+" + room.Name);
            }
        }
    }



    ///// <summary>
    ///// 当JoinRandom()调用失败时调用。参数提供ErrorCode和消息。
    ///// </summary>
    ///// <remarks>
    ///// 很可能所有的房间都满了，或者没有空房了。 <br/>
    ///// </remarks>
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    print("加入房间失败");

    //    if (OnJoinRandomFailed_Event != null)
    //    {
    //        OnJoinRandomFailed_Event();
    //    }
    //}
    [Header("角色控制器，需要放在Resources文件夹下")]
    public GameObject player;
    /// <summary>
    /// 登录界面
    /// </summary>
    public GameObject maxUI;

    public RandomMaterial[] randomMaterials;
    /// <summary>
    /// 加入房间时调用
    /// </summary>
    public override void OnJoinedRoom()
    {
        print("成功加入房间");
        //创建用户
        GameObject game = PhotonNetwork.Instantiate(player.name, new Vector3(0f, 0, 0f), Quaternion.identity);
        //  //记录生成角色控制器
        ////  UserData_Manager.initialize.photonView = game.GetComponent<PhotonView>();
        if (OnJoinedRoom_Event != null)
        {
            OnJoinedRoom_Event();
        }
        print(PhotonNetwork.LocalPlayer.UserId);
        maxUI.SetActive(false);
        UserPlayer.initialize.r.GetComponent<XRRayInteractor>().enabled = false;
        UserPlayer.initialize.r.GetComponent<XRInteractorLineVisual>().enabled = false;
        UserPlayer.initialize.r.GetComponent<LineRenderer>().enabled = false;
        foreach (var item in randomMaterials)
        {
            item.StartRandom();
        }
    } 
    #endregion
    // Update is called once per frame
    void Update()
    {


    }
}
