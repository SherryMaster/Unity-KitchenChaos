using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;

    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 inputvector = gameInput.GetInputVectorNormalized();

        Vector3 movDir = new Vector3(inputvector.x, 0, inputvector.y);
        isMoving = movDir != Vector3.zero;

        transform.position += movDir * moveSpeed * Time.deltaTime;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isMoving;
    }
}
