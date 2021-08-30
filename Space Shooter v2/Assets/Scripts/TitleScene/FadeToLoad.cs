using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToLoad : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string sceneNameToLoad = null;

    private void Awake()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (sceneNameToLoad == "")
        {
            if (SceneManager.GetActiveScene().name == "LobbyScene")
            {
                SceneManager.LoadScene(GameManager.sceneNameToLoad);
            }
        }
        else if (sceneNameToLoad != "")
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}