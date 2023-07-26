using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    private const string PLAYER_PREFS_BINDINGS = "IOnputBindings";

    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseInteraction;

    public enum Bindings {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate
    }


    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseInteraction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetKeyBinding(Bindings key) {
        string keyBindingString = "";

        switch (key) {
            case Bindings.Interact:
                keyBindingString = playerInputActions.Player.Interact.GetBindingDisplayString();
                break; 
            case Bindings.InteractAlternate:
                keyBindingString = playerInputActions.Player.InteractAlternate.GetBindingDisplayString();
                break;
            case Bindings.MoveUp:
                keyBindingString = playerInputActions.Player.Move.bindings[1].ToDisplayString();
                break;
            case Bindings.MoveDown:
                keyBindingString = playerInputActions.Player.Move.bindings[2].ToDisplayString();
                break;
            case Bindings.MoveLeft:
                keyBindingString = playerInputActions.Player.Move.bindings[3].ToDisplayString();
                break;
            case Bindings.MoveRight:
                keyBindingString = playerInputActions.Player.Move.bindings[4].ToDisplayString();
                break;
        }

        return keyBindingString;
    }

    public void SetNewKeyBinding(Bindings key, Action callbackAction) {
        playerInputActions.Player.Disable();

        InputAction inputAction = new();
        int bindingIndex = 0;

        switch (key) {
            case Bindings.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Bindings.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputActions.Player.Enable();
                callbackAction();
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}