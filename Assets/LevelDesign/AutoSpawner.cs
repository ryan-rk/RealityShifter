using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
	[SerializeField] bool isAutoSpawnAfterDeath = true;
	[SerializeField] float spawnDelayAfterDeath = 1f;
	[SerializeField] Spawnable spawnTargetPrefab;
	[SerializeField] Vector2 spawnPointOffset;
	List<Spawnable> spawnedTargets = new List<Spawnable>();

	// Start is called before the first frame update
	void Start()
	{
		SpawnTarget();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SpawnTarget()
	{
		Spawnable spawnedInstance = Instantiate(spawnTargetPrefab, (Vector2)transform.position + spawnPointOffset, Quaternion.identity, transform);
		spawnedTargets.Add(spawnedInstance);
		spawnedInstance.OnSpawnedTargetDestroyed += RespawnAfterDestroy;
	}

	void RespawnAfterDestroy()
	{
		if (isAutoSpawnAfterDeath)
		{
			StartCoroutine(DelayRespawn());
		}
	}

	IEnumerator DelayRespawn()
	{
		yield return new WaitForSeconds(spawnDelayAfterDeath);
		SpawnTarget();
	}

	private void OnDisable()
	{
		foreach (Spawnable spawnedTarget in spawnedTargets)
		{
			spawnedTarget.OnSpawnedTargetDestroyed -= RespawnAfterDestroy;
		}
	}
}
