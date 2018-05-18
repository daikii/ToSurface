using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour 
{
	public string roomName = "MTI";

	public bool isJoin = false;

	void Awake ()
	{
		PhotonNetwork.ConnectUsingSettings ("v1.0");

		// synchro speed
		PhotonNetwork.sendRate = 50;
		PhotonNetwork.sendRateOnSerialize = 50;
	}

	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 5 };
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
		Debug.Log ("OnJoinedLobby");
	}

	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
	}

	void Update()
	{
		if (GameObject.Find ("CamTrack(Clone)")) 
		{
			isJoin = true;
		}
	}
}
