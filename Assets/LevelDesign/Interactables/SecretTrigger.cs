using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretTrigger : MonoBehaviour
{
	[SerializeField] GameObject secretPanel;
	[SerializeField] Animator secretPanelAC;
	[SerializeField] float nextLevelDelay = 1f;
	Player player;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<Player>(out Player player))
		{
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.PlaySound("Success");
			}
			this.player = player;
			Time.timeScale = 0f;
			secretPanel.SetActive(true);
		}
	}

	public void CloseSecretMessage()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
		}
		secretPanelAC.Play("Close");
		Time.timeScale = 1f;
		player.SetWin();
		SceneTransition.Instance.TransitionOutOfScene(nextLevelDelay, () => { SceneManager.LoadScene("Level_4_6"); });
	}
}
