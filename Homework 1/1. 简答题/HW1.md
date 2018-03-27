---
layout: default
---

# Homework 1

## 1. 解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。

- 区别
  - 游戏对象（GameObjects）

    > 游戏中的物体就是游戏对象，例如游戏中的角色、道具和环境物体。游戏对象都是由空对象构建而成的。通过向空对象中加入不同的属性（组件），我们可以获得具有不同外观和行为的游戏对象。

  - 资源（Assets）

    > 游戏开发过程中需要的资源包括材质、纹理、模型、动画、预设、音频、场景等，是能应用在游戏中的素材。
- 联系

  > 资源能够被导入到游戏中，被游戏对象使用，影响游戏对象的属性。资源还可以被用来创建新的游戏对象。

## 2. 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）

Unity 官方教程的资源目录组织和游戏对象树如下：

![tree](assets\image\file-tree.png)

游戏资源被放到不同的层级中，层次和内容十分清晰。游戏对象间也有明确的层级关系，以便实现游戏中的组合模式。

## 3. 编写一个代码，使用 debug 语句来验证 [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) 基本行为或事件触发的条件

- 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
- 常用事件包括 OnGUI() OnDisable() OnEnable()

代码如下：
```csharp
// C# Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

  // 判断是否执行过相应函数
  private bool isUpdate = false;
  private bool isFixedUpdate = false;
  private bool isLateUpdate = false;
  private bool isOnGUI = false;

  // 当一个脚本实例被载入时 Awake 被调用
  void Awake () {
    Debug.Log ("Awake");
  }

  // Start 仅在 Update 函数第一次被调用前调用
  void Start () {
    Debug.Log ("Start");
  }

  // 当 MonoBehaviour 启用时，其 Update 在每一帧被调用
  void Update () {
    // 确保函数只被执行一次
    if (!isUpdate) {
      Debug.Log ("Update");
      isUpdate = true;
    }
  }

  // 当 MonoBehaviour 启用时，其 FixedUpdate 在每一帧被调用
  void FixedUpdate () {
    // 确保函数只被执行一次
    if (!isFixedUpdate) {
      Debug.Log ("Fixed Update");
      isFixedUpdate = true;
    }
  }

  // 当 Behaviour 启用时，其 LateUpdate 在每一帧被调用
  void LateUpdate () {
    // 确保函数只被执行一次
    if (!isLateUpdate) {
      Debug.Log ("Late Update");
      isLateUpdate = true;
    }
  }

  // 当对象变为不可用或非激活状态时此函数被调用
  void OnDisable () {
    Debug.Log ("On Disable");
  }

  // 当对象变为可用或激活状态时此函数被调用
  void OnEnable () {
    Debug.Log ("On Enable");
  }

  // 渲染和处理 GUI 事件时调用
  void OnGUI () {
    // 确保函数只被执行一次
    if (!isOnGUI) {
      Debug.Log ("On GUI");
      isOnGUI = true;
    }
  }

}
```

运行结果如下图：

![Console](assets\image\console.png)

可以看出，基本行为和事件的发生顺序为：

1.  Awake()
2.  OnEnable()
3.  Start()
4.  FixedUpdate()
5.  Update()
6.  LateUpdate()
7.  OnGUI()
8.  OnDisable()

顺序满足各事件触发的条件。

FixedUpdate()、LateUpdate()和Update()的区别：

*  FixedUpdate()

  > 处理 Rigidbody 时，需要用 FixedUpdate 代替 Update。例如: 给刚体加一个作用力时，你必须应用作用力在 FixedUpdate 里的固定帧，而不是 Update 中的帧(两者帧长不同)。

*  LateUpdate()

  > LateUpdate 是在所有 Update 函数调用后被调用。这可用于调整脚本执行顺序。例如: 当物体在 Update 里移动时，跟随物体的相机可以在 LateUpdate 里实现。

## 4. 查找脚本手册，了解 [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html)，Transform，Component 对象

