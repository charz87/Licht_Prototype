using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public float waitSecs;

    private Scene currentScene;


    void Update()
    {
        StartCoroutine(ChangeLevel());
    }

	IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(waitSecs);
        SceneManager.LoadScene(1);
        
    }
}
