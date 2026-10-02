# Unity AR Face Tracking - Android coursework reconstruction

An AR Foundation / ARCore exercise: track a face with the front camera,
attach a face mask or accessories, and tap the screen to cycle face materials.
This is separate from the repository's Python/OpenCV webcam detector.

## Provenance and current status

- Jurabek completed the original professor-guided face-tracking exercise on a phone.
- These C# scripts were newly reconstructed with AI assistance from the supplied
  `14_ARfoundation-facetracking_v4.pdf` lesson and Unity's public documentation.
  They are not a recovered copy of the original classroom Unity project.
- **This reconstructed version has not been compiled in Unity or tested on a device.**
- This folder is a scripts-and-setup starter, not a complete import-and-build Unity
  project: scenes, prefabs, materials and XR project settings must be configured below.
- The professor's PDFs and third-party model assets are not redistributed here.
- The separate plane-detection / raycasting lesson (13) is not claimed as implemented.

## Historical lesson environment

| Component | Lesson version |
|---|---|
| Unity Editor | 2020.3.34f1 |
| AR Foundation | 4.1.13 |
| ARCore XR Plugin | 4.1.13 |
| XR Plugin Management | 4.0.1 |
| Target | Compatible Android phone, front camera |

These are the lesson's historical versions, not current production recommendations.
The scripts target the AR Foundation 4.1 API (`AR Session Origin`, `facesChanged`).
Newer Unity/AR Foundation releases may require migration.

## Scripts

| Script | Attach to | Responsibility |
|---|---|---|
| `ARFaceSwitch.cs` | AR Session Origin | Cycle face mesh materials on touch; apply current material to newly detected faces |
| `FaceTrackingVisibility.cs` | Face prefab root | Hide mesh/accessory renderers while face/session tracking is unavailable |
| `UserFacingCamera.cs` | AR Camera | Request the front/user-facing camera |

## Create the Unity project

1. Create a new 3D project in the lesson-compatible editor. Install Android Build
   Support (SDK, NDK and OpenJDK) through Unity Hub.
2. Install the package versions above in Package Manager. Enable **ARCore** under
   Project Settings > XR Plug-in Management > Android.
3. Copy `Assets/Scripts` from this folder into the new project's `Assets` folder.
4. Remove the default Main Camera. Add **AR Session** and **AR Session Origin**
   from GameObject > XR. Its AR Camera is the only scene camera.
5. Add **AR Face Manager** and **ARFaceSwitch** to AR Session Origin.
6. On AR Camera, set AR Camera Manager > Facing Direction to **User** and add
   **UserFacingCamera**. Do not enable plane/image tracking for this face scene.

## Build the face prefab

1. Create an **AR Default Face** from GameObject > XR. Keep its ARFace,
   ARFaceMeshVisualizer, MeshFilter and MeshRenderer components.
2. Add **FaceTrackingVisibility** to the prefab root.
3. Create two or more materials using a mobile-compatible shader, with distinct
   colors. Assign the initial material to the face MeshRenderer.
4. Drag AR Default Face into an `Assets/Prefabs` folder, then remove the scene
   instance. Assign this prefab to AR Face Manager > **Face Prefab**.
5. On **ARFaceSwitch**, populate the **Face Materials** array with your materials
   in the order you want to cycle. Use the legacy Input Manager for the touch script.

Expected hierarchy:

```text
Scene
  AR Session
  AR Session Origin [ARFaceManager, ARFaceSwitch]
    AR Camera [ARCameraManager, UserFacingCamera, other default AR camera components]

Face prefab
  AR Default Face [ARFace, ARFaceMeshVisualizer, MeshRenderer, FaceTrackingVisibility]
    optional accessory meshes (locally created or appropriately licensed)
```

### Optional fox-style accessories

For a minimal independent reconstruction, create simple accessory meshes as
children of the face prefab, position them in local face space and tune their
scale/orientation on a device. Children follow the tracked face transform.
This does **not** implement independent facial landmark/ear-region tracking.

The classroom lesson instead used the Google ARCore sample's `fox_sample.fbx`
and materials, and optionally `canonical_face_mesh.fbx`. Obtain those separately
from [Google's source repository](https://github.com/google-ar/arcore-unity-sdk),
review the applicable license/asset notices, and configure the prefab/materials
in Unity. They are not included here. Do not import the legacy SDK runtime on
top of AR Foundation; the lesson uses its model/material assets as references.

## Android build and device check

1. Save the scene and add it to Build Settings > Scenes In Build.
2. Select Android > Switch Platform. Set a unique application identifier.
3. Following the historical lesson: remove Vulkan, use OpenGLES3, set minimum
   API level 24 or higher. Choose a target SDK supported by your installed tools;
   this is a local test build, not a Play Store submission.
4. Confirm ARCore is enabled. For a device build choose IL2CPP and ARM64 with the
   required NDK/toolchain installed. Use a phone supporting the required ARCore features.
5. Connect the phone with USB debugging enabled, Build And Run, and grant camera
   permission on the device. Do not commit signing keys or build credentials.

### Manual acceptance checklist - pending for this reconstruction

- [ ] Unity compiles without errors using the documented package versions.
- [ ] Android install succeeds and front camera opens after permission is granted.
- [ ] Face mesh/accessories follow head movement.
- [ ] One screen tap switches the face mesh material once.
- [ ] A newly detected face receives the currently selected material.
- [ ] Accessories retain their own materials during mask changes.
- [ ] Visuals hide when face tracking is lost and reappear when it returns.
- [ ] Permission denied / unsupported device behavior is checked.
- [ ] Record the actual Unity version, device model, Android version and results.

No APK, original screenshot or successful-build claim is supplied for this reconstruction.
The portfolio's Unity images are illustrations, not evidence of this new code running.

## Troubleshooting

- Rear camera opens: inspect User-facing camera request and device support.
- No face: check lighting, camera permission, ARCore support and Face Prefab assignment.
- Taps do nothing: fill the material array and enable the legacy Input Manager.
- No visible mask: check shader compatibility and ARFaceMeshVisualizer/MeshRenderer.
- Scripts fail with newer packages: use the historical versions or migrate the API.

## References and attribution

- Professor-provided `14_ARfoundation-facetracking_v4.pdf`, used privately as a lesson reference.
- [Unity AR Foundation 4.1 face manager documentation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.1/manual/face-manager.html).
- [Google ARCore Unity samples](https://github.com/google-ar/arcore-unity-sdk).

Classroom exercise and reconstruction, not a proprietary face-recognition model.
This module does not identify people and does not intentionally upload camera frames;
review the SDK/platform behavior and permissions before distributing a built app.
