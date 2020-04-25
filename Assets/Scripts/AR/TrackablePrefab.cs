using UnityEngine;

public class TrackablePrefab : MonoBehaviour
{
    public GameObject AssociatedGO { set; get; }

    public PlacingManager.PlacingType _placingType;


    public void UpdatePosition()
    {
        if (AssociatedGO == null) return;
        AssociatedGO.transform.position = transform.position;
        AssociatedGO.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+180, 0);
        if(AssociatedGO.name[0] == 'B')
            AssociatedGO.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        else if (AssociatedGO.name[0] == 'R')
            AssociatedGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        else if (AssociatedGO.name[0] == 'P')
            AssociatedGO.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public void DiscardInstantiatedPrefab()
    {
        if (_placingType == PlacingManager.PlacingType.instantiate)
        {
            if (AssociatedGO == null)
            {
                print($"Associated prefab is null!");
            }
            else
            {
                print($"Destroy instantiated prefab with name: {AssociatedGO.name}!");
            }

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
