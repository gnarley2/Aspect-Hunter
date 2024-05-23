using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introSceneEnd : MonoBehaviour
{
    //for scene transitions
    [SerializeField] Animator transistionAnim;

private string currentLevelName;
private Vector3 playerTargetPosition; // The position to move the player to in the new scene
private int savedProjectileIndex = -1;

private int tsceneIndex;
private void Start()
{
 
    currentLevelName = SceneManager.GetActiveScene().name;

}

public void SetPlayerTargetPosition(Vector3 position)
{
    playerTargetPosition = position;
}

public void SetProjectileIndex(int index)
{
    savedProjectileIndex = index;
}

public void LoadSceneFromTrigger(int targetSceneIndex)
{
    if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        tsceneIndex = targetSceneIndex;
        StartCoroutine(SceneTransition());
        
    }
    else
    {
        Debug.LogWarning("Invalid target scene index.");
    }
}
private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
     transistionAnim.SetTrigger("Start");

}


    IEnumerator SceneTransition()
{
    transistionAnim.SetTrigger("End");
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(tsceneIndex);
    transistionAnim.SetTrigger("Start");
}



}

