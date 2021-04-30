using UnityEngine;

public class MousePos : MonoBehaviour
{
    
    
    #region SingleTon

    private static MousePos _instance;

    public static MousePos instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
        }
    }

    #endregion
    
    public Touch _touch;
    public Vector3 pos;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                pos = Vector3.zero;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y,transform.position.z));
                Debug.Log(pos);
            }
        }
    }
}
