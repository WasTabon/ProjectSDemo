using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 5;
    public ParticleSystem deathParticles;

    public Color _startCollor = Color.yellow;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int currentHealth;
    private BoxCollider2D triggerCollider;
    private bool isTakingDamage = false;

    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>().transform;
        currentHealth = maxHealth;

        // Добавляем второй BoxCollider2D с триггером
        triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;

        // Увеличиваем размер триггерного коллайдера в 2 раза
        triggerCollider.size = spriteRenderer.bounds.size * 2f;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.position;
            Vector3 direction = playerPosition - transform.position;
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            // Flip the enemy sprite based on the movement direction
            if (direction.x > 0)
                spriteRenderer.flipX = false;
            else if (direction.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage()
    {
        if (!isTakingDamage) // Проверяем, не происходит ли уже анимация FlashRed
        {
            isTakingDamage = true;
            currentHealth--;
            StartCoroutine(FlashRed());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator FlashRed()
    {
        isTakingDamage = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = _startCollor;
        isTakingDamage = false;
    }

    private void Die()
    {
        // Play death particles and destroy the enemy
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // Обрабатываем столкновение с триггерным коллайдером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Здесь можно добавить код, который активируется при столкновении с игроком
            // Например, нанести урон игроку, если враг коснулся его
        }
    }
}
