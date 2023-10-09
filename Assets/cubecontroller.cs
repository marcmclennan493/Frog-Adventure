using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroller : MonoBehaviour
{
	[SerializeField] float speed = 4;
	private playerinputmanager input;
	private CharacterController controller;
	
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<playerinputmanager>();
		controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 targetDirection = new Vector3(input.move.x, 0, input.move.y);
        controller.Move(targetDirection * speed * Time.deltaTime);
    }
}
