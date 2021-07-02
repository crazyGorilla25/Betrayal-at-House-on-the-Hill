using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomAction : MonoBehaviour
{
    public enum ActionTrigger {Enter, Exit, EndTurn, Cross };

    [SerializeField] bool forced = false;
    [SerializeField] ActionTrigger actionTrigger;
    [SerializeField] UnityEvent onAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
