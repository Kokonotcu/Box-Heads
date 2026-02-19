using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InputControl : Singleton<InputControl>
{
	PlayerInput playerInput;

	InputAction.CallbackContext callbackContext = new InputAction.CallbackContext();
	Dictionary<Action<InputAction.CallbackContext>, InputAction> held = new Dictionary<Action<InputAction.CallbackContext>, InputAction>();

    PlayerInput PlayerInput {
		get 
		{
			if (playerInput == null)
				playerInput = GetComponent<PlayerInput>();
			
			return playerInput;
        }  
		set => playerInput = value; 
	}
    public void SubscribeStarted(Action<InputAction.CallbackContext> subscriber, string actName, bool isHold = false)
	{
		if (isHold)
		{
            PlayerInput.actions[actName].performed += subscriber;
		}
		else
		{
            PlayerInput.actions[actName].started += subscriber;
		}
	}

	public void SubscribeCancelled(Action<InputAction.CallbackContext> subscriber, string actName)
	{
        PlayerInput.actions[actName].canceled += subscriber;
	}

	public void SubscribeHeld(Action<InputAction.CallbackContext> subscriber, string actName)
	{
		held.Add(subscriber, PlayerInput.actions[actName]);
	}

	public void UnsubsribeAll(Action<InputAction.CallbackContext> subscriber, string actName) 
	{
		if (PlayerInput == null)
			return;
        PlayerInput.actions[actName].canceled -= subscriber;
        PlayerInput.actions[actName].performed -= subscriber;
        PlayerInput.actions[actName].started -= subscriber;
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
