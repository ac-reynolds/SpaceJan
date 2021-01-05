using System.Collections.Generic;
using UnityEngine;

/*
 * Global input handler that handles all input.  Every Update(), it will take input and effect the
 * appropriate changes in game state.  
 * 
 * Place a single instance of this script on any permanent object in the game. 
 */
public class InputHandler : MonoBehaviour
{

    //for handling inputs other than default movement
    public enum InputAction
    {
        Interact
    }
    private static Dictionary<InputAction, KeyCode> keyBinds = new Dictionary<InputAction, KeyCode> {
        { InputAction.Interact,   KeyCode.E }
    };

    //controller currently being manipulated by player movement commands
    private PlayerController _activeController;

    /*
     * As soon as this object is created (before Start()s are called), register this object to listen for
     * new SetActiveControllerEvent instances.
     */
    private void OnEnable() {
        EventHandler<SetActiveControllerEvent>.GetInstance().AddListener(OnSetActiveControllerEvent);
    }
    private void OnDisable() {
        EventHandler<SetActiveControllerEvent>.GetInstance().RemoveListener(OnSetActiveControllerEvent);
    }

    /*
     * Every frame, get vertical/horizontal inputs (WASD by default) and pass them to the player controller.
     */
    private void Update() {
        if (_activeController == null) {
            Debug.Log("Input handler has no active controller.  Invoke SetActiveControllerEvent to set the active controller.");
        } else {
            _activeController.RequestMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void OnSetActiveControllerEvent(SetActiveControllerEvent e) {
        _activeController = e.Controller;
    }
}
