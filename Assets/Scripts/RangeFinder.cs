using UnityEngine;
using System.Collections;

public class RangeFinder : MonoBehaviour {

    public RangeType Range;

    private TrackingTransformBehaviour Tracker;
    private GateBehaviour Gate;

    void Start()
    {
        Tracker = this.transform.parent.GetComponent<TrackingTransformBehaviour>() as TrackingTransformBehaviour;
        Gate = this.transform.parent.GetComponent<GateBehaviour>() as GateBehaviour;
    }

    void OnTriggerEnter(Collider enterer)
    {
        if (Tracker)
        {
            switch (Range)
            {
                case RangeType.Tracking:
                    Tracker.StartTracking(enterer);
                    break;
                case RangeType.MaxRange:
                    Tracker.StartShooting();
                    break;
                case RangeType.MinRange:
                    Tracker.StopShooting();
                    break;
            }
        }
        if (Gate)
        {
            Gate.Open(enterer);
        }
    }

    void OnTriggerExit(Collider exiter)
    {
        if (Tracker)
        {
            switch (Range)
            {
                case RangeType.Tracking:
                    Tracker.StopTracking(exiter);
                    break;
                case RangeType.MaxRange:
                    Tracker.StopShooting();
                    break;
                case RangeType.MinRange:
                    Tracker.StartShooting();
                    break;
            }
        }
        if (Gate)
        {
            Gate.Close(exiter);
        }
    }
}
