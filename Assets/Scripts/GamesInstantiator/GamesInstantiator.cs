using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesInstantiator : MonoBehaviour
{
    [SerializeField]
    private PlacingManager _placingManager;

    public delegate void Handler(bool isActive);
    public event Handler HideCanvas;

    [Serializable]
    public struct AssociatedPrefab
    {
        public string name;
        public GameObject prefab;
    }


    [SerializeField]
    private List<AssociatedPrefab> m_gamesAssociations = new List<AssociatedPrefab>();
    private Dictionary<string, GameObject> _gamesAssociations = new Dictionary<string, GameObject>();

    public List<string> list_Names = new List<string>();

    private string _currentName = "";

    #region MonoBehaviour

    public void GetCollections()
    {
        foreach (var associatedGame in m_gamesAssociations)
        {
            _gamesAssociations.Add(associatedGame.name, associatedGame.prefab);
            list_Names.Add(associatedGame.name);
        }
    }
    private void OnEnable()
    {
        _placingManager.OnTrackableDetected += OnTrackableDetected;
    }

    private void OnDisable()
    {
        _placingManager.OnTrackableDetected -= OnTrackableDetected;
        _currentName = "";
    }

    #endregion

    private bool OnTrackableDetected(string referenceName, TrackablePrefab trackablePrefab, object obj)
    {
        if (_currentName != referenceName)
        {
            print($"Current name not equal reference name!");
            return false;
        }
  
        if (_gamesAssociations.TryGetValue(referenceName, out GameObject go))
        {

            if (trackablePrefab.AssociatedGO != null) return true;

            trackablePrefab.AssociatedGO = Instantiate(go);
            trackablePrefab.AssociatedGO.transform.eulerAngles = new Vector3(0, trackablePrefab.AssociatedGO.transform.eulerAngles.y+270, 0);
            _placingManager.TrackableRecognized = true;
            HideCanvas(false);
            return true;
        }
        return false;
    }




    #region Methods

    public void SetCurrentGame(string gameName)
    {
        _currentName = gameName;
        _placingManager.RestartTracking();
    }

    #endregion

}
