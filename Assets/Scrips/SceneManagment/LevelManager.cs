using UnityEngine;

namespace ElementsArena.SceneManagement
{
    public class LevelManager : MonoBehaviour
    {
        public Transform[] spawns = null;
        public Camera[] cameras = null;

        public void SetSplitScreen()
        {
            for(int i = 0; i < cameras.Length; i++) 
            {
                cameras[i].gameObject.SetActive(true);
                if (i == 0) cameras[0].rect = new Rect(0, 0.5f, 1, 1);
                else cameras[i].rect = new Rect(0, -0.5f, 1, 1);
            }
        }
    }
}
