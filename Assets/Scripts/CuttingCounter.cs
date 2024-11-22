using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public event EventHandler<CutProgressChangedEventArgs> OnCutProgressChanged;
    public event EventHandler OnCut;
    public class CutProgressChangedEventArgs : EventArgs {
        public float cutProgressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cutProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cutProgress = 0;
                    OnCutProgressChanged?.Invoke(this, new CutProgressChangedEventArgs { cutProgressNormalized = GetCutProgressNormalized() });
                }
            }
        }
        else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            cutProgress++;
            float cutProgressMax = GetCutProgress();
            OnCut?.Invoke(this, EventArgs.Empty);
            OnCutProgressChanged?.Invoke(this, new CutProgressChangedEventArgs { cutProgressNormalized = GetCutProgressNormalized() });
            if (cutProgress >= cutProgressMax) {
                KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(kitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        else {
            return null;
        }
    }


    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO kitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == kitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    private int GetCutProgress() {
        return GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO()).cutProgrogressMax;
    }

    private float GetCutProgressNormalized() {
        return (float)cutProgress / GetCutProgress();
    }
}
