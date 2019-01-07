using System;
using UnityEngine;

public class ExitController : BasePickup
{
    private new Light light;
    private bool increaseLightRange;
    private new Renderer renderer;
    private FoW.FogOfWarUnit fogOfWarUnit;
    protected override void Awake()
    {
        base.Awake();
        CodeControl.Message.AddListener<AllObjectivesCollectedEvent>(OnAllObjectivesCollected);
        fogOfWarUnit = gameObject.AddComponent<FoW.FogOfWarUnit>();
        fogOfWarUnit.lineOfSightMask = ~0;
        fogOfWarUnit.lineOfSightPenetration = 0.5f;
        fogOfWarUnit.circleRadius = 8.5f;
    }

    private void OnAllObjectivesCollected(AllObjectivesCollectedEvent obj)
    {
        light.color = Color.green;
        light.intensity *= 2;
        renderer.material.color = Color.green;
    }

    private void Start()
    {
        light = gameObject.AddComponent<Light>();
        light.range = 10;
        light.intensity = 1.5f;
        light.color = Color.red;
        increaseLightRange = false;
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (light == null) return;
        if (increaseLightRange)
        {
            light.range += Time.deltaTime * 5;
            if (light.range >= 25) increaseLightRange = false;
        }
        else
        {
            light.range -= Time.deltaTime * 5;
            if (light.range <= 0) increaseLightRange = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchExitContactEvent();
        }
    }

    private void DispatchExitContactEvent()
    {
        CodeControl.Message.Send(new ExitContactEvent(transform));
    }

    private void OnDestroy()
    {
        CodeControl.Message.RemoveListener<AllObjectivesCollectedEvent>(OnAllObjectivesCollected);
    }
}
