using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;
    [HideInInspector]
    public float step_Length = 0.2f;
    [HideInInspector]
    public float movement_Frequency = 0.1f;

    private float counter;
    private bool move;
    private float speed = 0.001f;

    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> delta_Position;

    private List<Rigidbody> nodes;

    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;
    private bool Alive = true;
    public static bool GameIsOver = false;




    private bool create_Node_At_Tail;
    // Start is called before the first frame update
    void Awake()
    {
        tr = transform;
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();
        

        delta_Position = new List<Vector3>()
        {
            new Vector3(-step_Length,0f),//Left
            new Vector3(0f, step_Length),//Up
            new Vector3(step_Length, 0f),//Right
            new Vector3(0f, -step_Length),//Down
            
        };
        


    }

    private void Start()
    {
        StartCoroutine(Countdown(3));
        Alive = true;

    }

IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        
    }

    public void MoveFaster(List<Rigidbody> nodes)
    {
        if ((nodes.Count - 3) % 10 == 0)
            speed += 0.01f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovmentFrequency();
        CheckGameOver();
        
    }

    private void CheckGameOver()
    {
        if(!Alive)
        {
            FindObjectOfType<EndMeny>().EndMenuUI.SetActive(true);
            FindObjectOfType<EndMeny>().ShowScore();
            Alive = true;
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }


    void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        head_Body = nodes[0];
    }

    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int)PlayerDirection.Count);
        direction = (PlayerDirection)dirRandom;
    }

    void InitPlayer()
    {
        SetDirectionRandom();

        switch(direction)
        {
            case PlayerDirection.Right:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);

                break;
            case PlayerDirection.Left:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);

                break;
            case PlayerDirection.Up:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.Down:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;

        }
    }
    private void Move()
    {
        Vector3 dPosition = delta_Position[(int)direction];

        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;

        main_Body.position += dPosition;
        head_Body.position += dPosition;

        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }
        if(create_Node_At_Tail)
        {
            create_Node_At_Tail = false;
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);

            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());

            MoveFaster(nodes);
        }
    }

    void CheckMovmentFrequency()
    {
        counter += speed + Time.smoothDeltaTime;
        if(counter >= movement_Frequency)
        {
            counter = 0f;
            move = true;
        }
    }
    public void SetInputDirection(PlayerDirection dir)
    {
        if (dir == PlayerDirection.Up && direction == PlayerDirection.Down ||
            dir == PlayerDirection.Down && direction == PlayerDirection.Up ||
            dir == PlayerDirection.Right && direction == PlayerDirection.Left ||
            dir == PlayerDirection.Left && direction == PlayerDirection.Right)
        {
            return;
        }

        direction = dir;

        ForceMove();

    }

    private void ForceMove()
    {
        counter = 0;
        move = false;
        Move();

    }
    private void OnTriggerEnter(Collider target)
    {

        if(target.tag == Tags.Fruit)
        {
            target.gameObject.SetActive(false);

            create_Node_At_Tail = true;

            GameplayController.instance.IncreaseScore();
        }

        if(target.tag == Tags.Wall || target.tag == Tags.Bomb || target.tag == Tags.Tail)
        {
            Time.timeScale = 0f;
            Alive = false;

        }

    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
