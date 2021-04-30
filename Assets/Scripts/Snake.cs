using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;


public class Snake : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private GameObject _bonePrefab;
    
    private Vector3 _myPos;
    private Vector3 _defaultPos;
    private MeshRenderer _myMesh;
    private MeshRenderer _enemyMesh;

    public float speed;
    public bool isFever;
    private float angle;

    private void Start()
    {
        _myMesh = GetComponent<MeshRenderer>();
        UIManager.Instance.Init += () => Fever();
    }

    private void Update()
    {
        MoveTail();
        Move();
        
        if (!isFever)
        {
            MoveX();
        }
        else
        {
            transform.rotation =  Quaternion.Lerp(transform.rotation, Quaternion.identity,2);
            _defaultPos = new Vector3(0, 0.25f, transform.position.z);
            transform.position = Vector3.Lerp(transform.localPosition, _defaultPos, 2 * Time.deltaTime);
        } 
    }

    private void Move()
    {
        transform.position = transform.position + transform.forward * (speed * Time.deltaTime);
    }

    private void MoveTail()
    {
        float sqrDistance = Mathf.Sqrt(_distance);
        Vector3 previousPosition = transform.position;

        foreach (var bone in _tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 currentBonePosition = bone.position;
                bone.position = previousPosition;
                previousPosition = currentBonePosition;
            }
            else
            {
                break;
            }
        }
    }

    
    private void MoveX()
    {
        
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float direction = 0;
            var touch = Input.GetTouch(0).deltaPosition.x;
            Debug.Log(touch);
            if (touch > 5)
            {
                direction = 3.5f;
            }
                
            else if(touch < -5)
            {
                direction = -3.5f;
            }
            
            
            _myPos = transform.position;
            _myPos = new Vector3(transform.position.x + direction * Time.deltaTime, _myPos.y,
                _myPos.z);
            _myPos.x = Mathf.Clamp(_myPos.x, -2.4f, 2.4f);
            transform.position = _myPos;
        }
        
        //USE THIS CODE FOR ROTATION MOVEMENT
        
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            
            var touch = Input.GetTouch(0).deltaPosition.x;
            Debug.Log(touch);
            if (touch > 2)
            {
                angle = 2;
            }
                
            else if(touch < -2)
            {
                angle = -2;
            }
            
            
        }
        else if(Input.touchCount == 0)
        {
            transform.rotation =  Quaternion.Lerp(transform.rotation, Quaternion.identity,2);
        }
        
        angle = angle * speed * 10f * Time.fixedDeltaTime;
        transform.Rotate(0,angle,0);
        
        float minRotation = -45;
        float maxRotation = 45;
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler (currentRotation);*/

    }

    
    private void Fever()
    {
        isFever = true;
        speed *= 3;
        StartCoroutine(FeverTimer());
    }

    private IEnumerator FeverTimer()
    {
        yield return new WaitForSeconds(3);
        isFever = false;
        speed /= 3;
        StopCoroutine(FeverTimer());
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Eat"))
        {
            _enemyMesh = other.GetComponent<MeshRenderer>();
            if (_myMesh.material.color != _enemyMesh.material.color)
            {
                if (!isFever)
                {
                    UIManager.Instance.Death();
                }
                else if(isFever)
                {
                    Destroy(other.gameObject);
                    UIManager.Instance.PlusEat();
                }
                
            }
                
            Destroy(other.gameObject);
            UIManager.Instance.PlusEat();
            GameObject bone = Instantiate(_bonePrefab);
            _tails.Add(bone.transform);
        }
    }

    public void SwitchColorYeah()
    {
        foreach (var bones in _tails)
        {
            var gameobject = bones.gameObject.GetComponent<MeshRenderer>();
            gameobject.material.color = _myMesh.material.color;
        }
    }
}
