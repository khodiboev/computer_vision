using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARCameraManager))]
public sealed class UserFacingCamera : MonoBehaviour
{
    private void OnEnable()
    {
        // A request, not a guarantee: the device/provider must support it.
        GetComponent<ARCameraManager>().requestedFacingDirection = CameraFacingDirection.User;
    }
}
