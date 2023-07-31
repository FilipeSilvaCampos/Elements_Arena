using UnityEngine;

[System.Serializable]
public class Scene
{
    public int id;
    public Spawn[] spawns;
}

[System.Serializable]
public class Spawn
{
    public Vector3 position;
    public Quaternion rotation;
} 