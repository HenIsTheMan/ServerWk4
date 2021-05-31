using System.Text;
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
            string message = "Hello World";
            byte[] content = Encoding.Default.GetBytes(message);
            PhotonNetwork.RaiseEvent(1, content, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }
}
