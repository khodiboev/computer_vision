import time
import cv2


def main():
    cascade_path = cv2.data.haarcascades + "haarcascade_frontalface_default.xml"
    face_cascade = cv2.CascadeClassifier(cascade_path)
    if face_cascade.empty():
        print("Xato: Haar Cascade fayli yuklanmadi:", cascade_path)
        return

    cap = cv2.VideoCapture(0)
    if not cap.isOpened():
        print("Xato: kamera ochilmadi. System Settings > Privacy & Security > Camera "
              "bo'limida Terminal (yoki VS Code) uchun ruxsat berilganini tekshiring.")
        return

    prev_time = time.time()
    print("Webcam ishga tushdi. Chiqish uchun oynada 'q' tugmasini bosing.")

    try:
        while True:
            success, frame = cap.read()
            if not success:
                print("Kadr o'qilmadi, to'xtatilmoqda.")
                break

            frame = cv2.flip(frame, 1)
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

            faces = face_cascade.detectMultiScale(
                gray,
                scaleFactor=1.2,
                minNeighbors=5,
                minSize=(60, 60),
            )

            for (x, y, w, h) in faces:
                cv2.rectangle(frame, (x, y), (x + w, y + h), (255, 0, 0), 2)
                cv2.putText(frame, "Face", (x, y - 10),
                            cv2.FONT_HERSHEY_SIMPLEX, 0.6, (255, 0, 0), 2)

            now = time.time()
            fps = 1.0 / max(now - prev_time, 1e-6)
            prev_time = now

            cv2.putText(frame, f"Faces: {len(faces)}", (10, 30),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0, 255, 0), 2)
            cv2.putText(frame, f"FPS: {fps:.1f}", (10, 60),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0, 255, 0), 2)

            cv2.imshow("Face Detection (Haar Cascade)", frame)

            if cv2.waitKey(1) & 0xFF == ord("q"):
                break
    finally:
        cap.release()
        cv2.destroyAllWindows()
        cv2.waitKey(1)
        print("Kamera yopildi.")


if __name__ == "__main__":
    main()
