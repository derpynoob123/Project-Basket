using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Phase settings
public struct PhaseSetting
{
    public static string phaseName;
    public static float nextPhaseTimer;
}

public sealed class PhaseManager : MonoBehaviour
{
    int currentPhase;
    bool lastPhase;

    public void Phase(int phaseIndex)
    {
        PhaseSetting.phaseName = "Phase " + phaseIndex;
        Debug.Log("Current Phase: " + PhaseSetting.phaseName);

        if (phaseIndex == 1)
        {
            PhaseSetting.nextPhaseTimer = 12f;
            
            if (!GameManager.Singleton.isTutorial)
            {
                SpawnerSetting.minimumSpawnTime = 1f;
                SpawnerSetting.maximumSpawnTime = 1.5f;
                SpawnerSetting.spawnerActive = true;
            }
            else
            {
                SpawnerSetting.minimumSpawnTime = 2.5f;
                SpawnerSetting.maximumSpawnTime = 2.55f;
            }
            
            ObjectPool.SharedInstance.AddToActivePool(ObjectPool.SharedInstance.subPools[0]);
            ObjectPool.SharedInstance.AddToActivePool(ObjectPool.SharedInstance.subPools[1]);

        }
        if (phaseIndex == 2)
        {
            PhaseSetting.nextPhaseTimer = 8f;
            SpawnerSetting.minimumSpawnTime = 0.4f;
            SpawnerSetting.maximumSpawnTime = 1f;
            SpawnerSetting.spawnerActive = true;
            ObjectPool.SharedInstance.AddToActivePool(ObjectPool.SharedInstance.subPools[2]);
            ObjectPool.SharedInstance.AddToActivePool(ObjectPool.SharedInstance.subPools[3]);
        }
        if (phaseIndex == 3)
        {
            lastPhase = true;
            SpawnerSetting.minimumSpawnTime = 0.2f;
            SpawnerSetting.maximumSpawnTime = 0.4f;
            SpawnerSetting.spawnerActive = true;
        }
        
    }

    private void Awake()
    {
        lastPhase = false;
        currentPhase = 1;
    }

    private void Start()
    {
        Phase(currentPhase);
    }

    private void Update()
    {
        if (!GameManager.Singleton.paused && ! GameManager.Singleton.gameOver)
        {
            if (!GameManager.Singleton.isTutorial)
            {
                PhaseSetting.nextPhaseTimer -= Time.deltaTime;
            }
            
            if (!lastPhase && PhaseSetting.nextPhaseTimer <= 0 )
            {
                currentPhase++;
                Phase(currentPhase);
            }
        }
    }
}
