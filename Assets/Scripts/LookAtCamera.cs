using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
   private enum Mode {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode = Mode.LookAt;

    public void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0);
                break;
            case Mode.CameraForward:
                transform.rotation = Camera.main.transform.rotation;
                break;
            case Mode.CameraForwardInverted:
                transform.rotation = Camera.main.transform.rotation;
                transform.Rotate(0, 180, 0);
                break;
        }
    }
}
