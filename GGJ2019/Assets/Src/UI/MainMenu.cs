using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Animator animator;
    public Animation animation_partical;
    public Animation animation_expantion;

    float timer = 10;
    bool timing = false;

	public void Exit() {
        Application.Quit();
    }

    private void Update() {
        if (timing) timer -= Time.deltaTime;

        if (timer <= 0 && timing) {
            SceneManager.LoadScene(1);
        }
    }

    public void StartGame() {
        timing = true;
        animator.SetBool("PLAY", true);
        animation_partical.Play();
        animation_expantion.Play();
        
    }

}
