using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    enum Symbols { Event, Omen, Item, Dumbwaiter }
    enum Floors { Basement, Ground, Upper, Roof }

    /*
	List of all room attributes:
		Exits
		Symbols
		End Action (optional/forced)
		Enter Action
		Exit Action
		Cross Action
	*/
    [SerializeField] Symbols[] symbols;
    [SerializeField] string[] exits;
    [SerializeField] Floors[] floors;

    List<RoomAction> actions = new List<RoomAction>();

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponents<RoomAction>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
