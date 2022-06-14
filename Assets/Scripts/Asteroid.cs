using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static float xLeft = -10f;
    public static float xRight = 10f;    
    public static float yBottom = -6.5f;
    public static float yTop = 6.5f;
    public static int count;

    public float ImpulsForce = 0.001f;
    private Rigidbody2D _rb;

    public bool isShooted;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        count++;
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xLeft || transform.position.x > xRight ||
            transform.position.y > yTop || transform.position.y < yBottom) {            
            Destroy(gameObject);
        }            

        _rb.AddForce(-transform.position * ImpulsForce, ForceMode2D.Impulse);        
    }

    private void OnDestroy()
    {
        count--;
    }
}
