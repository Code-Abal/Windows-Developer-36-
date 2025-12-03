using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    #region  서버 관련
    
    /// <summary>Sends a packet to a client via TCP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to a client via UDP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    /// <summary>Sends a packet to all clients via TCP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via TCP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    /// <summary>Sends a packet to all clients via UDP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via UDP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }
    #endregion

    //회원가입
    public static void NewIDCheck(int _fromClient, bool IDcheck)
    {
        using (Packet _packet = new Packet((int)ServerPackets.NewIDboolCheck))
        {
            _packet.Write(IDcheck);

            SendTCPData(_fromClient, _packet);
        }
    }

    public static void NewUserbool(int _fromClient, bool insertResult)
    {
        using (Packet _packet = new Packet((int)ServerPackets.NewUserboolresult))
        {
            _packet.Write(insertResult);

            SendTCPData(_fromClient, _packet);
        }
    }

    //로그인
    public static void LoginCheck(int _fromClient, bool Loginboolcheck)
    {
        using (Packet _packet = new Packet((int)ServerPackets.LoginCheck))
        {
            _packet.Write(Loginboolcheck);

            SendTCPData(_fromClient, _packet);
            
            Debug.Log("로그인정보 To 클라이언트 : " + Loginboolcheck);
            
        }

    }



    //#region Packets
    ///// <summary>Sends a welcome message to the given client.</summary>
    ///// <param name="_toClient">The client to send the packet to.</param>
    ///// <param name="_msg">The message to send.</param>
    //public static void Welcome(int _toClient, string _msg)
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.welcome))
    //    {
    //        _packet.Write(_msg);
    //        _packet.Write(_toClient);

    //        SendTCPData(_toClient, _packet);
    //    }
    //}
    //public static void EnterLobby(int _toClient, Player _player)
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.EnterLobby))
    //    {
    //        _packet.Write(_player.username);
    //        _packet.Write(_player.IsReady);

    //        SendTCPData(_toClient, _packet);
    //    }
    //}
    //public static void Ready(Player _player)
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.Ready))
    //    {
    //        _packet.Write(_player.username);
    //        _packet.Write(_player.IsReady);

    //        SendTCPDataToAll(_packet);
    //    }
    //}
    //public static void GameStart()
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.GameStart))
    //    {
    //        SendTCPDataToAll(_packet);
    //    }
    //}

    ///// <summary>Tells a client to spawn a player.</summary>
    ///// <param name="_toClient">The client that should spawn the player.</param>
    ///// <param name="_player">The player to spawn.</param>
    //public static void SpawnPlayer()
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
    //    {
    //        int PlayerCount = 0;
    //        foreach (Client _client in Server.clients.Values)
    //        {
    //            if (_client.Player != null)
    //                PlayerCount++;
    //        }
    //        _packet.Write(PlayerCount);
    //            foreach (Client _client in Server.clients.Values)
    //        {
    //            if (_client.Player != null)
    //            {
    //                _packet.Write(_client.Player.id);
    //                _packet.Write(_client.Player.username);
    //                _packet.Write(_client.Player.transform.position);
    //                _packet.Write(_client.Player.transform.rotation);
    //            }
    //        }

    //        SendTCPDataToAll(_packet);

    //        foreach (Client _client in Server.clients.Values)
    //        {
    //            if(_client.Player != null)
    //                _client.Player.IsStart = true;
    //        }
    //    }
    //}

    ///// <summary>Sends a player's updated position to all clients.</summary>
    ///// <param name="_player">The player whose position to update.</param>
    //public static void PlayerPosition(Player _player)
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
    //    {
    //        _packet.Write(_player.id);
    //        _packet.Write(_player.transform.position);

    //        SendUDPDataToAll(_packet);
    //    }
    //}

    ///// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
    ///// <param name="_player">The player whose rotation to update.</param>
    //public static void PlayerRotation(Player _player)
    //{
    //    using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
    //    {
    //        _packet.Write(_player.id);
    //        _packet.Write(_player.transform.rotation);

    //        SendUDPDataToAll(_player.id, _packet);
    //    }
    //}

    //public static void ChangeHand(int handItem)
    //{
    //    using(Packet _packet = new Packet((int)ServerPackets.ChangeHand))
    //    {

    //        _packet.Write(handItem);

    //        SendUDPDataToAll(_packet);
    //    }
    //}
    //#endregion
}
