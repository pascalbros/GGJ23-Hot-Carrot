using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtonController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.name != "Car Controller") { return; }
        if (name == "Start") {
            SceneManager.LoadScene("Main");
        } else if (name == "Credits") {
            SceneManager.LoadScene("Credits");
        } else if (name == "Menu") {
            SceneManager.LoadScene("Menu");
        }
    }
}
