using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TickController : Singleton<TickController>
{

    [SerializeField]
    private float tickerTime = .3f;

    private float remainingTime;

    public delegate void OnTick();

    private OnTick fastMoverTick;
    private OnTick slowMoverTick;

    private int tickCounter = 0;

    private void Start() {
        remainingTime = tickerTime;
    }

    private void Update() {

        remainingTime -= Time.deltaTime;

        if(remainingTime <= 0) {

            tickCounter++;            
            fastMoverTick?.Invoke();

            if(tickCounter % 2 == 0) {
                slowMoverTick?.Invoke();
            }

            remainingTime = tickerTime;
        }
    }

    public void AddFastMoverAction(OnTick tickEvent) {
        fastMoverTick += tickEvent;
    }

    public void RemoveSlowMoverAction(OnTick tickEvent) {
        slowMoverTick -= tickEvent;
    }

    public void RemoveFastMoverAction(OnTick tickEvent) {
        fastMoverTick -= tickEvent;
    }

    public void AddSlowMoverAction(OnTick tickEvent) {
        slowMoverTick += tickEvent;
    }

    public void ResetActions() {

        fastMoverTick = null;
        slowMoverTick = null;
    }
}
