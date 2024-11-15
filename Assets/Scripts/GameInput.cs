using UnityEngine;

public class GameInput : MonoBehaviour
{

    private GameInputSystem gameInputSystem;

    private void Awake() {
        gameInputSystem = new GameInputSystem();
        gameInputSystem.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = gameInputSystem.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }
}
