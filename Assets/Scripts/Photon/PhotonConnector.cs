using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
        // [SerializeField] string nickName;
        // public static Action<> GetPhotonFriends = delegate { };
        // public static Action OnLobbyJoined = delegate { };

        #region Unity
        
        // void Awake()
        // {
        //     var nickName = PlayerPrefs.GetString("USERNAME");            
        // }

        void Start()
        {
            // var rndName = $"Tester{Guid.NewGuid()}";
            var nickName = PlayerPrefs.GetString("USERNAME");
            // if (PhotonNetwork.IsConnectedAndReady || PhotonNetwork.IsConnected) return;
        
            ConnectToPhoton(nickName);
        }
        
        #endregion
        #region Private

        void ConnectToPhoton(string nickName)
        {
            Debug.Log($"Connect to Photon as {nickName}");
            PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }

        void CreatePhotonRoom(string roomName)
        {
            var roomOptions = new RoomOptions {IsOpen = true, IsVisible = true, MaxPlayers = 4};
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        #endregion
        #region Photon Callbacks
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("You have connected to the Photon Master Server");
            if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
        }
        
        public override void OnJoinedLobby()
        {
            Debug.Log("You have connected to a Photon Lobby");
            // Debug.Log("Invoking get Playfab friends");
            CreatePhotonRoom("TestRoom");
            // GetPhotonFriends?.Invoke();
            // OnLobbyJoined?.Invoke();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"You have created a Photon Room named {PhotonNetwork.CurrentRoom.Name}");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"You have joined the Photon Room named {PhotonNetwork.CurrentRoom.Name}");
        }

        public override void OnLeftRoom()
        {
            Debug.Log($"You have left a Photon Room");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"You failed to join a Photon Room: {message}");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"The player {newPlayer.UserId} has joined the room");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"The player {otherPlayer.UserId} has left the room");
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log($"The new master client is the player {newMasterClient.UserId}");
        }

        #endregion
    }
}
