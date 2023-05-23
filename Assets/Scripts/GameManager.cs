using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour[] possibleEnemies;
    [SerializeField] private PlayerHealth playerPrefab;
    [SerializeField] private int numberEnemies;
    [SerializeField] private InputController inputController;

    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text resultText;
    private List<EnemyBehaviour> spawnedEnemies = new();
    private PlayerHealth spawnedPlayer;

    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        spawnedPlayer = Instantiate(playerPrefab);
        spawnedPlayer.transform.position = Vector3.zero;
        spawnedPlayer.Died += Loose;
        InitializeInputController();
        InstanceEnemies();
    }
    private void InstanceEnemies()
    {
        for (int i = 0; i < numberEnemies; i++)
        {
            var enemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Length)]);
            spawnedEnemies.Add(enemy);
            enemy.GetComponent<Health>().Died += () => OnDieEnemy(enemy);
            enemy.transform.position = GetRandomSpawnPoint();
            enemy.Initialize(spawnedPlayer);
        }
    }
    private void InitializeInputController()
    {
        inputController.Initialize(spawnedPlayer.GetComponent<PlayerCombatSystem>(), spawnedPlayer.GetComponent<PlayerMovementController>());
    }
    public void ResetGame()
    {
        foreach (var enemy in spawnedEnemies)
        {
            Destroy(enemy.gameObject);
        }

        spawnedEnemies.Clear();

        if (spawnedPlayer != null)
        {
            Destroy(spawnedPlayer.gameObject);
        }

        endGamePanel.SetActive(false);
        StartGame();
    }
    private void Win()
    {
        endGamePanel.SetActive(true);
        resultText.text = "You Win";
    }
    private void Loose()
    {
        endGamePanel.SetActive(true);
        resultText.text = "You Loose";
    }
    private void OnDieEnemy(EnemyBehaviour enemyBehaviour)
    {
        spawnedEnemies.Remove(enemyBehaviour);
        if (spawnedEnemies.Count == 0)
        {
            Win();
        }
    }
    private Vector3 GetRandomSpawnPoint()
    {
        float randomXCoordinate = UnityEngine.Random.Range(-AstarPath.active.data.gridGraph.width / 2, AstarPath.active.data.gridGraph.width / 2);
        float randomYCoordinate = UnityEngine.Random.Range(-AstarPath.active.data.gridGraph.depth / 2, AstarPath.active.data.gridGraph.depth / 2);
        Vector2 randomPoint = new Vector2(randomXCoordinate, randomYCoordinate);
        var nearestPoint = AstarPath.active.GetNearest(randomPoint);
        return new Vector3(nearestPoint.position.x, nearestPoint.position.y, 0);
    }
}
