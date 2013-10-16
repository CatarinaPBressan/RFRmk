using UnityEngine;
using System.Collections;

public class RangeFinder : MonoBehaviour {

    public RangeType Range;

    private TrackingTransformBehaviour tracker;

    void Start()
    {
        tracker = this.transform.parent.GetComponent<TrackingTransformBehaviour>() as TrackingTransformBehaviour;
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
    }
}
