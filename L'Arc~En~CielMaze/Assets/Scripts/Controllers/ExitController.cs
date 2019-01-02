using System;
using UnityEngine;

public class ExitController : BasePickup
{
    private new Light light;
    private bool increaseLightRange;
    private new Renderer renderer;
    protected override void Awake()
    {
        base.Awake();
        CodeControl.Message.AddListener<AllObjectivesCollectedEvent>(OnAllObjectivesCollected);
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
            light.range += Time.deltaTime * 2;
            if (light.range >= 25) increaseLightRange = false;
        }
        else
        {
            light.range -= Time.deltaTime * 2;
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
}
