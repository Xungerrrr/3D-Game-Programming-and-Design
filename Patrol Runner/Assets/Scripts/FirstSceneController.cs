using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, SceneController, UserAction
{
    public PatrolActionManager patrolActionManager;                // 巡逻兵动作管理者
    public PatrolFactory patrolFactory;                            // 生产巡逻兵的工厂
    public UserGUI userGUI;
    public ScoreRecorder scoreRecorder;
    public GameObject player;                                      // 游戏玩家
    public int playerRegion;                                       // 玩家所在区域

    private GameState gameState = GameState.START;                 // 游戏状态
    private List<GameObject> patrols;                              // 场景中巡逻兵列表

    // 场景初始化
    void Start() {
        Director director = Director.GetInstance();
        director.CurrentSceneController = this;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        patrolFactory = Singleton<PatrolFactory>.Instance;
        playerRegion = 5;
        patrolActionManager = gameObject.AddComponent<PatrolActionManager>();
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        director.SetFPS(30);
        director.leaveSeconds = 60;
        LoadResources();
        // 巡逻兵开始巡逻
        for (int i = 0; i < patrols.Count; i++) {
            patrolActionManager.Patrol(patrols[i]);
        }
    }

    // 加载游戏资源
    public void LoadResources() {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-1.5f, 0, -1.5f), Quaternion.identity) as GameObject;
        patrols = patrolFactory.GetPatrols();
        Camera.main.GetComponent<CameraFollowAction>().player = player;
    }

    private void Update() {
        for (int i = 0; i < patrols.Count; i++) {
            // 更新玩家区域信息
            patrols[i].GetComponent<PatrolData>().playerRegion = playerRegion;
        }
    }

    void OnEnable() {
        // 订阅游戏事件
        GameEventManager.OnGoalLost += OnGoalLost;
        GameEventManager.OnFollowing += OnFollowing;
        GameEventManager.GameOver += GameOver;
        GameEventManager.Win += Win;
    }

    void OnDisable() {
        GameEventManager.OnGoalLost -= OnGoalLost;
        GameEventManager.OnFollowing -= OnFollowing;
        GameEventManager.GameOver -= GameOver;
        GameEventManager.Win -= Win;
    }

    public void MovePlayer(float translationX, float translationZ) {
        if (translationX != 0 || translationZ != 0) {
            player.GetComponent<Animator>().SetBool("run", true);
        } else {
            player.GetComponent<Animator>().SetBool("run", false);
        }
        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;
        
        player.transform.LookAt(new Vector3(player.transform.position.x + translationX, player.transform.position.y, player.transform.position.z + translationZ));
        if (translationX == 0)
            player.transform.Translate(0, 0, Mathf.Abs(translationZ) * 2);
        else if (translationZ == 0)
            player.transform.Translate(0, 0, Mathf.Abs(translationX) * 2);
        else
            player.transform.Translate(0, 0, Mathf.Abs(translationZ) + Mathf.Abs(translationX));

    }

    // 失去目标，巡逻兵放弃追击
    public void OnGoalLost(GameObject patrol) {
        patrolActionManager.Patrol(patrol);
        scoreRecorder.Record();
    }

    // 玩家进入范围，巡逻兵开始追击
    public void OnFollowing(GameObject patrol) {
        patrolActionManager.Follow(player, patrol);
    }

    // 失败
    public void GameOver() {
        gameState = GameState.LOSE;
        StopAllCoroutines();
        patrolFactory.PausePatrol();
        player.GetComponent<Animator>().SetTrigger("death");
        patrolActionManager.DestroyAllActions();
    }

    // 胜利
    public void Win() {
        gameState = GameState.WIN;
        StopAllCoroutines();
        patrolFactory.PausePatrol();
    }

    // 获取分数
    public int GetScore() {
        return scoreRecorder.score;
    }

    // 重新开始
    public void Restart() {
        SceneManager.LoadScene("Scenes/Patrol Runner");
    }

    // 暂停游戏
    public void Pause() {
        gameState = GameState.PAUSE;
        patrolFactory.PausePatrol();
        player.GetComponent<Animator>().SetBool("pause", true);
        StopAllCoroutines();
    }

    // 开始游戏
    public void Begin() {
        gameState = GameState.RUNNING;
        patrolFactory.StartPatrol();
        player.GetComponent<Animator>().SetBool("pause", false);
        StartCoroutine(Director.GetInstance().CountDown());
    }

    // 获取游戏状态
    public GameState getGameState() {
        return gameState;
    }

    // 设置游戏状态
    public void setGameState(GameState gs) {
        gameState = gs;
    }
}
