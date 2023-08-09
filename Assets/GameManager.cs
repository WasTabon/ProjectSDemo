using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;

    private void Start()
    {
        CreatePlayer();
        CreateEnemies();
    }

    private void CreatePlayer()
    {
        Vector3 spawnPos = new Vector3(0, 13f, 0);
        
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        player.name = "Player";

        // Добавляем компоненты для игрока, если они отсутствуют
        if (!player.GetComponent<Rigidbody2D>())
            player.AddComponent<Rigidbody2D>();

        if (!player.GetComponent<BoxCollider2D>())
            player.AddComponent<BoxCollider2D>();

        if (!player.GetComponent<Animator>())
            player.AddComponent<Animator>();

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (!playerController)
            playerController = player.AddComponent<PlayerController>();

        // Настройки компонентов игрока
        playerController.moveSpeed = 5f;
        playerController.jumpForce = 10f;
        playerController.attackCooldown = 0.5f;

        // Замораживаем вращение по оси Z для Rigidbody2D игрока
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(15, 30f), Random.Range(5f, 8f), 0f);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.name = "Enemy_" + i;

            // Добавляем компоненты для врага, если они отсутствуют
            if (!enemy.GetComponent<Rigidbody2D>())
                enemy.AddComponent<Rigidbody2D>();

            if (!enemy.GetComponent<BoxCollider2D>())
                enemy.AddComponent<BoxCollider2D>();

            if (!enemy.GetComponent<SpriteRenderer>())
                enemy.AddComponent<SpriteRenderer>();

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (!enemyController)
                enemyController = enemy.AddComponent<EnemyController>();

            // Настройки компонентов врага
            enemyController.moveSpeed = Random.Range(2f, 4f);
            enemyController.maxHealth = Random.Range(2, 5);
            enemyController.deathParticles = Resources.Load<ParticleSystem>("DeathParticles"); // Путь к префабу частиц смерти в Resources

            // Замораживаем вращение по оси Z для Rigidbody2D врага
            enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
}
