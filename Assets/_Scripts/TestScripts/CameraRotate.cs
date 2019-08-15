using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public Transform player;
	private Vector3 offset;

	public void Start()
	{
		transform.position = player.position + offset;
	 	offset = transform.position - player.transform.position;
	}
	
	void Update()
	{
	 	transform.LookAt(player);
	}
	void LateUpdate()
    {
    	transform.position = player.transform.position + offset;
    }
}