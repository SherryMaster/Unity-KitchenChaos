using System;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private ClearCounter counter;
    [SerializeField] private GameObject visualObj;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == counter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show()
    {
        visualObj.SetActive(true);
    }

    private void Hide() {
        visualObj.SetActive(false);
    }
}
