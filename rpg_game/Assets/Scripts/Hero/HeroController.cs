using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    private SpriteRenderer sr;
    float moveSpeed = 0.8f;
    private Rigidbody2D body;
    private Animator animator;
    private GameObject controller;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        body.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        // Sprite Renderer Logic
        if (horizontalInput > 0) {
            sr.flipX = false;
        } else if (horizontalInput < 0) {
            sr.flipX = true;
        }

        // Animation Logic
        if (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput) != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enemy = collision.gameObject; 
        if (enemy.GetComponent<Enemy>() != null)
        {
            // Disable Game Mechanics
            Time.timeScale = 0;
            FindObjectOfType<Camera>().GetComponent<AudioListener>().enabled = false;

            // BattleView
            SceneManager.LoadScene("BattleView", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += OnBattleViewStart;

            // Have GameController Populate BattleView with Enemy
            controller.GetComponent<GameController>().BattleViewEnemyData(enemy.GetComponent<Enemy>().enemyData);
        }
    }

    private void OnBattleViewStart(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= OnBattleViewStart;
        StartCoroutine(BattleViewStart(scene));
    }

    private IEnumerator BattleViewStart(Scene scene)
	{
		while (scene.isLoaded == false)
		{
			yield return new WaitForEndOfFrame();
		}

        SceneManager.SetActiveScene(scene);

        yield return new WaitForEndOfFrame();

        GameObject blurPanel = GameObject.FindGameObjectWithTag("blurPanel");
        blurPanel.GetComponent<Krivodeling.UI.Effects.UIBlur>().BeginBlur(5f);

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.SetActive(false);

        GameObject battleviewCamera = GameObject.FindGameObjectWithTag("battleviewCamera");
        battleviewCamera.SetActive(true);
        battleviewCamera.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);
    }
}
