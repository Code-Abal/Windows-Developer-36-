using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerHandler
{
    //#region 기존 핸들러
    

    //public static void WelcomeReceived(int _fromClient, Packet _packet)
    //{
    //    int _clientIdCheck = _packet.ReadInt();
    //    string _username = _packet.ReadString();

    //    Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
    //    if (_fromClient != _clientIdCheck)
    //    {
    //        Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
    //    }
    //    Server.clients[_fromClient].SendIntoLobby(_username);
    //}
    //public static void Ready(int _fromClient, Packet _packet)
    //{
    //    Server.clients[_fromClient].Player.IsReady = !Server.clients[_fromClient].Player.IsReady;
    //    ServerSend.Ready(Server.clients[_fromClient].Player);

    //    foreach (Client _client in Server.clients.Values)
    //    {
    //        if (_client.Player != null && _client.Player.IsReady == false)
    //            return;
    //    }
        

    //        ServerSend.SpawnPlayer();
    //}
    //public static void PlayerMovement(int _fromClient, Packet _packet)
    //{
    //    bool[] _inputs = new bool[_packet.ReadInt()];
    //    for (int i = 0; i < _inputs.Length; i++)
    //    {
    //        _inputs[i] = _packet.ReadBool();
    //    }
    //    Quaternion _rotation = _packet.ReadQuaternion();

    //    Server.clients[_fromClient].Player.SetInput(_inputs, _rotation);
    //}
    //public static void GetItem(int _fromClient, Packet _packet)
    //{
    //    int getItem = _packet.ReadInt();

    //    Server.clients[_fromClient].Player.inventory.Add(getItem);
    //}

    //public static void ChangeHand(int _fromClient, Packet _packet)
    //{
    //    int HandItem = Server.clients[_fromClient].Player.ChangeHand();

    //    ServerSend.ChangeHand(HandItem);
    //}
    //#endregion

    #region 회원가입 
    //회원가입할 사용자 정보.
    public static void UserInfo(int _fromClient, Packet _packet)
    {
        string id = _packet.ReadString();
        string pwd = _packet.ReadString();

        ServerSend.NewUserbool(_fromClient, DB.NewInsertMember(id, pwd));
    }

    //회원가입할 사용자 아이디 중복체크.
    public static void NewIDCheck(int _fromClient, Packet _packet)
    {
        string id = _packet.ReadString();

        Debug.Log("중복검사를 실시합니다.");
        ServerSend.NewIDCheck(_fromClient, DB.NewMemberCheckIDMySql(id));

    }
    #endregion

    #region 로그인
    //로그인할 사용자 정보 받아서 일치여부.
    public static void UserLoginInfo(int _fromClient, Packet _packet)
    {
        string id = _packet.ReadString();
        string pwd = _packet.ReadString();

        Debug.Log("로그인 정보 수신");
        
        ServerSend.LoginCheck(_fromClient, DB.LoginCheckMySql(id, pwd));
    }
    #endregion 


}
