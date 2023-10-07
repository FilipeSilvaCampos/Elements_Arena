using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ElementsArena.Photon
{
    public class PhotonPropertiesKeys
    {
        public const string LooserPlayer = "iLoose";
    }

    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] MenuManager menuManager;
        [SerializeField] GameObject connectingPanel;

        bool isConnecting = false;
        Core.GameManager gameManager;

        #region MonoBehaviour Callbacks
        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            connectingPanel.SetActive(false);
        }

        private void Update()
        {
            connectingPanel.SetActive(isConnecting);
        }
        #endregion

        #region Pun Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnetedToMaster() was called by Pun");
            menuManager.SwitchMode(MenuState.SelectingCharacter);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by Pun");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 });
        }

        public override void OnCreatedRoom()
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { [PhotonPropertiesKeys.LooserPlayer] = "" });
            Debug.Log("OnCreateRoom() was called by Pun");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom() was called by Pun");
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
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
                menuManager.SwitchMode(MenuState.SelectingCharacter);
                return;
            }

            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }
}