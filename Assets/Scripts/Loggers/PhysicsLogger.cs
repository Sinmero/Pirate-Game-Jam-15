using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsLogger : Logger
{
    private static PhysicsLogger statLogger;
    public static PhysicsLogger instance {get {return statLogger;}}

    private void Awake() {
        if (statLogger == null) statLogger = this;
    }
}
