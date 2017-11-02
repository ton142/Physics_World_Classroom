namespace VRTK {
    using UnityEngine;

    public class ScaleRig : MonoBehaviour {

        public GameObject trajectory;
        public GameObject rightController;
        public GameObject cameraRig;
        public DrawDots drawDots;
        public float fadeDuration;          // through trial and error, 8 seems like a good number

        private Vector3 lengthOfReach;
        private Vector3 scale = new Vector3(1, 1, 1);

        void Start() {
            if (rightController.GetComponent<VRTK_ControllerEvents>() == null) {
                return;
            }
            rightController.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(TallWideEnough);
        }

        private void TallWideEnough(object sender, ControllerInteractionEventArgs e) {
            lengthOfReach = rightController.transform.position; // get the position of where the controller trigger was pressed

            FadeToBlack();                          // start fade to black
            CameraRigGrow();
            trajectory.SetActive(true);             // display Trajectory object
            FadeFromBlack();                        // start fade back to clear

            rightController.GetComponent<CreateDots>().enabled = true;  // allow controller to be able to create dots
            rightController.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= new ControllerInteractionEventHandler(TallWideEnough);
        }

        void CameraRigGrow() {
            while (lengthOfReach.y <= drawDots.maxHeight) { // if the y-position of lengthOfReach is not equal to or greater than the y-position of this object
                cameraRig.transform.localScale += scale;      // increase the scale of the rig by the Scale vector         
                lengthOfReach += scale;                       // update the lengthOfReach vector with the Scale Vector
           }
        }

        // Fading scripts

        private void FadeToBlack() {
            SteamVR_Fade.Start(Color.clear, 0f);            // set start colour
            SteamVR_Fade.Start(Color.black, fadeDuration);  // fade to black
        }

        private void FadeFromBlack() {
            SteamVR_Fade.Start(Color.black, 0f);            // set start colour
            SteamVR_Fade.Start(Color.clear, fadeDuration);  // fade back to clear
        }
    }
}
