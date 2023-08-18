using UnityEngine;

namespace ElementsArena.Core
{
    public class PersistenteObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static GameObject hasSpawned = null;

        private void Awake()
        {
            if (hasSpawned != null) return;

            

            hasSpawned = SpawnPersistentObject(); ;
        }

        GameObject SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            return persistentObject;
        }
    }
}