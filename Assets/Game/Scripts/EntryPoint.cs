using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExampleClock.Boostrapper.Scripts.Widgets
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            //Settings
            Screen.orientation = ScreenOrientation.AutoRotation;

            //Scene
            SceneManager.LoadScene("Alarm");
        }
    }
}