using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SendMessage: MonoBehaviour {
    public void OnSendMessageButtonClicked()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.RaiseEvent(1, null, RaiseEventOptions.Default, SendOptions.SendReliable);
            PhotonNetwork.RaiseEvent(2, "ok", RaiseEventOptions.Default, SendOptions.SendReliable);
            PhotonNetwork.RaiseEvent(2, "Ok", RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
}
