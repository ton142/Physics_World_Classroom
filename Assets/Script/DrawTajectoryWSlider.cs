using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawTajectoryWSlider : MonoBehaviour 
{
    //public
    public float speed; // the initial speed
    public Vector3 start; // start points
    public Vector3 end; // end points
    public float angle;// give an initial angle
    public float angle_r = 0.0f;// angle_r in radiant
    public float timeResolution = 0.02f;
    public float maxTime = 0.0f;
    public GameObject other;
    public List<GameObject> dot = new List<GameObject>();
    public GameObject dott; //creating dots

    //for drawing dotts
    public float distance = 1.0f;
    public bool useInitalCameraDistance = false;
    private float actualDistance;

    //private
    private Vector3 Velocity_Vector;

    //private Sphere sphere;
    private float Velocity_Y = 0.0f;
    private float Velocity_X = 0.0f;
    private int index = 0;

	// determine whether to start drawing dots
	public bool startDrawing;
	public bool angleChanged;

    // Use this for initialization
    void Start()
    {
        //Distance_calculation(); // calculate the distance to the reference point;
        start = other.transform.position;
        //other.transform.
        //first get the radiant
        angle_r = (angle / 180.0f) * Mathf.PI;
        //Debug.Log(angle_r);
        Velocity_X = speed * Mathf.Cos(angle_r);
        Velocity_Y = speed * Mathf.Sin(angle_r);
        //Debug.Log (Velocity_X);

        maxTime = (2.0f * Velocity_Y) / 9.81f;
        //Debug.Log (maxTime);
        end = new Vector3(maxTime * Velocity_X, 0, 0);
        other.transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);

		startDrawing = false;
		angleChanged = false;
    }

	void Update(){
		if (startDrawing)
			drawDots (); // checks if startDrawing is true

		if (angleChanged) {
			for (int i = 0; i < dot.Count; i++)
				DestroyObject (dot [i]); // if the angle has changed, delete previous objects

			startDrawing = true;	// create new ones
			index = 0;				// reset counter
			angleChanged = false;	// reset angle changed
			timeResolution = 0.02f; // reset time resolution
		}
	}

    // Update is called once per frame
    public void drawDots()
    {
        int maxIndex = 10 * (int)(maxTime / timeResolution);
        //Debug.Log (maxIndex);
        timeResolution += 0.3f;

		if (index > maxIndex) {
			startDrawing = false;
			return;
		}
        float dx = speed * timeResolution * Mathf.Cos(angle * Mathf.Deg2Rad);
        float dy = speed * timeResolution * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics.gravity.magnitude * timeResolution * timeResolution / 2.0f);
        Vector3 currentPosition = new Vector3(start.x + dx, start.y + dy, 0); //change from 0 to 2
                                                                              //Instantiate(dott,currentPosition,transform.rotation);

        if ((index % 2) == 0)
        {
            dot.Add(Instantiate(dott, currentPosition, transform.rotation));
        }
        Debug.Log(index);
        index++;
    }
}


