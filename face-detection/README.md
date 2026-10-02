# Real-time Webcam Face Detection

Python/OpenCV Haar Cascade detection with mirrored webcam video, multi-face bounding
boxes, face counter and FPS overlay. Separate from [Unity AR tracking](../unity-ar-face-tracking/).

## Demonstrated behavior

- Detects multiple faces in the laptop webcam stream.
- Reported ~30 FPS on the author's laptop; speed varies with hardware and lighting.
- Also tested with a photo displayed on a phone held in front of the laptop webcam.
  The detector runs on the laptop, not the phone.
- Detects face-shaped regions, not identity or liveness: displayed photos can be detected too.

## Run locally

From the repository root on macOS/Linux:

```bash
cd face-detection
python3 -m venv .venv
source .venv/bin/activate
python -m pip install -r requirements.txt
python face_detection_webcam.py
```

Windows: use `py -m venv .venv` and `.venv\Scripts\activate` instead.
Grant OS camera access to your terminal/editor when prompted.
Press **q** with the OpenCV window focused to exit and release the camera.

## How it works

`cv2.VideoCapture(0)` opens the default local camera. Frames are mirrored, converted
to grayscale and processed using OpenCV's bundled frontal-face Haar cascade.
Blue boxes mark detections. This script contains no network/upload calls.

## Limitations

- GitHub opens source code, not a running camera demo; this is a local Python app.
- Small, rotated, obscured or poorly lit faces may be missed; false positives are possible.
- FPS is frame-loop timing, not a rigorous cross-device benchmark.
- If the camera fails, check permissions, close other camera apps and check camera index.

Existing dependency pins are preserved. The relocation was not retested with a live camera.

[Back to all projects](../README.md)
