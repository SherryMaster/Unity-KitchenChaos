using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }


    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactDistance = 2f;
    [Space]
    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask counterLayerMask;

    private bool isMoving = false;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Awake() {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter) {
            selectedCounter.Interact();
        }
    }

    // Update is called once per frame
    void Update() {
        HandleMovements();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isMoving;
    }

    public void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter counter)) {
                //counter.Interact();
                if (selectedCounter != counter)
                {
                    SetSelectedCounter(counter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }

    private void HandleMovements() {
        Vector2 inputvector = gameInput.GetInputVectorNormalized();

        Vector3 movDir = new Vector3(inputvector.x, 0, inputvector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);

        if (!canMove) {
            Vector3 movDirX = new Vector3(movDir.x, 0, 0);
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirX, moveDistance);

            Vector3 movDirZ = new Vector3(0, 0, movDir.z);
            bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirZ, moveDistance);

            if (canMoveX && !canMoveZ) {
                movDir = movDirX;
                canMove = true;
            }
            else if (!canMoveX && canMoveZ) {
                movDir = movDirZ;
                canMove = true;
            }
            else {
                canMove = false;
            }
        }

        if (canMove) {
            isMoving = movDir != Vector3.zero;
            transform.position += movDir * moveSpeed * Time.deltaTime;

            if (movDir != Vector3.zero) {
                float rotateSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
            }
        }
        else {
            isMoving = false;
        }
    }

    private void SetSelectedCounter(ClearCounter counter) {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs { selectedCounter = counter });
    }
}
