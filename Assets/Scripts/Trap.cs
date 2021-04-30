using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyComponent = other.GetComponent<Snake>();
            if (enemyComponent.isFever == false)
            {
                UIManager.Instance.Death();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
