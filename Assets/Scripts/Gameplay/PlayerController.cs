using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float impulseForce;
    [SerializeField] private float forwardDrag;
    [SerializeField] private float gravityIncrease;
    [SerializeField] private float gravityDecrease;

    private InputController input;
    private Rigidbody rigidBody;
    private float fall;
    private float currentTilt;

    // Start is called before the first frame update
    void Start()
    {
        input = GameManager.instance.inputController;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float vertical = input.moveDirection.y;
        float horizontal = input.moveDirection.x;

        Vector3 tempGravity = Physics.gravity;

        if(vertical > 0)
        {
            //TODO: Lower Gravity
            tempGravity = tempGravity / gravityDecrease;
        }
        else if (vertical < 0)
        {
            //TODO: Higher Gravity
            tempGravity = tempGravity * gravityIncrease;
        }

        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);

        if(horizontal > 0)
        {
            direction = -Vector3.right;
        }
        else if(horizontal < 0)
        {
            direction = Vector3.right;
        }

        rigidBody.AddForce(direction * Time.deltaTime * turnSpeed);

        //Forward Drag
        //TODO: Maybe use forwdrag (below) to calculate right plane if horizontal movement moves player to any of the sides
        //Vector3 forwdrag = rigidBody.velocity - Vector3.ProjectOnPlane(-transform.forward, rigidBody.velocity);
        rigidBody.AddForce(-Vector3.forward * rigidBody.velocity.z * forwardDrag * Time.deltaTime);

        //NOTES: Keep in mind rigidbody needs to have UseGravity = false
        rigidBody.AddForce(tempGravity * (rigidBody.mass * rigidBody.mass));

        //OLD TILT, KEEP THIS FOR REFERENCE
        //===================================================================================================
        //currentTilt = Mathf.MoveTowards(currentTilt, horizontal * horizontal, Time.deltaTime * tiltSpeed);
        //transform.localRotation = Quaternion.Euler(0, 0, -currentTilt);

        //OLD MOVEMENT, KEEP THIS FOR REFERENCE
        //====================================================================================================================================================
        //float roll = input.moveDirection.x;
        ////float tilt = input.moveDirection.y/*;*/

        ////TODO: TEST THIS
        //float yaw = input.moveDirection.x / 8;

        //float tip = (transform.right + Vector3.up).magnitude - 1.414214f;

        //if (tilt != 0)
        //    transform.Rotate(transform.right, tilt * Time.deltaTime * 10, Space.World);
        //if (roll != 0)
        //    transform.Rotate(transform.forward, roll * Time.deltaTime * -10, Space.World);
        //if(yaw != 0)
        //    transform.Rotate(transform.up, yaw * Time.deltaTime * 15, Space.World);

        //Gravity
        //rigidBody.velocity += Vector3.up * Time.deltaTime;

        ////Vertical (to the glider) velocity turns into horizontal velocity
        //Vector3 vertVel = rigidBody.velocity - Vector3.ProjectOnPlane(transform.up, rigidBody.velocity);
        //fall = vertVel.magnitude;
        //rigidBody.velocity -= vertVel * Time.deltaTime;
        //rigidBody.velocity += vertVel.magnitude * transform.forward * Time.deltaTime / 10;

        ////Drag
        //Vector3 forwardDrag = rigidBody.velocity - Vector3.ProjectOnPlane(transform.forward, rigidBody.velocity);
        //rigidBody.AddForce(-forwardDrag * forwardDrag.magnitude * Time.deltaTime / 1000);

        //Vector3 sideDrag = rigidBody.velocity - Vector3.ProjectOnPlane(transform.right, rigidBody.velocity);
        //rigidBody.AddForce(-sideDrag * sideDrag.magnitude * Time.deltaTime);

        //airspeed = rigidBody.velocity.magnitude;

        //tiltometer.rotation = Quaternion.LookRotation(Vector2.up);
        //====================================================================================================================================================
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Candybag"))
        {
            Vector3 forwardUp = -Vector3.forward + Vector3.up;
            rigidBody.AddForce(forwardUp * impulseForce, ForceMode.Impulse);
        }
    }
}
