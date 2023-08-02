using UnityEngine;

namespace ElementsArena.Movement
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

            //Rotate character
            Quaternion targetRotation = cameraTarget.rotation;
            targetRotation.x = 0;
            targetRotation.z = 0;
            characterMovement.transform.rotation = targetRotation;
        }

        public Transform GetCameraTarget()
        {
            return cameraTarget;
        }

        public void IncrementCameraRotation(Vector2 deltaLook)
        {
            targetLook += deltaLook;
            targetLook.x = Mathf.Clamp(targetLook.x, XCameraRange.x, XCameraRange.y);
        }
    }
}