using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;

    private int horizontal = 0, vertical = 0;

    public enum Axis
    {
        Horizontal,
        Vertical
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent < PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0;
        vertical = 0;

        GetkeyBoardInput();

        SetMovement();
    }

    private void SetMovement()
    {
        if(vertical != 0)
        {
            playerController.SetInputDirection((vertical == 1)? PlayerDirection.Up: PlayerDirection.Down);
        }
        else if(horizontal != 0)
        {
            playerController.SetInputDirection((horizontal == 1) ? PlayerDirection.Right : PlayerDirection.Left);
        }
    }

    int GetAxisRaw(Axis axis)
    {
        if(axis == Axis.Horizontal)
        {
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);

            if (left)
            {
                return -1;
            }
            if (right)
                return 1;

            return 0;
        }
        else if( axis == Axis.Vertical)
        {
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);
            if (up)
            {
                return 1;
            }
            if (down)
                return -1;

            return 0;
        }
        return 0;
    }

    private void GetkeyBoardInput()
    {
        horizontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);

        if (horizontal != 0)
        {
            vertical = 0;
        }
    }


}
