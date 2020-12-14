using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayalNetworkManager : NetworkManager
{
	public List<Player> players { get; } = new List<Player>();

	public static event Action<int> ClientOnConnect;

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		base.OnServerAddPlayer(conn);

		Player player = conn.identity.GetComponent<Player>();

		players.Add(player);

		player.SetCharacterId(1);

		player.SetDisplayName($"Player {numPlayers}");
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		Player player = conn.identity.GetComponent<Player>();

		players.Remove(player);

		base.OnServerDisconnect(conn);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);

		ClientOnConnect?.Invoke(numPlayers);
	}
}
