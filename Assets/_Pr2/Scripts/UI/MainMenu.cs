using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Pr2.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void OpenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
