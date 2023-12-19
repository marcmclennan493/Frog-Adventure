/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    [RequireComponent(typeof(Rigidbody))]
public class cubecontroller : MonoBehaviour
{
	
	[SerializeField] float moveSpeed = 4;


	
	private playerinputmanager input;
	private CharacterController controller;
	private Animator animator;
    	public Vector3 jump;
    	public float jumpForce = 2.0f;
    	public bool isGrounded;
    	Rigidbody rb;
	[SerializeField] GameObject mainCam;
	[SerializeField] Transform cameraFollowTarget;
	float xRotation;
	float yRotation;
	
    // Start is called before the first frame update
    void Start()
    {
    		rb = GetComponent<Rigidbody>();
    		jump = new Vector3(0.0f, 2.0f, 0.0f);
			
        input = GetComponent<playerinputmanager>();
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
    }
	
    	void OnCollisionEnter()
    	{
    		isGrounded = true;
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
		
		    		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
    Debug.Log("shmump");
    			rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    			isGrounded = false;
			}
	//	animator.SetFloat("speed", input.move.magnitude);
		Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
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
	
}*/

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CubeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float autoStepThreshold = 0.5f; // Adjust this value based on your needs
    [SerializeField] private Transform cameraFollowTarget;

    private playerinputmanager input;
    private CharacterController controller;
    private Animator animator;

    private float xRotation;
    private float yRotation;
    private Vector3 velocity; // For handling vertical velocity

    void Start()
    {
        input = GetComponent<playerinputmanager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speed = 0;
        Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y);
        float targetRotation = 0;

        if (input.move != Vector2.zero)
        {
            speed = moveSpeed;

            targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraFollowTarget.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            animator.SetTrigger("JumpTrigger");
        }

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime * 5;

        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        Vector3 move = targetDirection.normalized * speed * Time.deltaTime;

        // Check for small ridges and adjust the character's position
        CheckForAutoStep();

        // Move the character controller
        controller.Move(move + velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        xRotation += -input.look.y;
        yRotation += input.look.x;
        xRotation = Mathf.Clamp(xRotation, -30, 70);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }

void CheckForAutoStep()
{
    // Raycast to check the height difference in front of the character
    RaycastHit hit;
    float raycastDistance = 2.0f; // Adjust this value based on your needs
    float maxHeightForAutoStep = 2.0f; // Adjust this value based on your needs

    if (Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.forward, out hit, raycastDistance))
    {
        Collider collider = hit.collider;

        if (collider != null)
        {
            // Check if the height difference is within the threshold and the object's height is below the limit
            if (hit.point.y - collider.bounds.min.y <= autoStepThreshold && collider.bounds.size.y <= maxHeightForAutoStep)
            {
                // Reset vertical velocity when grounded to allow auto-stepping again
                if (controller.isGrounded)
                {
                    velocity.y = 0f;
                }

                // Move the character up smoothly
                controller.Move(Vector3.up * (hit.point.y - transform.position.y));
            }
        }
    }
}




}













