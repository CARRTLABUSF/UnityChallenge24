using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private int lives = 5;

    public void removeLife()
    {
        lives -= 1;
    }

    public int getLives()
    {
        return lives;
    }
}
