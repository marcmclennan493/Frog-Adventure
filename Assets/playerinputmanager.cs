using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerinputmanager : MonoBehaviour
{
	
	public Vector2 move;
	
	void OnMove(InputValue value)
	{
		move = value.Get<Vector2>();
	}
	

}
