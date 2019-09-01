using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

	public Animator animator;

	private int levelToLoad;
	
	// Update is called once per frame
	void Update () {
	/*	if (Input.GetMouseButtonDown(0))
		{
			FadeToNextLevel();
		}*/
	}

	public void FadeToNextLevel ()
	{
		FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void FadeToLevel (int levelIndex)
	{
        if (levelIndex >= SceneManager.sceneCountInBuildSettings) levelIndex = 0;
		levelToLoad = levelIndex;
		//animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete ()
	{
        Debug.Log("On fade complete");
        SceneManager.LoadScene(levelToLoad);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
