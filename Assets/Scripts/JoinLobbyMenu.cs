using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
	[SerializeField] InputField addressInput= null;
	[SerializeField] Button joinButton = null;
	public void Join()
	{
		string address = addressInput.text;

		NetworkManager.singleton.networkAddress = address;
		NetworkManager.singleton.StartClient();

		joinButton.interactable = false;
	}
}
