using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
	[SerializeField] GameObject[] characterPrefabs = new GameObject[12];
	[SerializeField] Text[] playerNameTexts = new Text[6];

	private void OnEnable()
	{
		Player.ClientOnPlayerInfoUpdated += ClientHandlePlayerInfoUpdated;
	}

	private void OnDisable()
	{
		Player.ClientOnPlayerInfoUpdated -= ClientHandlePlayerInfoUpdated;
	}

	void ClientHandlePlayerInfoUpdated()
	{
		List<Player> players = ((BetrayalNetworkManager)NetworkManager.singleton).players;

		Debug.Log(players.Count);

		for (int i = 0; i<players.Count; i++)
		{
			playerNameTexts[i].text = players[i].GetDisplayName();
		}

		for(int i  =players.Count; i < playerNameTexts.Length; i++)
		{
			playerNameTexts[i].text = "Waiting for player...";
		}
	}

	public void LeaveLobby()
	{
		if (NetworkServer.active && NetworkClient.isConnected)
		{
			NetworkManager.singleton.StopHost();
		}
		else
		{
			NetworkManager.singleton.StopClient();

			SceneManager.LoadScene(0);
		}
	}
}
