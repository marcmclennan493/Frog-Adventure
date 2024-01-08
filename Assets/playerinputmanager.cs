using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerinputmanager : MonoBehaviour
{
	
	public Vector2 move;
	public Vector2 look;
	
	void OnMove(InputValue value)
	{
		move = value.Get<Vector2>();
	}
	
	void OnLook(InputValue value) {
		if (Input.GetMouseButton(0)){
		look = value.Get<Vector2>();
		}
	}
	
    void Update()
    {
        // Check for the Escape key press
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
	
}