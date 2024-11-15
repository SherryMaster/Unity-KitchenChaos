using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;

    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 inputvector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputvector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputvector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputvector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputvector.x += 1;
        }

        inputvector.Normalize();

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
