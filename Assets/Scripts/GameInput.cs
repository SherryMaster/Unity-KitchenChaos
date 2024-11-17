using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;

    private GameInputSystem gameInputSystem;

    private void Awake() {
        gameInputSystem = new GameInputSystem();
        gameInputSystem.Player.Enable();
        gameInputSystem.Player.Interact.Enable();

        gameInputSystem.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("Interact performed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = gameInputSystem.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }
}