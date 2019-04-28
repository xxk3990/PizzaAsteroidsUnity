using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    // Unnecessary
    //public float speed;                       // Speed of the vehicle, not needed anymore

    // Necessary
    public float accelRate;                     // Small, constant rate of acceleration
    public Vector3 vehiclePosition;             // Local vector for movement calculation
    public Vector3 direction;                   // Way the vehicle should move
    public Vector3 velocity;                    // Change in X and Y
    public Vector3 acceleration;                // Small accel vector that's added to velocity
    public float angleOfRotation;               // 0 
    public float maxSpeed;                      // 0.5 per frame, limits mag of velocity
    public float decelRate = 0.9f;                 //0.9f, set in unity
    float totalCamHeight;
    float totalCamWidth;
    Camera camera;
    public GameObject bulletPrefab;
    public List<GameObject> laserList;
    bool shot = true;
    float shotClock;
    

    // Use this for initialization
    void Start ()
    {
        vehiclePosition = new Vector3(0, 0, 0);     // Or you could say Vector3.zero
        direction = new Vector3(1, 0, 0);           // Facing right
        velocity = new Vector3(0, 0, 0);            // Starting still (no movement)
        camera = Camera.main;
        totalCamHeight = camera.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * camera.aspect;
        laserList = new List<GameObject>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
    
        RotateVehicle();

        Drive();

        SetTransform();

        Wrap();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootLasers();

        }
        if(shot == false)
        {
            shotClock += Time.deltaTime;
        }
        
    }

    /// <summary>
    /// Changes / Sets the transform component
    /// </summary>
    public void SetTransform()
    {
        // Rotate vehicle sprite
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        // Set the transform position
        transform.position = vehiclePosition;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Drive()
    {
        // Accelerate
        // Small vector that's added to velocity every frame
        if(Input.GetKey(KeyCode.UpArrow)) 
        {
            acceleration = accelRate * direction;

            // We used to use this, but acceleration will now increase the vehicle's "speed"
            // Velocity will remain intact from one frame to the next
            //velocity = direction * speed;             // Unnecessary
            
        }
        else 
        {
            acceleration = Vector3.zero;

            velocity *= decelRate;
        }
        velocity += acceleration * Time.deltaTime;
        

        // Limit velocity so it doesn't become too large
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to vehicle's position
        vehiclePosition += velocity;
        acceleration = Vector3.zero;
    }

    public void RotateVehicle()
    {
        // Player can control direction
        // Left arrow key = rotate left by 2 degrees
        // Right arrow key = rotate right by 2 degrees
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angleOfRotation += 2;
            direction = Quaternion.Euler(0, 0, 2) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angleOfRotation -= 2;
            direction = Quaternion.Euler(0, 0, -2) * direction;
        }
    }
    void Wrap()
    {
        //wrapping vehicle method
        
        if (vehiclePosition.x < camera.transform.position.x - totalCamWidth / 2 - 1)
        {
            vehiclePosition = new Vector3(camera.transform.position.x + totalCamWidth / 2, vehiclePosition.y, 0);
        }
        else if(vehiclePosition.x > camera.transform.position.x + totalCamWidth / 2 + 1)
        {
            vehiclePosition = new Vector3(camera.transform.position.x - totalCamWidth / 2, vehiclePosition.y, 0);
        }
        
        if (vehiclePosition.y < camera.transform.position.y - totalCamHeight / 2 -1)
        {
            vehiclePosition = new Vector3(camera.transform.position.x, camera.transform.position.y + totalCamHeight / 2, 0);
        }
        else if (vehiclePosition.y > camera.transform.position.y + totalCamHeight / 2 + 1)
        {
            vehiclePosition = new Vector3(camera.transform.position.x, camera.transform.position.y - totalCamHeight / 2, 0);
        }
        
    }
    void ShootLasers()
    {

        if (shotClock > 0.5f && shot == false)
        {
            //only shoot every half a second!
            shot = true;
            shotClock = 0f;
        }

        if (shot)
        {
            shot = false;
            GameObject bull = Instantiate(bulletPrefab); //creating bullet prefab
            laserList.Add(bull); //adding it to the list of lasers
            bull.transform.position = vehiclePosition; //setting the lasers' position to the vehicle position
            bull.GetComponent<bullet>().bulletDirection = direction; //setting the direction of the bullets to the direction of the vehicle
        }
        
        
        
    }
}
