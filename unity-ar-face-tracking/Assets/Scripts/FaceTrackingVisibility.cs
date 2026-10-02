using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Attach to the face prefab root. Hide visuals when tracking is lost;
// never disable the ARFace root or it cannot receive tracking updates.
[RequireComponent(typeof(ARFace))]
public sealed class FaceTrackingVisibility : MonoBehaviour
{
    private ARFace face;
    private Renderer[] renderers;

    private void Awake()
    {
        face = GetComponent<ARFace>();
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void OnEnable()
    {
        face.updated += OnFaceUpdated;
        RefreshVisibility();
    }

    private void OnDisable()
    {
        face.updated -= OnFaceUpdated;
        SetVisible(false);
    }

    private void OnFaceUpdated(ARFaceUpdatedEventArgs args)
    {
        RefreshVisibility();
    }

    private void RefreshVisibility()
    {
        SetVisible(face.trackingState == TrackingState.Tracking &&
                   ARSession.state == ARSessionState.SessionTracking);
    }

    private void SetVisible(bool visible)
    {
        foreach (Renderer visual in renderers)
            if (visual != null)
                visual.enabled = visible;
    }

    private void Update()
    {
        // Session state may change without a face.updated callback.
        RefreshVisibility();
    }
}
