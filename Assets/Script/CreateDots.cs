namespace VRTK
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class CreateDots : MonoBehaviour
    {
        public GameObject dott;

        //for drawing dots
        //private float distance = 1.0f;
        private float actualDistance;
        public float distance = 0f;
        public TextMesh mText;              // add message in actual scene
        public Material newMaterial;
        public Material startingMaterial;
		public LineRenderer trajectory;

        //for calculating diffference
        public  DrawDots spacedot;
        private int count = 0;

        private void Start()
        {
            if (GetComponent<VRTK_ControllerEvents>() == null)
            {
                return;
            }
            GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        }

        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)          // if Vive trigger is pressed
        {
            if (count < spacedot.dot.Count) {
                dott.GetComponent<Renderer>().material = startingMaterial;
                Vector3 position = transform.position;                                              // get position of the controller
                position.y += 0.05f;                                                                // set the position for the ball to be created 0.05 away from the controller
                Instantiate(dott, new Vector3(position.x, position.y, 0), transform.rotation);  // keep z-position at 0 so that the dots stay along the trajectory line
                displayDots(position, count);
                count++;
                displayTrajectory();
            } else {
                switch (SceneManager.GetActiveScene().name) {
                    case "TestScene":
                        SceneManager.LoadScene("TestAngle30");
                        break;
                    case "TestAngle30":
                        SceneManager.LoadScene("TestSpeed10");
                        break;
                    case "TestSpeed10":
                        SceneManager.LoadScene("TestSpeed20");
                        break;
                    case "TestSpeed20":
                        SceneManager.LoadScene("TestGravity5");
                        break;
                    case "TestGravity5":
                        SceneManager.LoadScene("TestGravity15");
                        break;
                    default:
                        SceneManager.LoadScene("TestScene");
                        break;

                }
            }
        }

        void displayDots(Vector3 position, int index) {
            float dist = 0;
            
            if (spacedot.dot.Count > 0 && index < spacedot.dot.Count) {
                dist = Vector3.Distance(spacedot.dot[index].transform.position, position); // calculate the distance from the trajectory to the input
                distance = dist;
                Debug.Log(dist);
                //mText.text = "Error: " + dist;
                spacedot.dot[index].GetComponent<Renderer>().material = newMaterial; //changes the material from trasnparent to green
            }
        }

		void displayTrajectory(){
			if (count >= spacedot.dot.Count)
				trajectory.material = newMaterial; //changes the line of the trajectory to be visible
		}
	}
}