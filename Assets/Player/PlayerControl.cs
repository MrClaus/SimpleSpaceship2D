using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float PositionTop = 5.4f;
    public float PositionBottom = -5.4f;
    public float PositionLeft = -8.8f;
    public float PositionRight = 8.8f;

    public float ShootTime = 0.5f;
    public GameObject Fire;
    public Transform FirePos;
    public Transform background;
    public Animator AnimJetLeft;
    public Animator AnimJetRight;

    private Rigidbody2D _rb;
    private bool isShooting;
    private bool isCrashed;

    public UI GameUI;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (!isCrashed) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            v = (v >= 0) ? v : 0;

            // move spaceship
            _rb.angularVelocity = -256f * h * Time.deltaTime * 50f;
            _rb.velocity = transform.up * v * Time.deltaTime * 400f;
            //_rb.AddForce(transform.up * Time.deltaTime * 100f * v);

            // move background
            background.Translate(transform.up * 0.5f * Time.deltaTime * -v);
            if (background.position.y > 2)
                background.position = new Vector3(background.position.x, 2, background.position.z);
            if (background.position.y < -2)
                background.position = new Vector3(background.position.x, -2, background.position.z);
            if (background.position.x > 4.45)
                background.position = new Vector3(4.45f, background.position.y, background.position.z);
            if (background.position.x < -4.45)
                background.position = new Vector3(-4.45f, background.position.y, background.position.z);

            // shoot process
            if (Input.GetKey(KeyCode.Space) && !isShooting) {
                isShooting = true;
                GameObject shoot = Instantiate(Fire, FirePos.position, transform.rotation);
                shoot.transform.SetParent(FirePos);
                Invoke("Reshooting", ShootTime);
            }

            // anim moving spaceship
            if (v != 0) {
                AnimJetLeft.SetBool("isShow", true);
                AnimJetRight.SetBool("isShow", true);
            } else {
                AnimJetLeft.SetBool("isShow", false);
                AnimJetRight.SetBool("isShow", false);
            }
        }

        // Quit
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    // Reloading weapons
    private void Reshooting()
    {
        isShooting = false;
    }

    // Collisions with screen-borders
    private void OnTriggerEnter2D(Collider2D border)
    {
        string name = border.gameObject.name;
        var pos = transform.position;
        if (name == "border_left")
            pos = new Vector3(PositionRight, transform.position.y, 0f);
        if (name == "border_right")
            pos = new Vector3(PositionLeft, transform.position.y, 0f);
        if (name == "border_up")
            pos = new Vector3(transform.position.x, PositionBottom, 0f);
        if (name == "border_down")
            pos = new Vector3(transform.position.x, PositionTop, 0f);
        transform.position = pos;
    }

    // Collisions with asteroids
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.Find("smoke").gameObject.SetActive(true);
        isCrashed = true;

        GameUI.Loose();
    }

    // Game restart
    public void Restart()
    {
        transform.Find("smoke").gameObject.SetActive(false);
        transform.position = new Vector3(0, 0, transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        isCrashed = false;
    }

    // Getting scores
    public void AddScore()
    {
        GameUI.AddScore();
    }
}
