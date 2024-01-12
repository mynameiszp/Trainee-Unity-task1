using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInputManager : IControlStrategy
{
    public bool Left { get; private set; } = false;
    public bool Right { get; private set; } = false;
    public bool Up { get; private set; } = false;
    public bool Down { get; private set; } = false;
    public bool WasTouched { get; private set; }
    private Vector2 _startPos;

    public void ManageInput()
    {
        Left = Right = Up = Down = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    WasTouched = false;
                    break;
                case TouchPhase.Moved:
                    if (WasTouched)
                        return;
                    Vector2 deltaSwipe = touch.position - _startPos;
                    if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                    {
                        Left |= deltaSwipe.x < 0;
                        Right |= deltaSwipe.x > 0;
                    }
                    else
                    {
                        Up |= deltaSwipe.y > 0;
                        Down |= deltaSwipe.y < 0;
                    }
                    WasTouched = true;
                    break;
                case TouchPhase.Ended:
                    WasTouched = false;
                    break;
            }
        }
    }
}
