using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIGraphicsScript : MonoBehaviour
{
    public AIPath aIPath;
    void Update()
    //Changing the sprite depending on the current direction
    {
        if (aIPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(0.53f,0.53f,0.53f);
        }
        else if (aIPath.desiredVelocity.x <= 0.01f) {
            transform.localScale = new Vector3(-0.53f,0.53f,0.53f);
        }
    }
}
