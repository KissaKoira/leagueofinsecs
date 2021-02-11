using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public Camera camera;
    public GameObject cameraPoint;
    public GameObject body;

    public GameObject testObj;
    public GameObject qPref;

    public float playerSpeed = 5f;
    public float rotationSpeed = 1000;
    public float cameraSpeed = 20f;
    private Vector2 playerPos;
    private Vector2 playerTarget;
    private Vector2 playerDirection;

    void Start()
    {

    }

    void Update()
    {
    //Camera Control

        if (Input.GetButton("camControl"))
        {
            camera.transform.position = cameraPoint.transform.position;
        }

        Vector3 camPos = camera.transform.position;

        if (Input.mousePosition.y >= Screen.height - 10f) {

            camPos.z += cameraSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= 10f)
        {
            camPos.z -= cameraSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - 10f)
        {
            camPos.x += cameraSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= 10f)
        {
            camPos.x -= cameraSpeed * Time.deltaTime;
        }

        camera.transform.position = camPos;

    //Character Control

        playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            LayerMask ground = LayerMask.GetMask("Ground");

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100, ground))
            {
                testObj.transform.position = hit.point;

                playerTarget = new Vector2(hit.point.x, hit.point.z);
                playerDirection = (playerTarget - playerPos).normalized;
            }
        }

        if (Input.GetButtonDown("stop"))
        {
            playerTarget = playerPos;
            testObj.transform.position = new Vector3(0, 0, 0);
        }

        // Angle between player's current and target rotation:
        // targetAngle1 - as a float (-1 - 1)
        // targetAngle2 - in degrees (0 - 180)
        float targetAngle1 = Vector3.Dot(Vector3.Cross(body.transform.forward, new Vector3(playerDirection.x, body.transform.position.y, playerDirection.y)), Vector3.up);
        float targetAngle2 = Vector3.Angle(body.transform.forward, new Vector3(playerDirection.x, 0, playerDirection.y));

        if (Mathf.Abs(targetAngle1) < 0.05 && targetAngle2 < 5)
        {
            //player is pointing towards the target
        }
        else if (targetAngle1 > 0)
        {
            body.transform.Rotate(0, Time.deltaTime * rotationSpeed * 1, 0);
        }
        else
        {
            body.transform.Rotate(0, Time.deltaTime * rotationSpeed * -1, 0);
        }

        if ((playerTarget - playerPos).magnitude > 0.1)
        {

            gameObject.transform.position += new Vector3(playerDirection.x * playerSpeed * Time.deltaTime, 0, playerDirection.y * playerSpeed * Time.deltaTime);
        }

        //Abilities

        if (Input.GetButtonDown("qAbility")) {

            RaycastHit hit;
            LayerMask ground = LayerMask.GetMask("Ground");

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100, ground))
            {
                GameObject qOrb = GameObject.Instantiate(qPref);
                qOrb.transform.position = body.transform.position;
                Vector3 qDirection = (hit.point - body.transform.position).normalized;
                qOrb.GetComponent<Rigidbody>().velocity = new Vector3(qDirection.x * 20f, 0, qDirection.z * 20f);
            }
        }
    }
}
