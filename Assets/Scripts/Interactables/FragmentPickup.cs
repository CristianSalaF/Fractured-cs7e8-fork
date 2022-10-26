using UnityEngine;

public class FragmentPickup : MonoBehaviour
{
    [SerializeField] private int _fragmentValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc != null) 
            {
                pc.PlayPickupSFX();
            }
            
            other.transform.localScale += (Vector3.one * (_fragmentValue / 10f));
            
            gameObject.SetActive(false);
        }
    }
}
