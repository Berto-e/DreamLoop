using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckDevices : MonoBehaviour
{
    Gamepad gamepad;

    private void Update() {
        CheckDevice();
    }

    public bool CheckDevice()
    {
        gamepad = Gamepad.current;
        if(gamepad != null)
            return true;
        else
            return false;
        
    }
}
