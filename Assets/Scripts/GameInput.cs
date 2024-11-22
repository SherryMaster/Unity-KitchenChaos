using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private GameInputSystem gameInputSystem;

    private void Awake() {
        gameInputSystem = new GameInputSystem();
        gameInputSystem.Player.Enable();
        gameInputSystem.Player.Interact.Enable();

        gameInputSystem.Player.Interact.performed += Interact_performed;
        gameInputSystem.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = gameInputSystem.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }
}