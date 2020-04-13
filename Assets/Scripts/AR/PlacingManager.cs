using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;

[RequireComponent(typeof(ARSessionOrigin))]
[RequireComponent(typeof(ARTrackedImageManager))]
public class PlacingManager : MonoBehaviour
{
    public enum PlacingType
    {
        instantiate,
        activate
    }

    [SerializeField]
    private GameObject _trackedImagePrefab;

    [SerializeField]
    private bool _trackingFromStart = false;

    private ARTrackedImageManager _imageTrackingManager;

    private List<TrackablePrefab> _trackablePrefabs = new List<TrackablePrefab>();

    public bool TrackableRecognized { get; set; } = false;

    private bool _isActive = false;

    public event Action<string, TrackablePrefab, object> OnTrackableDetected;

    #region MonoBehavior

    private void OnEnable()
    {
        _imageTrackingManager = GetComponent<ARTrackedImageManager>();
        _imageTrackingManager.trackedImagesChanged += OnTrackedImageChanged;

        if (_trackingFromStart)
        {
            StartTracking();
        }
        else
        {
            StopTracking();
        }
    }

    private void OnDisable()
    {
        _imageTrackingManager.trackedImagesChanged -= OnTrackedImageChanged;

    }

    #endregion

    #region methods

    public void RestartTracking()
    {
        StartCoroutine(RestartCorutine());
    }

    public void StartTracking()
    {
        if (_isActive) return;
        _isActive = true;
        TrackableRecognized = false;

        _imageTrackingManager.enabled = true;
        _imageTrackingManager.trackedImagePrefab = _trackedImagePrefab;

    }

    public void StopTracking()
    {
        foreach (TrackablePrefab trackablePrefab in _trackablePrefabs)
        {
            trackablePrefab.DiscardInstantiatedPrefab();
        }
        _isActive = false;
    }

    #endregion

    private IEnumerator RestartCorutine()
    {
        StopTracking();
        yield return null;
        StartTracking();
    }

    private void CheckFirstRecognition(ARTrackedImagesChangedEventArgs args)
    {
        if (TrackableRecognized) return;
        if (args.updated.Count > 0)
        {
            TrackablePrefab trackablePrefab = args.updated[0].gameObject.GetComponent<TrackablePrefab>();
            if (trackablePrefab == null) return;
            string imageName = args.updated[0].referenceImage.name;
            _trackablePrefabs.Add(trackablePrefab);
            OnTrackableDetected?.Invoke(imageName, trackablePrefab, args.updated[0]);
        }
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        CheckFirstRecognition(args);

        foreach (ARTrackedImage image in args.updated)
        {
            TrackablePrefab trackablePrefab = image.gameObject.GetComponent<TrackablePrefab>();
            if (trackablePrefab == null) continue;
            trackablePrefab.UpdatePosition();
        }
    }

}
