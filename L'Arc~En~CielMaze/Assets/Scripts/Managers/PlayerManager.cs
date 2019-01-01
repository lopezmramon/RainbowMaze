using UnityEngine;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    private PlayerCharacterController player;
    public GameObject playerCharacterPrefab;
    public CinemachineVirtualCamera virtualCamera;
    public PlayerStats playerStats;

    private void Awake()
    {
        CodeControl.Message.AddListener<GeneratePlayerCharacterRequestEvent>(OnPlayerCharacterGenerationRequested);
        CodeControl.Message.AddListener<EnemyContactEvent>(OnEnemyContact);
        playerStats = new PlayerStats(5, 5);
    }

    private void OnEnemyContact(EnemyContactEvent obj)
    {
        playerStats.health -= obj.enemy.damage;
        Debug.Log(playerStats.health);
        if(playerStats.health > 0)
        {
            Destroy(obj.transform.gameObject);
        }
    }

    private void OnPlayerCharacterGenerationRequested(GeneratePlayerCharacterRequestEvent obj)
    {
        GameObject playerCharacter = Instantiate(playerCharacterPrefab, obj.cellParent);
        player = playerCharacter.GetComponent<PlayerCharacterController>();
        player.transform.localPosition = obj.cell.SpawnOverCellLocalPosition(1);
        virtualCamera.LookAt = player.transform;
        virtualCamera.Follow = player.transform;
    }
}