- 分别翻译官方对三个对象的描述（Description）
  - GameObject

    > GameObjects are the fundamental objects in Unity that represent characters, props and scenery.

    > 游戏对象是Unity中基础的对象，它代表了角色、道具和场景。

  - Transform

    > The Transform component determines the Position, Rotation, and Scale of each object in the scene.

    > 变换组件决定了场景中每个对象的位置、旋转角度和缩放比例。

  - Component

    > Components are the nuts & bolts of objects and behaviors in a game. 

    > 组件是游戏中对象和行为的枢纽。

- 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件

  ![pic1](assets\image\ch02-homework.png)

  - table 对象（实体）的属性
    - Name: 对象的名字
    - Tag: 用于通过 Tag 名称来快速查找对象
    - Layer: 可用于仅对某些特定的对象组投射光线、渲染或应用光照
    - Static: 准备静态几何结构以用于自动批处理；计算遮挡剔除 (Occlusion Culling)
  - table 的 Transform 的属性
    - Position: X、Y 和 Z 坐标中变换的位置。(0, 0, 0)
    - Rotation: 围绕 X、Y 和 Z 轴的旋转，以度计。(0, 0, 0)
    - Scale: 沿 X、Y 和 Z 轴的缩放，“1” 是原始大小。(1, 1, 1)
  - table 的部件
    - Cube (Mesh Filter): 网格过滤器从资源中拿出网格并将其传递给网格渲染器 (Mesh Renderer) 用于屏幕渲染
    - Box Collider: 箱体碰撞体 (Box Collider) 是基本立方体形碰撞基元
    - Mesh Renderer: 网格渲染器 (Mesh Renderer) 从网格过滤器 （Mesh Filter) 获得几何结构，并根据物体的变换组件 (Transform) 定义的位置进行渲染
    - Material: 材质用于将纹理置于游戏对象 (GameObject) 上

- 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）

![pic2](assets\image\relation.png)

## 5. 整理相关学习资料，编写简单代码验证以下技术的实现：

- 查找对象
```csharp
//通过名字查找：
public static GameObject Find(string name);
//通过标签查找单个对象：
public static GameObject FindWithTag(string tag);
//通过标签查找多个对象：
public static GameObject[] FindGameObjectsWithTag(string tag);
```
- 添加子对象
```csharp
public static GameObject CreatePrimitive(PrimitiveTypetype);
```
- 遍历对象树
```csharp
foreach (Transform child in transform) {
  Debug.Log(child.gameObject.name);
}
```
- 清除所有子对象
```csharp
foreach (Transform child in transform) {  
  Destroy(child.gameObject);
} 
```

## 6. 资源预设（Prefabs）与 对象克隆 (clone)

- 预设（Prefabs）有什么好处？

  > 预设就是预制好的游戏对象，包含了完整的组件和属性，可看作是游戏对象的模板。利用预制能够批量实例化出具有相同属性的游戏对象。预设与实例之间是联系的，改变预设的属性能够更改所有与之关联的对象的属性。

- 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？

  > 不同于预设，对象克隆只是对对象的复制，新对象与克隆本体之间没有关联，不会相互影响。

- 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象

```csharp
// C# Script
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Instantiate : MonoBehaviour {
  
  public GameObject table;
  private int columnNum = 10; // 列数
  private int rowNum = 5; // 行数

  void Start() {
    for (int i = 0; i < rowNum; i++) {
      for (int j = 0; j < columnNum; j++) {
        // 创建 table 的实例  
        Instantiate(table, new Vector3(j, i, 0), Quaternion.identity);
      }
    }
  }

}
```

效果图如下：
![prefabs](assets\image\prefabs.png)

## 7. 尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法向子对象发送消息

> 组合模式将对象组合成树形结构，以表示 “部分整体” 的层次结构。这种模式使得用户能够以一致的方式处理单个对象及对象的组合。

父类对象方法：

```csharp
  void Start () {
    this.BroadcastMessage ("CallChildren");
  }
```

子类对象方法：
```csharp
  void CallChildren () {
    Debug.Log ("I'm here!");
  }
```

[back](./Unity-3D-Homework)
