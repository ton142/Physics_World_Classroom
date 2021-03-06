﻿namespace VRTK {
    using UnityEngine;

    public class TakeScreenshot : MonoBehaviour {
        private int photoCount = 0;
        public AudioSource audio;
        public GameObject leftController;

        void Start() {
            leftController.GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(takePhoto);
        }

        void takePhoto(object sender, ControllerInteractionEventArgs e) {
                audio.Play();
                Application.CaptureScreenshot("Screenshot" + photoCount + ".png");
                photoCount++;
        }
    }
}
