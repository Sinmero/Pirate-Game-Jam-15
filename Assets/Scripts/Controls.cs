using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public bool _isArrowControls = true;
    private static Controls _controls;
    public static Controls instance { get { return _controls; } }
    private static controlKeys _keys;
    public static controlKeys keys { get { return _keys; } }
    private static controlKeys _defaultKeys = new controlKeys(KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.E, 
    KeyCode.LeftShift, KeyCode.R, KeyCode.Escape, KeyCode.B, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Space, KeyCode.I, KeyCode.Q, KeyCode.F);
    private static controlKeys _customkeys = new controlKeys(KeyCode.W, KeyCode.A, KeyCode.D, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.E, 
    KeyCode.Space, KeyCode.R, KeyCode.Escape, KeyCode.B, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Space, KeyCode.I, KeyCode.Q, KeyCode.F);
    private static controlKeys _lockedControls = new controlKeys(KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.E, 
    KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None);



    private void Awake()
    {
        _keys = _defaultKeys;

        if (_controls == null) _controls = this;

        if (_isArrowControls) _keys = _customkeys;
    }



    public static void LockControls()
    {
        _keys = _lockedControls;
        SystemLogger.instance.Log($"Controls locked.", null);
    }



    public static void UnlockControls() {
        _keys = _customkeys;
        SystemLogger.instance.Log($"Controls unlocked.", null);
    }
}




public struct controlKeys
{
    public controlKeys(KeyCode jump, KeyCode left, KeyCode right, KeyCode red, KeyCode green, KeyCode blue, KeyCode grey, KeyCode interact, KeyCode dash, 
    KeyCode returnToCheckpoint, KeyCode settingsMenu, KeyCode buildMenu, KeyCode key1, KeyCode key2, KeyCode key3, KeyCode key4, KeyCode key5, KeyCode confirm, KeyCode inventory, KeyCode interactSecondary, KeyCode interactHold)
    {
        _jump = jump;
        _left = left;
        _right = right;
        _red = red;
        _green = green;
        _blue = blue;
        _interact = interact;
        _grey = grey;
        _dash = dash;
        _returnToCheckpoint = returnToCheckpoint;
        _settingsMenu = settingsMenu;
        _buildMenu = buildMenu;
        _key1 = key1;
        _key2 = key2;
        _key3 = key3;
        _key4 = key4;
        _key5 = key5;
        _confirm = confirm;
        _inventory = inventory;
        _interactSecondary = interactSecondary;
        _interactHold = interactHold;
    }

    public KeyCode _jump;
    public KeyCode _left;
    public KeyCode _right;
    public KeyCode _red;
    public KeyCode _green;
    public KeyCode _blue;
    public KeyCode _grey;
    public KeyCode _interact;
    public KeyCode _dash;
    public KeyCode _returnToCheckpoint;
    public KeyCode _settingsMenu;
    public KeyCode _buildMenu;
    public KeyCode _key1;
    public KeyCode _key2;
    public KeyCode _key3;
    public KeyCode _key4;
    public KeyCode _key5;
    public KeyCode _confirm;
    public KeyCode _inventory;
    public KeyCode _interactSecondary;
    public KeyCode _interactHold;
}