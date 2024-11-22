using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image bar;

    public void Start() {
        cuttingCounter.OnCutProgressChanged += CuttingCounter_OnCutProgressChanged;

        bar.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnCutProgressChanged(object sender, CuttingCounter.CutProgressChangedEventArgs e) {
        bar.fillAmount = e.cutProgressNormalized;

        if (e.cutProgressNormalized == 1f || e.cutProgressNormalized == 0f) {
            Hide();
        }
        else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
