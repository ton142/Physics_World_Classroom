using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawDots : MonoBehaviour {

    public float speed = 15.0f; // the initial speed in m/s
    public float angle = 45.0f;// give an initial angle in degrees
    public float gravity = Physics.gravity.magnitude;

    public GameObject variableText;
    public GameObject titleText; // display how many points along trajectory
    public GameObject path;
    public GameObject startPoint;
    public GameObject dott; //creating dots

    // used by other classes
    public List<GameObject> dot = new List<GameObject>();   // found in CreateDots
    //public float maxDistance;                               // found in ScaleRig; DON'T CHANGE ON INSEPCTOR
    public float maxHeight;                                 // found in ScaleRig
    

    private int addDot = 0;
    private Vector3 start; // start points
    private Vector3 end; // end points
    private float angle_r = 0.0f;// angle_r in radiant
    private float timeResolution = 0.02f;
    private float maxTime = 0.0f;
    private float Velocity_Y = 0.0f;
    private float Velocity_X = 0.0f;
    private int index = 0;
    //private float gravity = Physics.gravity.magnitude;
    private List<Vector3> positions = new List<Vector3>();
    private LineRenderer trajectory;
    private bool startDrawing;

    // Use this for initialization
    void Start() {
        start = startPoint.transform.position;
        //first get the radiant
        angle_r = (angle / 180.0f) * Mathf.PI;
        Velocity_X = speed * Mathf.Cos(angle_r);
        Velocity_Y = speed * Mathf.Sin(angle_r);

        maxTime = (2.0f * Velocity_Y) / 9.81f;
        startPoint.transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);

        startDrawing = true;
    }

    void Update() {
        if (startDrawing)
            drawDots(); // checks if startDrawing is true

        displayText();
    }

    void displayText() {
        variableText.GetComponent<Text>().text = "\nVariables:\n Velocity = " + speed + "m/s \n Angle = " + angle + " degrees \n Gravity = " + gravity + " N";
        titleText.GetComponent<Text>().text = "Can you guess " + addDot + " points along this unknown trajectory?"; // allow the # of points to change depending on tha trajectory
    }

    void drawDots() {
        // is there a way to keep it to just 5 points no matter what trajectory?
        timeResolution += 0.25f;

        float dx = speed * timeResolution * Mathf.Cos(angle * Mathf.Deg2Rad);
        float dy = speed * timeResolution * Mathf.Sin(angle * Mathf.Deg2Rad) - (gravity * timeResolution * timeResolution / 2.0f);

        Vector3 currentPosition = new Vector3(start.x + dx, start.y + dy, 0); //change from 0 to 2
        positions.Add(currentPosition);


        if (positions[index].y <= start.y && index > 1) {       //makes sure that the points are drawn till the position is <= the starting y-position
            drawTrajectory();

            //maxDistance = positions[index - 1].x; // distance from start point to the last dot
            maxHeight = positions[index / 2].y;
            return;
        }

        if (index % 2 == 0) {
            GameObject newDot = Instantiate(dott, currentPosition, transform.rotation); // draw all dots that that have index divisble by 2
            dot.Add(newDot);
            addDot++;
        }

        index++;
        Debug.Log(index);
    }

    void drawTrajectory() {
        int count = 0;
        trajectory = path.GetComponent<LineRenderer>();

        Vector3[] coordinates = new Vector3[positions.Count];
        trajectory.positionCount = index;
        positions.CopyTo(coordinates);

        while (count < index) {
            trajectory.SetPosition(count, coordinates[count]);
            count++;
        }

        startDrawing = false;
    }
}