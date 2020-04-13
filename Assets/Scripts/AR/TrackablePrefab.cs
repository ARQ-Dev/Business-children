using UnityEngine;

public class TrackablePrefab : MonoBehaviour
{
    public GameObject AssociatedGO { set; private get; }

    public PlacingManager.PlacingType _placingType;

    public void UpdatePosition()
    {
        if (AssociatedGO == null) return;
        AssociatedGO.transform.position = transform.position;
        AssociatedGO.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void DiscardInstantiatedPrefab()
    {
        if (_placingType == PlacingManager.PlacingType.instantiate)
        {
            Destroy(AssociatedGO);
            AssociatedGO = null;
        }
        else
        {
            if (AssociatedGO == null) return;

            foreach (Transform tr in AssociatedGO.transform)
            {
                tr.gameObject.SetActive(false);
            }
            IRestartable restartComponent = AssociatedGO.GetComponent<IRestartable>();
            if (restartComponent != null) restartComponent.Restart();
            AssociatedGO = null;
        }
    }
}
