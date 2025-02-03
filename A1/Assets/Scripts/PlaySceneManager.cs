using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject targetPrefab;
    public GameObject hazardPrefab;
    public GameObject obstaclePrefab;

    private GameObject currentCharacter;
    private GameObject currentTarget;
    private GameObject currentHazard;
    private GameObject currentObstacle;
    public AudioClip oneShotClip;
    private enum SpawnType
    {
        Reset,
        Seek,
        Flee,
        Arrival,
        Avoidance
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnObjects(SpawnType.Reset);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnObjects(SpawnType.Seek);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnObjects(SpawnType.Flee);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnObjects(SpawnType.Arrival);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnObjects(SpawnType.Avoidance);
        }
    }

    void PlayMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (oneShotClip != null) // Ensure the clip is assigned
        {
            audioSource.PlayOneShot(oneShotClip); // Always play the sound
        }
    }
    void ResetScene()
    {
        DestroyImmediate(currentCharacter);
        DestroyImmediate(currentTarget);
        DestroyImmediate(currentHazard);
        DestroyImmediate(currentObstacle);
    }

    Vector3 GetRandomSpawnPosition(float margin = 0.1f)
    {
        float x = Random.Range(margin, 1 - margin);
        float y = Random.Range(margin, 1 - margin);
        return Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10));
    }

    void SpawnObjects(SpawnType type)
    {
        ResetScene();
        currentCharacter = Instantiate(characterPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        switch (type)
        {
            case SpawnType.Seek:
                PlayMusic();
                currentTarget = Instantiate(targetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                currentCharacter.AddComponent<SeekBehavior>().target = currentTarget.transform;
                break;
            case SpawnType.Flee:
                PlayMusic();
                currentHazard = Instantiate(hazardPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                currentCharacter.AddComponent<FleeBehavior>().hazard = currentHazard.transform;
                break;
            case SpawnType.Arrival:
                PlayMusic();
                currentTarget = Instantiate(targetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                currentCharacter.AddComponent<ArrivalBehavior>().target = currentTarget.transform;
                break;
            case SpawnType.Avoidance:
                PlayMusic();
                currentTarget = Instantiate(targetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                currentObstacle = Instantiate(obstaclePrefab, GetRandomSpawnPosition(), Quaternion.identity);

                // Ensure obstacle is between character and target
                Vector3 midpoint = (currentCharacter.transform.position + currentTarget.transform.position) / 2;
                currentObstacle.transform.position = midpoint + Random.insideUnitSphere * 2;

                currentCharacter.AddComponent<AvoidanceBehavior>().Initialize(currentTarget.transform, currentObstacle.transform);
                break;
            case SpawnType.Reset:
                ResetScene();
                break;
        }
    }
}
