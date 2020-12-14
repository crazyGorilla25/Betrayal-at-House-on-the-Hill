using Mirror;
using System;
using System.Linq;
using UnityEngine;

public class Player : NetworkBehaviour
{
	[SerializeField] Character[] allCharacters = new Character[12];

	[SyncVar(hook =nameof(AuthorityHandlePartyOwnerStateUpdated))]
	bool isPartyOwner;
	[SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
	string displayName = "";
	[SyncVar(hook = nameof(ClientHandlePlayerCharacterIdUpdated))]
	[SerializeField] int myCharacterId;

	Character myCharacter;

	public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;
	public static event Action ClientOnDisplayNameUpdated;
	public static event Action ClientOnCharacterIdUpdated;
	public static event Action ClientOnStartPlayer;

	public string GetDisplayName()
	{
		return displayName;
	}

	public Character GetCharacter()
	{
		foreach(Character character in allCharacters)
		{
			if (character.GetId() == myCharacterId)
			{
				return character;
			}
		}

		return null;
	}

	public int GetCharacterId()
	{
		return myCharacterId;
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
		ClientOnDisplayNameUpdated?.Invoke();

		if (!isClientOnly) return;

		((BetrayalNetworkManager)NetworkManager.singleton).players.Remove(this);
	}

	public override void OnStartLocalPlayer()
	{
		ClientOnStartPlayer?.Invoke();
	}

	void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
	{
		if (!hasAuthority) return;

		AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
	}
	void ClientHandlePlayerCharacterIdUpdated(int oldId, int newId)
	{
		ClientOnCharacterIdUpdated?.Invoke();
	}
	void ClientHandleDisplayNameUpdated(string oldName, string newName)
	{
		ClientOnDisplayNameUpdated?.Invoke();
	}
	#endregion

	#region Server
	public override void OnStartServer()
	{
		DontDestroyOnLoad(gameObject);
	}

	[Server]
	public void SetDisplayName(string newName)
	{
		displayName = newName;
	}
	[Server]
	public void SetCharacter(int characterId)
	{
		foreach(Character character in allCharacters)
		{
			if(character.GetId() == characterId)
			{
				myCharacter = character;
				break;
			}
		}
	}
	[Server]
	public void SetCharacterId(int characterId)
	{
		myCharacterId = characterId;
	}
	[Command]
	public void CmdTryChangeCharacter(int characterId)
	{
		bool characterTaken = false;
		foreach(Player player in ((BetrayalNetworkManager)NetworkManager.singleton).players)
		{
			if (player == this) continue;

			if (Mathf.FloorToInt(player.GetCharacterId() / 2) * 2 == Mathf.FloorToInt(characterId / 2) * 2)
			{
				characterTaken = true;
			}
		}
		if (characterTaken) return;

		SetCharacter(characterId);
	}
	[Command]
	public void CmdTrySetCharacterId(int characterId)
	{
		if (characterId < 0 || characterId > 11) return;

		SetCharacterId(characterId);
	} 
	#endregion
}
