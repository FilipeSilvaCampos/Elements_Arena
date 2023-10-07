using UnityEngine;

[CreateAssetMenu(fileName = "WaterProjectile", menuName = "Make New Water Projetile", order = 0)]
public class WaterScriptableProjectile : ScriptableObject
{
    public GameObject prefab;
    public bool isGrounded;

    public GameObject Launch(Vector3 position, Quaternion rotation)
    {
        if (isGrounded)
        {
            position.y = GroundHeight(position);
            rotation.x = 0;
        }

        return Instantiate(prefab, position, rotation);
    }

    float GroundHeight(Vector3 observOrigin)
    {
        RaycastHit hit;

        Ray ray = new Ray(observOrigin, Vector3.down);
        Physics.Raycast(ray, out hit);

        return hit.point.y;
    }
}
