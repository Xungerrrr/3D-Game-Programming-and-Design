using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { WIN, LOSE, PAUSE, START };
public enum SSActionEventType:int { Started, Completed }
public class SSDirector : System.Object {
	public State state = State.PAUSE;
	public int totalSeconds = 100;
	public int leaveSeconds;
	public string countDownTitle = "Start";
	private static SSDirector _instance;
	public SceneController currentSceneController { get; set; }

	public static SSDirector getInstance() {
		if (_instance == null) {
			_instance = new SSDirector ();
		}
		return _instance;
	}
	public int getFPS() {
        return Application.targetFrameRate;
    }
    public void setFPS(int fps) {
        Application.targetFrameRate = fps;
    }
    public IEnumerator CountDown() {
        while (leaveSeconds > 0) {
            yield return new WaitForSeconds(1f);
            leaveSeconds--;
        }
    }
}

public interface SceneController {
	void loadResources ();
	void pause ();
	void resume ();
}

public interface IUserAction {
	void moveBoat();
	void characterIsClicked(MyCharacterController characterCtrl);
	void restart();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, 
        int intParam = 0, string strParam = null, object objectParam = null);
}
/*public class Moveable: MonoBehaviour {
	
	readonly float move_speed = 20;

	// change frequently
	int moving_status;	// 0->not moving, 1->moving to middle, 2->moving to dest
	Vector3 dest;
	Vector3 middle;

	void Update() {
		if (moving_status == 1) {
			transform.position = Vector3.MoveTowards (transform.position, middle, move_speed * Time.deltaTime);
			if (transform.position == middle) {
				moving_status = 2;
			}
		} else if (moving_status == 2) {
			transform.position = Vector3.MoveTowards (transform.position, dest, move_speed * Time.deltaTime);
			if (transform.position == dest) {
				moving_status = 0;
			}
		}
	}
	public void setDestination(Vector3 _dest) {
		dest = _dest;
		middle = _dest;
		if (_dest.y == transform.position.y) {	// boat moving
			moving_status = 2;
		}
		else if (_dest.y < transform.position.y) {	// character from shore to boat
			middle.y = transform.position.y;
		} else {								// character from boat to shore
			middle.x = transform.position.x;
		}
		moving_status = 1;
	}

	public void reset() {
		moving_status = 0;
	}
}*/