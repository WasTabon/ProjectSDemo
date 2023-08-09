using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject holePrefab;
    public GameObject platformPrefab;

    public int numberOfPlatforms = 10;
    public float platformWidth = 2f;
    public float minY = 1f;
    public float maxY = 3f;
    public float gapSize = 2f;

    private void Start()
    {
        GeneratePlatforms();
    }

    private void GeneratePlatforms()
    {
        float currentX = 0f;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            bool hasHole = Random.value > 0.5f;

            if (hasHole)
            {
                Instantiate(holePrefab, new Vector3(currentX, Random.Range(minY, maxY), 0f), Quaternion.identity);
                currentX += gapSize;
            }
            else
            {
                Instantiate(platformPrefab, new Vector3(currentX, Random.Range(minY, maxY), 0f), Quaternion.identity);
                currentX += platformWidth;
            }
        }

        // Generate ground
        float groundWidth = numberOfPlatforms * platformWidth + (numberOfPlatforms - 1) * gapSize;
        Instantiate(groundPrefab, new Vector3(groundWidth / 2f, 0f, 0f), Quaternion.identity, transform);
    }
}