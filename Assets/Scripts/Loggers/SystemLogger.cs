using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemLogger : Logger
{
    private static SystemLogger statLogger;
    public static SystemLogger instance {get {return statLogger;}}

    private void Awake() {
        if (statLogger == null) statLogger = this;
    }
}
