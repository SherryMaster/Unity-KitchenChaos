using System;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] visualObjArr;

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
        foreach (GameObject visualObj in visualObjArr) {
            visualObj.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject visualObj in visualObjArr) {
            visualObj.SetActive(false);
        }
    }
}
