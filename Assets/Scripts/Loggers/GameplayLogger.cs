using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLogger : Logger
{
    private static GameplayLogger statLogger;
    public static GameplayLogger instance {get {return statLogger;}}

    private void Awake() {
        if (statLogger == null) statLogger = this;
    }
}
