using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeTime = 2.5f;
    private bool isHitAster;
    private GameObject target;


    void Start()
    {
        Invoke("Destroy", LifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string name = collision.gameObject.name;
        Asteroid aster_parent = collision.gameObject.GetComponent<Asteroid>();
        if (name.Substring(0, 5) == "aster" && !isHitAster && !aster_parent.isShooted)
        {
            // bullet
            isHitAster = true;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0;
            GetComponent<SpriteRenderer>().color = color;

            // target
            aster_parent.isShooted = true;
            target = collision.gameObject;
            string type = name.Substring(6, 1);
            GameObject aster = collision.gameObject.transform.Find("asteroids_anim").gameObject;
            GameObject bang = collision.gameObject.transform.Find("bang").gameObject;
            aster.GetComponent<Animator>().Play("AsterFade" + type);
            bang.SetActive(true);

            // score
            transform.parent.transform.parent.GetComponent<PlayerControl>().AddScore();

            // to destroy
            Invoke("TargetDestroy", 1f);
        }
    }

    private void TargetDestroy()
    {
        Destroy(target);
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
