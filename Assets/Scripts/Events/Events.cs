using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class definitions for events to be invoked.  Each event should be limited to 
 * only including properties.  
 */
public class SetActiveControllerEvent
{
    public PlayerController Controller { get; set; }
    public SetActiveControllerEvent(PlayerController controller) {
        Controller = controller;
    }
}
