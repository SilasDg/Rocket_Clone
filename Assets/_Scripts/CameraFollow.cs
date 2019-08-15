using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public GameObject player;

	public float sensitivity;
	private Vector3 offset;

  	public void Start()
    {
        offset = transform.position - player.transform.position;
    }
 
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }   

	void FixedUpdate()
	{
		float rotateHorizontal = Input.GetAxis ("Mouse X");
		//float rotateVertical = Input.GetAxis ("Mouse Y");
		transform.RotateAround (player.transform.position, -Vector3.up, -rotateHorizontal * sensitivity);
		//transform.RotateAround (Vector3.zero, transform.right, rotateVertical * sensitivity);
	}

}

/*
{
   public GameObject player;

    private Vector3 offset;
 
    public void Start()
    {
        offset = transform.position - player.transform.position;
    }
 
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }   
}
*/