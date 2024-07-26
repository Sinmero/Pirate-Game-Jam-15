using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLogger : Logger
{
    private static SoundLogger soundLogger;
    public static SoundLogger instance {get {return soundLogger;}}

    private void Awake() {
        if (soundLogger == null) soundLogger = this;
    }
}
