using UnityEngine;

public class GlockTriggerSpawner : MonoBehaviour
{
    [SerializeField] GameObject glockTriggerPrefab;
    [SerializeField] float glockSpawnTime = 40f;

    TimerManager timerManager;
    private bool hasSpawned = false;

    //first finds the timemanagerscript
    private void Start()
    {
        timerManager = FindAnyObjectByType<TimerManager>();
    }

    // checks every frame if is already spawned, and if the timer is below the spedified time.
    // I am not useing Ienumerator or anything else as this has low performance impact and is not worth implementing
    //ic conditions are met, it spawns the glock and sets hasSpawned to true
    private void Update()
    {
        if (!hasSpawned && timerManager.currentTime <= glockSpawnTime)
        {
            SpawnTrigger();
            hasSpawned = true;
        }
    }

    // Just spawns the prefab
    void SpawnTrigger()
    {
        GameObject pickup = Instantiate(glockTriggerPrefab, transform.position, Quaternion.identity);
    }
}
