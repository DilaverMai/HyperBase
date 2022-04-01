using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElephantIntro : MonoBehaviour
{
    public float TransTime;

    IEnumerator Start() {
        yield return new WaitForSeconds(TransTime);
        SceneManager.LoadScene("Game");
    }
}
