using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ElementsArena.Photon
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] MenuManager menuManager;
        bool isConnecting = false;

        #region MonoBehaviour Callbacks
        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        #endregion

        #region Pun Callbacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnetedToMaster() was called by Pun");
            menuManager.SetGameOnlineMode();
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by Pun");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 });
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreateRoom() was called by Pun");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom() was called by Pun");
            if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
        #endregion

        public void Connecet()
        {
            if (isConnecting) return;

            if (PhotonNetwork.IsConnected)
            {
                menuManager.SetGameOnlineMode();
            }

            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }
}