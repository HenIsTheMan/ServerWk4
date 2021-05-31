using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class RaiseEventListener: MonoBehaviourPun
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnEvent(EventData obj)
    {
        Debug.Log("OnEvent (code: " + obj.Code + "): " + (string)obj.CustomData);
    }
}
