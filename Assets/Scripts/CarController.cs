using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] bodies;

    public void ChangeBody(int playerIndex) {
        foreach (var body in bodies) {
            body.SetActive(false);
        }
        bodies[playerIndex].SetActive(true);
    }
}
