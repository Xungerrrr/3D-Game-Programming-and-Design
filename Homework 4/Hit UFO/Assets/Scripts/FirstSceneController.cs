using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, SceneController, UserAction {

    public FlyActionManager actionManager;
    public DiskFactory diskFactory;
    public UserGUI userGUI;
    public ScoreRecorder scoreRecorder;

    private Queue<GameObject> diskQueue = new Queue<GameObject>(); // 游戏中的飞碟
    private List<GameObject> diskNotshot = new List<GameObject>(); // 未被击中的飞碟
    private int diskNumber;                                        // 每个回合的飞碟数量
    private int currentRound;                                       //当前回合
    private int roundNumber;                                       // 回合总数
    private float frequency;                                       // 飞碟出现的频率
    private GameState gameState = GameState.PAUSE;                 // 游戏状态

    // 场景初始化
    void Start() {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        diskNumber = 10;
        currentRound = 1;
        roundNumber = 3;
        frequency = 2f;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        diskFactory = Singleton<DiskFactory>.Instance;
        actionManager = gameObject.AddComponent<FlyActionManager>();
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        director.setFPS(60);
    }

    // 将飞碟加载到飞碟队列中
    public void LoadResources() {
        diskQueue.Enqueue(diskFactory.GetDisk(currentRound));
    }

    private void Update() {
        // 判断回合结束
        if (diskNumber == 0 && currentRound < roundNumber) {
            CancelInvoke("LoadResources");
        }
        // 判断游戏结束
        if (diskNumber == 0 && currentRound == roundNumber) {
            gameState = GameState.FINISH;
        }
        else if (diskNumber == 0 && diskNotshot.Count == 0) {
            diskNumber = 10;
            frequency -= 0.4f;
            gameState = GameState.ROUND_FINISH;
        }
        // 游戏开始时加载飞碟资源
        if (gameState == GameState.START) {
            InvokeRepeating("LoadResources", 1f, frequency);
            gameState = GameState.ROUND_START;
        }
        // 游戏暂停或结束时取消加载飞碟资源
        if (gameState == GameState.FINISH || gameState == GameState.ROUND_FINISH || gameState == GameState.PAUSE) {
            CancelInvoke("LoadResources");
        }
        // 回合开始，抛出飞碟
        if (gameState == GameState.ROUND_START)
            ThrowDisk();
    }

    // 抛出飞碟
    public void ThrowDisk() {
        float position_x = 16;
        if (diskQueue.Count != 0) {
            diskNumber--;
            GameObject disk = diskQueue.Dequeue(); // 取出飞碟
            diskNotshot.Add(disk);
            disk.SetActive(true);

            // 设置飞碟的随机位置
            float ran_y = Random.Range(1f, 4f);
            float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
            disk.GetComponent<DiskData>().direction = new Vector3(ran_x, ran_y, 0);
            Vector3 position = new Vector3(-disk.GetComponent<DiskData>().direction.x * position_x, ran_y, 0);
            disk.transform.position = position;

            // 设置飞碟初始所受的力和角度
            float power = Random.Range(10f, 15f);
            float angle = Random.Range(15f, 28f);
            actionManager.Fly(disk, angle, power);
        }

        for (int i = 0; i < diskNotshot.Count; i++) {
            GameObject temp = diskNotshot[i];
            //飞碟飞出摄像机视野也没被打中
            if (temp.transform.position.y < -20 && temp.gameObject.activeSelf == true) {
                diskFactory.FreeDisk(diskNotshot[i]);
                diskNotshot.Remove(diskNotshot[i]);
            }
        }
    }

    // 击中飞碟
    public void Hit(Vector3 position) {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<DiskData>() != null) {
                scoreRecorder.Record(hit.collider.gameObject); // 按照飞碟种类计分
                hit.collider.gameObject.transform.position = new Vector3(0, -20, 0); // 将飞碟移到底部
            }
        }
    }

    // 获取分数
    public int GetScore() {
        return scoreRecorder.score;
    }

    // 重新开始
    public void Restart() {
        gameState = GameState.START;
        scoreRecorder.Reset();
        currentRound = 1;
        diskNumber = 10;
        frequency = 2f;
    }

    // 暂停游戏
    public void Pause() {
        gameState = GameState.PAUSE;
    }

    // 开始游戏
    public void Begin() {
        gameState = GameState.START;
    }
    
    // 开始下一回合
    public void NextRound() {
        gameState = GameState.START;
        currentRound++;
    }
    
    // 获取游戏状态
    public GameState getGameState() {
        return gameState;
    }

    // 设置游戏状态
    public void setGameState(GameState gs) {
        gameState = gs;
    }

    // 获取回合
    public int GetRound() {
        return this.currentRound;
    }
    
    // 暂停几秒后回收飞碟
    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject obj) {
        yield return new WaitForSeconds(wait_time);
        // 等待之后执行的动作  
        hit.collider.gameObject.transform.position = new Vector3(0, -20, 0);
        disk_factory.FreeDisk(obj);
    }
}
