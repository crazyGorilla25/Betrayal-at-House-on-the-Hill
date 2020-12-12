using Mirror;
using System;
using System.Linq;
using UnityEngine;

public class Player : NetworkBehaviour
{
	[SerializeField] GameObject[] allCharacters = new GameObject[12];

	[SyncVar(hook =nameof(AuthorityHandlePartyOwnerStateUpdated))]
	bool isPartyOwner;
	[SyncVar(hook = nameof(ClientHandlePlayerCharacterUpdated))]
	int characterId;
	[SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
	string displayName;

	Character myCharacter;

	public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;
	public static event Action ClientOnPlayerInfoUpdated;

	public string GetDisplayName()
	{
		return displayName;
	}

	#region Client
	public override void OnStartClient()
	{
		if (NetworkServer.active) return;

		DontDestroyOnLoad(gameObject);

		((BetrayalNetworkManager)NetworkManager.singleton).players.Add(this);
	}
	public override void OnStopClient()
	{
		ClientOnPlayerInfoUpdated?.Invoke();

		if (!isClientOnly) return;

		((BetrayalNetworkManager)NetworkManager.singleton).players.Remove(this);
	}

	void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
	{
		if (!hasAuthority) return;

		AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
	}
	void ClientHandlePlayerCharacterUpdated(int oldCharacterId, int newCharacterId)
	{
		ClientOnPlayerInfoUpdated?.Invoke();
	}
	void ClientHandleDisplayNameUpdated(string oldName, string newName)
	{
		ClientOnPlayerInfoUpdated?.Invoke();
	}
	#endregion

	#region Server
	[Server]
	public void SetDisplayName(string newName)
	{
		displayName = newName;
	}
	[Server]
	public void SetCharacter(int characterId)
	{
		myCharacter = allCharacters.SingleOrDefault(cha => cha.GetComponent<Character>().GetId() == characterId).GetComponent<Character>();
	}
	#endregion
}
