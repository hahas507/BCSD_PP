using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToLoad : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string sceneNameToLoad;

    private void Awake()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (GameManager.sceneNameToLoad != null)
        {
            SceneManager.LoadScene(GameManager.sceneNameToLoad);
        }
        else if (GameManager.sceneNameToLoad == null)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}