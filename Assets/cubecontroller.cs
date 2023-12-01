using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	
	[SerializeField] float moveSpeed = 4;
	
	private playerinputmanager input;
	private CharacterController controller;
	private Animator animator;
	public Rigidbody rb;
	
	[SerializeField] GameObject mainCam;
	[SerializeField] Transform cameraFollowTarget;
	float xRotation;
	float yRotation;
	
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<playerinputmanager>();
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
		float speed = 0;
		Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y);
		float targetRotation  = 0;
		 
		
		if(input.move != Vector2.zero) {
			
			speed = moveSpeed;
			
			targetRotation = Quaternion.LookRotation(inputDir).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
			
			Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
		}
		animator.SetFloat("speed", input.move.magnitude);
		Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        controller.Move(targetDirection * speed * Time.deltaTime);
		
		if(Input.GetButtonDown("Jump")) {
		rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
		}
		
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
