namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class ControlReactor : MonoBehaviour
    {
        public TextMesh go;
		public int isAngle;
        public Control3DEventArgs eValue;
        private VRTK_Control_UnityEvents controlEvents;

		public int angle;
		public int speed;

        private void Start()
        {
            controlEvents = GetComponent<VRTK_Control_UnityEvents>();
            if (controlEvents == null)
            {
                controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
            }

            controlEvents.OnValueChanged.AddListener(HandleChange);
			angle = 0;
			speed = 0;
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            go.text = e.value.ToString() + "(" + e.normalizedValue.ToString() + "%)";
			if (isAngle == 1)
				angle = (int)e.value;
			else
				speed = (int)e.value;
        }
    }
}