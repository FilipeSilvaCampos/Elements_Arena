using UnityEngine;

namespace ElementsArena.Prototype
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform[] spawns;
        [SerializeField] Camera[] cameras;

        public Transform GetSpawn(int playerIndex)
        {
            if (playerIndex < spawns.Length) return spawns[playerIndex];
            else return spawns[0];
        }

        public Camera GetCamera(int playerIndex)
        {
            if (playerIndex < cameras.Length) return cameras[playerIndex];
            else return cameras[0];
        }
    }
}
