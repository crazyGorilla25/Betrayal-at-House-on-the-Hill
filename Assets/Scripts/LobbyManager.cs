using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
	[SerializeField] Character[] characterPrefabs = new Character[12];
	[SerializeField] Text[] playerNameTexts = new Text[6];
	[SerializeField] Image[] playerCharacterIcons = new Image[6];
	[SerializeField] GameObject[] infoBackgrounds = new GameObject[6];
	[SerializeField] GameObject[] infoObjects = new GameObject[6];
	[SerializeField] Button startButton = null;

	private void OnEnable()
	{
		Player.ClientOnDisplayNameUpdated += ClientHandleDisplayNameUpdated;
		Player.ClientOnCharacterIdUpdated += ClientHandleCharacterIdUpdated;
		Player.ClientOnStartPlayer += ClientHandleStart;
		Player.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerUpdated;
	}

	private void OnDisable()
	{
		Player.ClientOnDisplayNameUpdated -= ClientHandleDisplayNameUpdated;
		Player.ClientOnCharacterIdUpdated -= ClientHandleCharacterIdUpdated;
		Player.ClientOnStartPlayer -= ClientHandleStart;
		Player.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerUpdated;
	}

	void ClientHandleStart()
	{
		List<Player> players = ((BetrayalNetworkManager)NetworkManager.singleton).players;
		for (int i = 0; i < infoObjects.Length; i++)
		{
			Button[] buttons = infoObjects[i].GetComponentsInChildren<Button>();
			foreach(Button button in buttons)
			{
				if (i < players.Count && i >= 0)
				{
					button.interactable = players[i].hasAuthority;
				}
			}
		}
	}
	void AuthorityHandlePartyOwnerUpdated(bool state)
	{
		startButton.gameObject.SetActive(state);
	}
	public void StartGame()
	{
		NetworkClient.connection.identity.GetComponent<Player>().CmdStartGame();
	}

	void ClientHandleCharacterIdUpdated()
	{
		List<Player> players = ((BetrayalNetworkManager)NetworkManager.singleton).players;

		bool characterTaken = false;

		for (int i = 0; i < players.Count; i++)
		{
			int playerCharacterId = players[i].GetCharacterId();

			for (int j = 0; j < players.Count; j++)
			{
				if (j == i) continue;

				if (Mathf.FloorToInt(players[j].GetCharacterId()/2)*2 == Mathf.FloorToInt(playerCharacterId/2)*2)
				{
					characterTaken = true;
				}
			}

			foreach (Character character in characterPrefabs)
			{
				if (character.GetId() == playerCharacterId)
				{
					playerCharacterIcons[i].sprite = character.gameObject.GetComponent<SpriteRenderer>().sprite;
				}
			}

			playerCharacterIcons[i].color = characterTaken ? new Color(.5f, .5f, .5f) : new Color(1, 1, 1);

			startButton.interactable = players.Count >= 3 && !characterTaken;
		}
	}

	void ClientHandleDisplayNameUpdated()
	{
		List<Player> players = ((BetrayalNetworkManager)NetworkManager.singleton).players;

		for (int i = 0; i<players.Count; i++)
		{
			playerNameTexts[i].text = players[i].GetDisplayName();

			infoBackgrounds[i].SetActive(false);

		}

		for(int i =players.Count; i < playerNameTexts.Length; i++)
		{
			playerNameTexts[i].text = "Waiting for player...";
			infoBackgrounds[i].SetActive(true);
		}
	}

	public void LobbyChangeCharacter(int idChange)
	{
		List<Player> players = ((BetrayalNetworkManager)NetworkManager.singleton).players;

		for(int i = 0; i< players.Count; i++)
		{
			if (players[i].hasAuthority)
			{
				int playerCharacterId = players[i].GetCharacterId();

				int newId = playerCharacterId + idChange;
				
				if(newId > 11)
				{
					newId -= 12;
				} else if (newId < 0)
				{
					newId += 12;
				}

				foreach (Character character in characterPrefabs)
				{
					if (character.GetId() == newId)
					{
						playerCharacterIcons[i].sprite = character.gameObject.GetComponent<SpriteRenderer>().sprite;
					}
				}

				players[i].CmdTrySetCharacterId(newId);
			}
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
