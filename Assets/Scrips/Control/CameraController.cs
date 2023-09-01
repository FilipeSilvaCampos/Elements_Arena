using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Control
{
    [ExecuteAlways]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CharacterMovement characterMovement = null;
        [SerializeField] Transform cameraTarget = null;
        [SerializeField] float targetHeight = 2f;
        [SerializeField] Vector2 XCameraRange = new Vector2(-70, 70);

        Vector2 targetLook;

        public Quaternion LookRotation => cameraTarget.rotation;

        private void LateUpdate()
        {
            cameraTarget.position = characterMovement.transform.position + Vector3.up * targetHeight;
            cameraTarget.rotation = Quaternion.Euler(targetLook.x, targetLook.y, 0);
        }

        public void IncrementCameraRotation(Vector2 deltaLook)
        {
            targetLook += deltaLook;
            targetLook.x = Mathf.Clamp(targetLook.x, XCameraRange.x, XCameraRange.y);
        }

        public Transform GetCameraTarget()
        {
            return cameraTarget;
        }
    }
}