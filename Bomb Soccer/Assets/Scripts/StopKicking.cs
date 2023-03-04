using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopKicking : MonoBehaviour
{
    public PlayerMovement player;

    public void StopKick()
    {
        player.kicking = false;
    }
}
