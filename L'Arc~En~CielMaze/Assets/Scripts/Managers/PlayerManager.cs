using UnityEngine;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    private PlayerCharacterController player;
    public GameObject playerCharacterPrefab;
    public CinemachineVirtualCamera[] virtualCameras;
    public PlayerStats playerStats;
    private int objectivesCollected = 0;
    private Level level;
    private FoW.FogOfWarUnit fowUnit;
    private void Awake()
    {
        CodeControl.Message.AddListener<GeneratePlayerCharacterRequestEvent>(OnPlayerCharacterGenerationRequested);
        CodeControl.Message.AddListener<GenerateGridRequestEvent>(OnGridGenerationRequested);
        CodeControl.Message.AddListener<EnemyContactEvent>(OnEnemyContact);
        CodeControl.Message.AddListener<ObjectiveCollectedEvent>(OnObjectiveCollected);
        CodeControl.Message.AddListener<PlayerPointOfViewChangeRequestEvent>(OnPointOfViewChangeRequested);
        playerStats = new PlayerStats(10, 5);
    }

    private void OnPointOfViewChangeRequested(PlayerPointOfViewChangeRequestEvent obj)
    {
        CinemachineBrain.SoloCamera = virtualCameras[(object)CinemachineBrain.SoloCamera == virtualCameras[0] ? 1 : 0];
    }

    private void OnGridGenerationRequested(GenerateGridRequestEvent obj)
    {
        level = obj.level;
    }

    private void OnObjectiveCollected(ObjectiveCollectedEvent obj)
    {
        objectivesCollected++;
        Color rainbowColorWithCorrectIntensity = RainbowColorReference.UsableRainbowColor(obj.color);
        rainbowColorWithCorrectIntensity.a = 255 * ((objectivesCollected + 1) / level.objectiveAmount);
        player.ChangeLightColor(rainbowColorWithCorrectIntensity);
    }

    private void OnEnemyContact(EnemyContactEvent obj)
    {
        playerStats.health -= obj.enemy.damage;
        player.ChangeLightRange(playerStats.health);
        if (playerStats.health > 0)
        {
            Destroy(obj.transform.gameObject);
            DispatchPlaySFXRequestEvent("hurt" + UnityEngine.Random.Range(1, 3).ToString(), 0.5f);
        }
        else if (playerStats.health <= 0)
        {
            DispatchPlayerDeathEvent();
        }
    }

    private void OnPlayerCharacterGenerationRequested(GeneratePlayerCharacterRequestEvent obj)
    {
        objectivesCollected = 0;
        GameObject playerCharacter = Instantiate(playerCharacterPrefab, obj.cellParent);
        fowUnit = playerCharacter.AddComponent<FoW.FogOfWarUnit>();
        fowUnit.circleRadius = 12.5f;
        fowUnit.lineOfSightMask = ~0;
        player = playerCharacter.GetComponent<PlayerCharacterController>();
        player.transform.localPosition = obj.cell.SpawnOverCellLocalPosition(1);
        virtualCameras[0].LookAt = player.transform;
        virtualCameras[0].Follow = player.transform;      
        DispatchPlaySFXRequestEvent("playerborn", 0.5f);
    }

    private void DispatchPlayerDeathEvent()
    {
        CodeControl.Message.Send(new PlayerDeathEvent());
    }

    private void DispatchPlaySFXRequestEvent(string sfxName, float volume)
    {
        CodeControl.Message.Send(new PlaySFXRequestEvent(sfxName, volume));
    }
}
