using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesInstantiator : MonoBehaviour
{
    [SerializeField]
    private PlacingManager _placingManager;

    [Serializable]
    public struct AssociatedPrefab
    {
        public string name;
        public GameObject prefab;
    }


    [SerializeField]
    private List<AssociatedPrefab> m_gamesAssociations = new List<AssociatedPrefab>();
    private Dictionary<string, GameObject> _gamesAssociations = new Dictionary<string, GameObject>();

    private string _currentName = "";

    #region MonoBehaviour

    private void Start()
    {
        foreach (var associatedGame in m_gamesAssociations)
        {
            _gamesAssociations.Add(associatedGame.name, associatedGame.prefab);
        }
    }

    private void OnEnable()
    {
        _placingManager.OnTrackableDetected += OnTrackableDetected;
    }

    private void OnDisable()
    {
        _placingManager.OnTrackableDetected -= OnTrackableDetected;
    }

    #endregion

    private void OnTrackableDetected(string referenceName, TrackablePrefab trackablePrefab, object obj)
    {
        if (_currentName != referenceName) return;
        if (_gamesAssociations.TryGetValue(referenceName, out GameObject go))
        {
            GameObject associatedGO = Instantiate(go);
            trackablePrefab.AssociatedGO = associatedGO;
            _placingManager.TrackableRecognized = true;
        }
    }

    #region Methods

    public void SetCurrentGame(string gameName)
    {
        _currentName = gameName;
        _placingManager.RestartTracking();
    }

    #endregion

}
