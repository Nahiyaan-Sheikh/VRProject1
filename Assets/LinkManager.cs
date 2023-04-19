using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// ���ӷ�����
/// </summary>
public class LinkManager : MonoBehaviourPunCallbacks
{
    public static LinkManager initialize;
    //1.���ӷ�����
    //2.��ȡ�����б�
    //3.��ȡ�û��б�
    //4.���ݷ������Ƽ��뷿��
    private void Awake()
    {
        initialize = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    #region ��ע���¼�
    /// <summary>
    /// �ɹ����ӷ�����
    /// </summary>
    public Action OnConnectedToMaster_Event;
    /// <summary>
    /// ���ӷ�����ʧ�ܻ��߶Ͽ�����
    /// </summary>
    public Action OnDisconnected_Event;
    /// <summary>
    /// ������Ϸ����
    /// </summary>
    public Action OnJoinedLobby_Event;
    /// <summary>
    /// ���뷿��ɹ�ʱ����
    /// </summary>
    public Action OnJoinedRoom_Event;
    /// <summary>
    /// ���뷿��ʧ��ʱ����
    /// </summary>
    public Action OnJoinRandomFailed_Event; 
    #endregion
    #region ��������
    /// <summary>
    ///���ӷ�����
    /// </summary>
    public void Connect()
    {

        //// �Ƿ����ӷ�����
        //if (PhotonNetwork.IsConnected)
        //{
        //    //  PhotonNetwork.JoinRandomRoom();
        //}
        //else
        //{
        //    print("���ӹ��ӷ�����");
        //    //���ӹ��ӷ�����
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
    /// �������(�������֮����Ի�ȡ�����б�)���ɹ����ӷ�����֮����Զ�����һ�δ�����
    /// </summary>
    public void JoinLobby()
    {
        // ����ǰ���ڴ�������״̬Ϊ���Լ������ʱ������ҽ������
        if (!PhotonNetwork.InLobby && PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }    /// <summary>
         /// ��������
         /// </summary>
         /// <param name="roonName">��������</param>
    public void CreatRoom(string roonName)
    {
        if (PhotonNetwork.IsConnected)
        {
            print("��������" + roonName);
            PhotonNetwork.CreateRoom(roonName);
        }
        else
        {
            print("��û���ӷ�����");
        }

    }
    /// <summary>
    /// ���뷿��
    /// </summary>
    /// <param name="roonName">��������</param>
    public void JionRoom(string roonName)
    {
        if (PhotonNetwork.IsConnected)
        {
            print("���뷿��" + roonName);
            PhotonNetwork.JoinRoom(roonName);
        }
        else
        {
            print("��û���ӷ�����");
        }
    }
    #endregion
    #region ״̬�ص�
    /// <summary>
    /// ����Photon�������Ӳ����������֤�����
    /// </summary>
    //public override void OnConnectedToMaster()
    //{
    //    print("���ӷ������ɹ�");
    //    //��ȡ�����б�
    //    if (OnConnectedToMaster_Event != null)
    //    {
    //        OnConnectedToMaster_Event();
    //    }
    //    JoinLobby();
 
    //}


    /// <summary>
    /// ��Photon�������Ͽ����Ӻ���á�
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("���ӷ���ʧ�ܻ��ߵ�����");
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
    /// ������Ϸ�����ص�
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("������Ϸ����");
        if (OnJoinedLobby_Event != null)
        {
            OnJoinedLobby_Event();
        }
        //�����������
      //  PhotonNetwork.JoinRandomRoom();
    }
    /// <summary>
    /// �������֮�󣬷����б����
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("�����б����");
        if (roomList.Count != 0)
        {

            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo room = roomList[i];
                print("��������+" + room.Name);
            }
        }
    }



    ///// <summary>
    ///// ��JoinRandom()����ʧ��ʱ���á������ṩErrorCode����Ϣ��
    ///// </summary>
    ///// <remarks>
    ///// �ܿ������еķ��䶼���ˣ�����û�пշ��ˡ� <br/>
    ///// </remarks>
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    print("���뷿��ʧ��");

    //    if (OnJoinRandomFailed_Event != null)
    //    {
    //        OnJoinRandomFailed_Event();
    //    }
    //}
    [Header("��ɫ����������Ҫ����Resources�ļ�����")]
    public GameObject player;
    /// <summary>
    /// ��¼����
    /// </summary>
    public GameObject maxUI;

    public RandomMaterial[] randomMaterials;
    /// <summary>
    /// ���뷿��ʱ����
    /// </summary>
    public override void OnJoinedRoom()
    {
        print("�ɹ����뷿��");
        //�����û�
        GameObject game = PhotonNetwork.Instantiate(player.name, new Vector3(0f, 0, 0f), Quaternion.identity);
        //  //��¼���ɽ�ɫ������
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
