using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_nametag : MonoBehaviour
{
    public Camera camera;
    public GameObject body;

    public float namePlateOffSet;

    void Start()
    {

    }

    void Update()
    {
        gameObject.transform.position = camera.WorldToScreenPoint(new Vector3(body.transform.position.x, body.transform.position.y + namePlateOffSet, body.transform.position.z));
    }
}
