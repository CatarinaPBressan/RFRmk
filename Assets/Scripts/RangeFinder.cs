using UnityEngine;
using System.Collections;

public class RangeFinder : MonoBehaviour {

    public RangeType Range;

    private TrackingTransformBehaviour tracker;
    private GateBehaviour gate;

    void Start()
    {
        tracker = this.transform.parent.GetComponent<TrackingTransformBehaviour>() as TrackingTransformBehaviour;
        gate = this.transform.parent.GetComponent<GateBehaviour>() as GateBehaviour;
    }

    void OnTriggerEnter(Collider enterer)
    {
        if (tracker)
        {
            switch (Range)
            {
                case RangeType.Tracking:
                    tracker.StartTracking(enterer);
                    break;
                case RangeType.MaxRange:
                    tracker.StartShooting();
                    break;
                case RangeType.MinRange:
                    tracker.StopShooting();
                    break;
            }
        }
        if (gate)
        {
            gate.Open(enterer);
        }
    }

    void OnTriggerExit(Collider exiter)
    {
        if (tracker)
        {
            switch (Range)
            {
                case RangeType.Tracking:
                    tracker.StopTracking(exiter);
                    break;
                case RangeType.MaxRange:
                    tracker.StopShooting();
                    break;
                case RangeType.MinRange:
                    tracker.StartShooting();
                    break;
            }
        }
        if (gate)
        {
            gate.Close(exiter);
        }
    }
}
