using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InputControl : Singleton<InputControl>
{
	private PlayerInput playerInput;
	InputAction.CallbackContext callbackContext;
	Dictionary<Action<InputAction.CallbackContext>, InputAction> held;

	void Start()
    {
		playerInput = GetComponent<PlayerInput>();
		//For button hold activity
		held = new Dictionary<Action<InputAction.CallbackContext>, InputAction>();
		callbackContext = new InputAction.CallbackContext();
	}

	public void SubscribeStarted(Action<InputAction.CallbackContext> subscriber, string actName, bool isHold = false)
	{
		if (isHold)
		{
			playerInput.actions[actName].performed += subscriber;
		}
		else
		{
			playerInput.actions[actName].started += subscriber;
		}
	}

	public void SubscribeCancelled(Action<InputAction.CallbackContext> subscriber, string actName)
	{
		playerInput.actions[actName].canceled += subscriber;
	}

	public void SubscribeHeld(Action<InputAction.CallbackContext> subscriber, string actName)
	{
		held.Add(subscriber, playerInput.actions[actName]);
	}

	public void UnsubsribeAll(Action<InputAction.CallbackContext> subscriber, string actName) 
	{
		playerInput.actions[actName].canceled -= subscriber;
		playerInput.actions[actName].performed -= subscriber;
		playerInput.actions[actName].started -= subscriber;
		if (held.ContainsKey(subscriber))
		{
			held.Remove(subscriber);
		}
	}

	public bool IsActionHeld(string actName)
	{
		return playerInput.actions[actName].IsPressed();
	}

	private void Update()
	{
		foreach (var action in held)
		{
			if (action.Value.IsPressed())
			{
				action.Key.Invoke(callbackContext);
			}
		}
	}
}
