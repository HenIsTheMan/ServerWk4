using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Server {
    internal sealed class Launcher: MonoBehaviourPunCallbacks {
        #region Fields

        private string gameVer;

        [Tooltip("The maximum number of players per room." +
            "When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Launcher() {
            gameVer = "4.0";

            maxPlayersPerRoom = 4;
        }

        static Launcher() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start() {
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SerializationProtocolType
                = ExitGames.Client.Photon.SerializationProtocol.GpBinaryV16;

            Connect();
        }

        #endregion

        private void Connect() {
            if(PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
            } else {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVer;
            }
        }

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string msg) {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom() {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }

        #endregion
    }
}