using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IControllable {

	private int jumpInUse = 0;
    [SerializeField]
    private float airWait = 0.15f;
    [SerializeField]
    private float platformWait = 0.075f;

    private bool freefall = false;
    private bool firstMoved = false;
    private bool killedByEnemy = false;

    public float score = 0;
    public bool started = false;
    int fallController = 0;
    int t = 1;
    Animator m_Animator;
    GameObject particleObject;
    GameObject hitParticle;
    public Transform playerXform;
    public Rigidbody2D rb;
    float forceMultiplier = 65f;
    
    AudioSource[] audios;
    //private new Transform playerXform;
    public void Initialize() {
        //playerXform = GetComponent<Transform>();
    }
    public void Start() {
        playerXform = transform;
        rb = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        Restart.Instance.anim = m_Animator;
        audios = GetComponents<AudioSource>();
    }

    public void Jump(Vector2 dir) {

        if (playerXform.position.x <= 1000) {
            if (playerXform.position.x == 1000f) {
                dir.x += 2;
                dir.y += 1;
            }
            else if (playerXform.position.x == 750f) {
                dir.x += 2;
                dir.y += 1;
            }
            else if (playerXform.position.x == 500f) {
                dir.x += 2;
                dir.y += 1;
            }
            else if (playerXform.position.x == 250f) {
                dir.x += 2;
                dir.y += 1;
            }
        }

        started = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_Animator.SetTrigger("jump");
        rb.AddForce(dir * forceMultiplier);

    }

    public void SpecialJump() {

        if (GameController.Instance.paused == false ) {
            if (firstMoved == false) {
                firstMoved = true;
            }
            else {
                m_Animator.SetTrigger("dive");
            }
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(FreeFallPrep());
        }
        
    }
    IEnumerator FreeFallPrep() {
        yield return new WaitForSeconds(airWait);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        freefall = true;
    }

    void FixedUpdate () {
        if (started == false) {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else {
            jumpInUse++;
        }
        InputManager.Instance.SetText((int)transform.position.x);
        if(freefall == true) {
            rb.velocity = Vector2.zero;  
            playerXform.position -= new Vector3(0, 0.5f * Mathf.Sqrt(t), 0) /2;
            t++;
        }
        else {
            if (rb.velocity.y < -1f && jumpInUse > 0 ) {
                if(fallController == 2 ) {
                    m_Animator.SetTrigger("fall");
                }
                else if (fallController == 0 || fallController == 1 ) {
                    fallController++;
                }
            }
            else {
                fallController = 0;
            }
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D col) {
        playerXform.position += Vector3.up * 0.1f;
        m_Animator.SetTrigger("land");
        CreateLandParticle(playerXform.position);

        freefall = false;
        t = 0;
        rb.gravityScale = 1;
        rb.velocity = Vector3.zero;

        if (col.gameObject.tag == "BottomCollider") {
            Restart.Instance.Replay();
            killedByEnemy = true;
        }

        else if (col.gameObject.tag == "Normal" || col.gameObject.tag == "Bouncy" || col.gameObject.tag == "TopCollider") {
            Vector3 hit = col.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);
            if (angle < 5) {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                yield return new WaitForSeconds(platformWait);
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                if(col.gameObject.tag == "TopCollider" && killedByEnemy == false) {
                    Jump(new Vector2(12f, 7f));
                    audios[2].Play();

                    particleObject = ObjectPooler.Instance.GetPooledObject("Particle");
                    particleObject.transform.position = col.transform.position;
                    particleObject.SetActive(true);
                    col.transform.parent.gameObject.SetActive(false);
                }

                else if (col.gameObject.tag == "Bouncy") {
                    Jump(new Vector2(20f, 9f));
                    audios[1].Play();
                }
                else {
                    Jump(new Vector2(12f, 9f));
                    audios[0].Play();
                }
                jumpInUse = 0;
            }
            else {
                SpecialJump();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
    Restart.Instance.Replay();
    }

    void Update() {
        if (particleObject != null) {
            if (particleObject.transform.position.x < (CameraController.Instance.transform.position.x - 20f)) {
                particleObject.SetActive(false);
            }
        }
        if(hitParticle != null && !hitParticle.GetComponent<ParticleSystem>().IsAlive()) {
            hitParticle.SetActive(false);
        }
    }

    public void CreateLandParticle (Vector3 pos) {
        hitParticle = ObjectPooler.Instance.GetPooledObject("HitParticle");
        hitParticle.transform.position = pos + Vector3.up * 2f ;
        hitParticle.SetActive(true);
    }

    #region IControllable implementation
    int IControllable.JumpInUse {
		get {
			return jumpInUse;
		}
	}
	#endregion

	#region IControllable implementation

	Vector3 IControllable.Position {
		get {
			return transform.position;
		}
	}

	#endregion
 }