using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> This script spawns competitors in a random time to a random place <summary>
/// <summary> Main Camera <summary>

public class CompetitorSpawner : MonoBehaviour {
	
	#region Properties
	// Save the prefabs
	[SerializeField]
	private GameObject competitorPrefab0;
	[SerializeField]
	private GameObject competitorPrefab1;
	[SerializeField]
	private GameObject competitorPrefab2;
	[SerializeField]
	private GameObject competitorPrefab3;
	[SerializeField]
	private GameObject competitorPrefab4;
	[SerializeField]
	private GameObject competitorPrefab5;
	[SerializeField]
	private GameObject competitorPrefab6;
	[SerializeField]
	private GameObject competitorPrefab7;
	[SerializeField]
	private GameObject competitorPrefab8;
	[SerializeField]
	private GameObject competitorPrefab9;

	// Timer support
	private Timer timer;

	// Constants
	private const float minSpawnTime = 5f;
	private const float maxSpawnTime = 10f;
	private const int numberOfPrefabs = 10;

	private GameObject competitor;
	#endregion


	#region Methods
	void Start() {
		// Add the timers as a component
		timer = gameObject.AddComponent<Timer>();

		// Set the duration of the timer to a random time amount
		timer.Duration = Random.Range(minSpawnTime, maxSpawnTime);

		// Start the timer for the first time
		timer.Run();
	}
	void Update() {
		if (timer.Finished) {
			// Spawn a new competitor
			competitor = SpawnCompetitor();

			// Run the timer again
			timer.Duration = Random.Range(minSpawnTime, maxSpawnTime);
			timer.Run();
		}
	}
	private GameObject SpawnCompetitor() {
		// Set a spawn position interval
		const float minSpawnx = -4f;
		const float maxSpawnx = 5f;
		const float Spawny = 5.99927f;
		const float Spawnz = 20f;
		// Set a random x component for spawn position
		float Spawnx = Random.Range(minSpawnx, maxSpawnx);
		// Set the spawn position
		Vector3 SpawnPosition = new Vector3(Spawnx, Spawny, Spawnz);

		// Set a spawn direction
		Quaternion spawnDirection = new Quaternion(0f, 180f, 0f, 0f);

		// This will be returned by this function
		GameObject newCompetitor;

		// Get a random prefab number
		int prefabNumber = Random.Range(0, numberOfPrefabs);

		// Instantiate a new competitor
		if (prefabNumber == 0) {
			newCompetitor = Object.Instantiate(competitorPrefab0, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 1) {
			newCompetitor = Object.Instantiate(competitorPrefab1, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 2) {
			newCompetitor = Object.Instantiate(competitorPrefab2, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 3) {
			newCompetitor = Object.Instantiate(competitorPrefab3, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 4) {
			newCompetitor = Object.Instantiate(competitorPrefab4, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 5) {
			newCompetitor = Object.Instantiate(competitorPrefab5, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 6) {
			newCompetitor = Object.Instantiate(competitorPrefab6, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 7) {
			newCompetitor = Object.Instantiate(competitorPrefab7, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else if (prefabNumber == 8) {
			newCompetitor = Object.Instantiate(competitorPrefab8, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
		else {
			newCompetitor = Object.Instantiate(competitorPrefab9, SpawnPosition, spawnDirection);
			// Return our new competitor
			return newCompetitor;
		}
	}
	#endregion
}