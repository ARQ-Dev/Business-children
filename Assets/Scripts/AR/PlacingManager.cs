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

    public event Func<string, TrackablePrefab, object, bool> OnTrackableDetected ;

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
        TrackableRecognized = false;
        StopTracking();
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
        _imageTrackingManager.enabled = false;
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
        ARTrackedImage image = null;
        foreach (var image_in_camera in args.updated)
        {
            if(image_in_camera.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                image = image_in_camera;
            }
        }
        if (image!=null)
        {
            TrackablePrefab trackablePrefab = image.gameObject.GetComponent<TrackablePrefab>();
            if (trackablePrefab == null) return;
            string imageName = image.referenceImage.name;
            if (!_trackablePrefabs.Contains(trackablePrefab)) _trackablePrefabs.Add(trackablePrefab);
            var recognized = OnTrackableDetected?.Invoke(imageName, trackablePrefab, args.updated[0]);
            TrackableRecognized = recognized ?? false;
        }
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        CheckFirstRecognition(args);
        
        foreach (ARTrackedImage image in args.updated)
        {
            if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                TrackablePrefab trackablePrefab = image.gameObject.GetComponent<TrackablePrefab>();
                if (trackablePrefab == null) continue;
                trackablePrefab.UpdatePosition();
            }
        }
    }

}
