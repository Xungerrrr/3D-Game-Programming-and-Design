---
title: Unity 3D - 牧师与魔鬼
subtitle: 3D 游戏编程与设计 第四周游戏
date: 2018-04-01 11:55:20
tags:
- Unity
categories:
- Unity
header_image: /intro/unity.png
abstract: 3D 游戏编程与设计 第四周游戏
---
## 游戏脚本
> Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many ways. Keep all priests alive! Good luck!

## 程序需要满足的要求
* [play the game]( http://www.flash-game.net/game/2535/priests-and-devils.html)
* 列出游戏中提及的事物（Objects）
* 用表格列出玩家动作表（规则表），注意，动作越少越好
* 请将游戏中对象做成预制
* 在 GenGameObjects 中创建 长方形、正方形、球 及其色彩代表游戏中的对象。
* 使用 C# 集合类型 有效组织对象
* 整个游戏仅 主摄像机 和 一个 Empty 对象， **其他对象必须代码动态生成！！！** 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 **违背本条准则，不给分**
* 请使用课件架构图编程，**不接受非 MVC 结构程序**
* 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

## 游戏中提及的事物（Object）
> 牧师、魔鬼、船、go 按钮、开始岸、结束岸、水

## 玩家动作表
| 动作         | 规则                             |
|:-------------|:--------------------------------|
| 开船         | 船停靠在岸边且船上至少有一人       |
| 上船         | 船停靠在岸边，船上有空位并且岸上有人|
| 下船         | 船停靠在岸边且船上有人             |

## 实现思路
### MVC架构
- Model：游戏中的 GameObject。
- View: UserGUI，ClickGUI。
- Controller: SSDirector（最高级别的 Controller），FirstController，以及其他基础的 Controller (BoatController, MyCharacterController, ShoreController)。

### 游戏接口定义
游戏中定义了两个接口类型，分别负责游戏场景控制和用户交互。这两个接口不能直接实例化，而是要通过继承这两个接口来实现相应的功能。
```csharp
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
```
### View 定义
#### ClickGUI
ClickGUI 类用来监测用户的点击行为，并利用 SceneController 中的方法进行处理。
```csharp
public class ClickGUI : MonoBehaviour {
    IUserAction action;
    MyCharacterController characterController;

    public void setController(MyCharacterController characterCtrl) {
        characterController = characterCtrl;
    }

    void Start() {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }

    void OnMouseDown() {
        if (SSDirector.getInstance().state == State.START) {
            if (gameObject.name == "boat") {
            	action.moveBoat ();
            } else {
                action.characterIsClicked (characterController);
            }
        }
    }
}
```
#### UserGUI
UserGUI 类用来显示游戏中的开始/暂停按钮、游戏状态和倒计时等元素，也是用户控制游戏开始和暂停的入口。

UserGUI 类定义参见 [GitHub](https://github.com/Xungerrrr/3D-Game-Programming-and-Design/tree/master/Homework%202/Priests%20and%20Devils)。

### Controller 定义
#### SSDirector 类定义
SSDirector 类是最高层的控制器，在游戏过程中只有一个实例，控制游戏的开始、暂停以及资源的加载。其中 state 是一个自定义的枚举型变量，记录游戏的四种状态，以实现游戏的暂停功能。CountDown 方法实现了游戏的倒计时功能。
```csharp
public enum State { WIN, LOSE, PAUSE, START };

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
```

#### 其他 Controller 类定义
其他 Controller 类定义参见 [GitHub](https://github.com/Xungerrrr/3D-Game-Programming-and-Design/tree/master/Homework%202/Priests%20and%20Devils)。

## 游戏效果预览
{% asset_img "Priests and Devils.gif" "Priests and Devils" %}

## 参考博客
[1] [学习 Unity(5) 小游戏实例——牧师与魔鬼](https://www.jianshu.com/p/07028b3da573)

[2] [Unity3D 学习——牧师与恶魔过河游戏 (组合模式、单实例模式)](https://blog.csdn.net/H12590400327/article/details/70037805)
