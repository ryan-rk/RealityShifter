using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
	[SerializeField] float loadScreenDuration = 3f;

	private void Start()
	{
		StartCoroutine(LoadScreenProcess());
	}

	IEnumerator LoadScreenProcess()
	{
		yield return new WaitForSeconds(loadScreenDuration);
		SceneManager.LoadScene("StartMenu");
	}

}
