using UnityEngine;
using UnityEngine.XR.ARFoundation;

// Coursework reconstruction for AR Foundation 4.1.x.
// Changes only the dynamic face mesh, not the accessory materials.
[RequireComponent(typeof(ARFaceManager))]
public sealed class ARFaceSwitch : MonoBehaviour
{
    [SerializeField] private Material[] faceMaterials;
    private ARFaceManager faceManager;
    private int materialIndex;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    private void OnEnable()
    {
        faceManager.facesChanged += OnFacesChanged;
        foreach (ARFace face in faceManager.trackables)
            ApplyMaterial(face);
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFacesChanged;
    }

    private void Update()
    {
        // One transition per touch gesture, even with multiple fingers.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            SwitchFaces();
    }

    public void SwitchFaces()
    {
        if (faceMaterials == null || faceMaterials.Length == 0)
            return;
        materialIndex = (materialIndex + 1) % faceMaterials.Length;
        foreach (ARFace face in faceManager.trackables)
            ApplyMaterial(face);
    }

    private void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        // Faces first detected after a tap receive the current material too.
        foreach (ARFace face in args.added)
            ApplyMaterial(face);
    }

    private void ApplyMaterial(ARFace face)
    {
        if (faceMaterials == null || faceMaterials.Length == 0 ||
            faceMaterials[materialIndex] == null)
            return;
        ARFaceMeshVisualizer mesh = face.GetComponentInChildren<ARFaceMeshVisualizer>(true);
        if (mesh == null)
            return;
        MeshRenderer meshRenderer = mesh.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.sharedMaterial = faceMaterials[materialIndex];
    }
}
