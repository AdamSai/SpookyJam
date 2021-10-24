using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private InputController input;
    private Rigidbody rigidBody;
    private float fall;

    // Start is called before the first frame update
    void Start()
    {
        input = GameManager.instance.inputController;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
    {
        float roll = input.moveDirection.x;
        float tilt = input.moveDirection.y;

        //TODO: TEST THIS
        float yaw = input.moveDirection.x / 8;

        float tip = (transform.right + Vector3.up).magnitude - 1.414214f;

        if (tilt != 0)
            transform.Rotate(transform.right, tilt * Time.deltaTime * 10, Space.World);
        if (roll != 0)
            transform.Rotate(transform.forward, roll * Time.deltaTime * -10, Space.World);
        if(yaw != 0)
            transform.Rotate(transform.up, yaw * Time.deltaTime * 15, Space.World);

        //Gravity
        rigidBody.velocity += Vector3.up * Time.deltaTime;

        //Vertical (to the glider) velocity turns into horizontal velocity
        Vector3 vertVel = rigidBody.velocity - Vector3.ProjectOnPlane(transform.up, rigidBody.velocity);
        fall = vertVel.magnitude;
        rigidBody.velocity -= vertVel * Time.deltaTime;
        rigidBody.velocity += vertVel.magnitude * transform.forward * Time.deltaTime / 10;

        //Drag
        Vector3 forwardDrag = rigidBody.velocity - Vector3.ProjectOnPlane(transform.forward, rigidBody.velocity);
        rigidBody.AddForce(-forwardDrag * forwardDrag.magnitude * Time.deltaTime / 1000);

        Vector3 sideDrag = rigidBody.velocity - Vector3.ProjectOnPlane(transform.right, rigidBody.velocity);
        rigidBody.AddForce(-sideDrag * sideDrag.magnitude * Time.deltaTime);

        //airspeed = rigidBody.velocity.magnitude;

        //tiltometer.rotation = Quaternion.LookRotation(Vector2.up);
    }
}