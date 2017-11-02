namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CreatePracticeDots : MonoBehaviour {

        public GameObject templateDot;
        public bool use = true;
        private int count = 0;

        // Use this for initialization
        void Start() {
            if (GetComponent<VRTK_ControllerEvents>() == null) {
                // Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
                return;
            }
            GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        }


        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)          // if Vive trigger is pressed
        {
            if (use) {
                Vector3 position = transform.position;                                              // get position of the controller
                GameObject clone = Instantiate(templateDot, position, transform.rotation);
                clone.SetActive(true);

            }
        }
    }
}
