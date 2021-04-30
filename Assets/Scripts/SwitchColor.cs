using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    private MeshRenderer _myRend;
    private MeshRenderer _playerRend;
    private Snake _playerScript;
    
    private void Start()
    {
        _myRend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRend = other.gameObject.GetComponent<MeshRenderer>();
            _playerScript = other.gameObject.GetComponent<Snake>();
            _playerRend.material.color = _myRend.material.color;
            _playerScript.SwitchColorYeah();
        }
    }
}
