namespace VRTK
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using System;

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
         // to display the final score
        private int count = 0;
        private List<Vector3> redDots = new List<Vector3>();

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
                redDots.Add(new Vector3(position.x, position.y, 0)); 
                displayDots(position, count);
                //for (int i = 0; i < redDots.Count; i++) { 
                   // Debug.Log(redDots[i].ToString("F4")); // print out the position of each new guess
                   // Debug.Log("redDots length:" + redDots.Count.ToString()); // print out the length of the array of guesses
                   // Debug.Log("score:" + errorMargin(redDots[count], spacedot.positions[count]).ToString()); // print out the score of the previous guess
                //}
                count++;
                displayTrajectory();

                if (count == spacedot.dot.Count) {
                    Debug.Log("final score:" + calculateScore().ToString());
                    displayText();
                }


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

        float calculateScore() {
            float score = 0;
            for (int i = 0; i < redDots.Count; i++) {
                score += errorMargin(redDots[i], spacedot.positions[i]);
            }
            return (score / redDots.Count);
        }

        void displayText() {
            spacedot.scoreText.GetComponent<Text>().text = "Your guesses were an average of " + Math.Round(calculateScore(),2) + " meters off!";
        }

        float errorMargin(Vector3 correctPosition, Vector3 guessPosition) {
            float x_margin = correctPosition.x - guessPosition.x;
            float y_margin = correctPosition.y - guessPosition.y;
            float z_margin = correctPosition.z - guessPosition.z;

            return (Mathf.Sqrt(x_margin*x_margin + y_margin*y_margin + z_margin*z_margin));

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