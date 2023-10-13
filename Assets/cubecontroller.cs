using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	[SerializeField] float speed = 4;
	private playerinputmanager input;
	private CharacterController controller;
	private Animator animator;
	
	[SerializeField] Transform cameraFollowTarget;
	float xRotation;
	float yRotation;
	
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<playerinputmanager>();
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 targetDirection = new Vector3(input.move.x, 0, input.move.y);
		Quaternion targetRotation  = Quaternion.LookRotation(targetDirection);
		if(input.move != Vector2.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
		}
		animator.SetFloat("speed", input.move.magnitude);
        controller.Move(targetDirection * speed * Time.deltaTime);
    }
	
	private void LateUpdate() {
		CameraRotation();
	}
	
	void CameraRotation() {
		xRotation += input.look.y;
		yRotation += input.look.x;
		xRotation = Mathf.Clamp(xRotation, -30, 70);
		Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
		cameraFollowTarget.rotation = rotation;
	}
	
}
