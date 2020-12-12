using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayalNetworkManager : NetworkManager
{
	public List<Player> players { get; } = new List<Player>();

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		base.OnServerAddPlayer(conn);

		Player player = conn.identity.GetComponent<Player>();

		players.Add(player);

		player.SetDisplayName($"Player {numPlayers}");
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		Player player = conn.identity.GetComponent<Player>();

		players.Remove(player);

		base.OnServerDisconnect(conn);
	}
}
