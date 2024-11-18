using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact() {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint.position, Quaternion.identity);

            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        }
        else {
            Destroy(kitchenObject.gameObject);
            kitchenObject = null;
        }
    }
}
