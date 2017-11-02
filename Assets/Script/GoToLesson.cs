    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GoToLesson : MonoBehaviour {
    public string sceneName = "TestScene";    

        void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                SceneManager.LoadScene(sceneName);    // add more scenes
            }
        }
    }
