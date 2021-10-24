using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionAudioPlayer : MonoBehaviour
{
    public static event Action died;

    public static void PlaySound() {
        # region observer
        died?.Invoke();
        # endregion
    }

}
