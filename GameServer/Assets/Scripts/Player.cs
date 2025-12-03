using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    public float gravity = -9.8f;
    public float moveSpeed = 150f;
    public float jumpSpeed = 5f;

    public bool IsReady = false;
    public bool IsStart = false;

    private bool[] inputs;
    private float yVelocity = 0f;

    public List<int> inventory = new List<int>();
    public int Hand = 0;
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        inputs = new bool[4];

        inventory.Add(0);
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        if (IsStart == false)
            return;
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        if(controller.isGrounded)
        {
            yVelocity = 0f;
        }
        yVelocity += gravity;

        _moveDirection.y = yVelocity;
        controller.Move(_moveDirection);

       //주석 ServerSend.PlayerPosition(this);
       //주석 ServerSend.PlayerRotation(this);
    }

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }

    public int ChangeHand()
    {
        Hand += 1;
        if (Hand == inventory.Count)
            Hand = 0;

        return inventory[Hand];
    }
}
