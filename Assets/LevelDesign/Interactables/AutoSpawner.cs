using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
	[SerializeField] float initialSpawnDelay = 1f;
	[SerializeField] bool isAutoSpawnAfterDeath = true;
	[SerializeField] float spawnDelayAfterDeath = 1f;
	[SerializeField] Spawnable spawnTargetPrefab;
	[SerializeField] Vector2 spawnPointOffset;
	List<Spawnable> spawnedTargets = new List<Spawnable>();

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SpawnDelay(initialSpawnDelay));
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
			StartCoroutine(SpawnDelay(spawnDelayAfterDeath));
		}
	}

	IEnumerator SpawnDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
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
