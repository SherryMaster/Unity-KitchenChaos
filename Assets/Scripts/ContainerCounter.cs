using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            KitchenObject kitchenObject = player.GetKitchenObject();
            KitchenObjectSO playerKitchenObjectSO = kitchenObject.GetKitchenObjectSO();
            if (playerKitchenObjectSO == kitchenObjectSO) {
                player.ClearKitchenObject();
                Destroy(kitchenObject.gameObject);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
        else {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
