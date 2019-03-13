---
title: Unity 3D - 太阳系
subtitle: 3D 游戏编程与设计 第四周简答
date: 2018-03-31 17:41:38
tags:
- Unity
categories:
- Unity
header_image: /intro/unity.png
abstract: 3D 游戏编程与设计 第四周简答
---

## 游戏对象运动的本质是什么?
> 游戏对象的运动，是由游戏对象空间位置的连续变化所形成的。其中，游戏对象的空间位置由游戏中的坐标系统所决定，包含位置、旋转角度和缩放比例等属性。

## 请用三种方法以上方法，实现物体的抛物线运动。
> 抛体运动的特点
  - 水平方向：匀速直线运动
  - 竖直方向：自由落体运动（匀变速直线运动）

### 方法1：修改 transform 属性
```csharp
public float HorizontalSpeed = 1.0f; // 水平初速度
public float VerticalSpeed = 1.0f; // 垂直初速度
public float Acceleration = 1.0f; // 抛体运动加速度

void Update () {
    // 更新垂直速度
    VerticalSpeed -= Acceleration * Time.deltaTime;
    // 更新水平位置
    this.transform.position += Vector3.left * HorizontalSpeed * Time.deltaTime;
    // 更新垂直位置
    this.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;
}
```

### 方法2：使用 transform.Translate 方法
```csharp
public float HorizontalSpeed = 1.0f; // 水平初速度
public float VerticalSpeed = 1.0f; // 垂直初速度
public float Acceleration = 1.0f; // 抛体运动加速度

void Update () {
    // 更新垂直速度
    VerticalSpeed -= Acceleration * Time.deltaTime;
    // 更新水平位置
    this.transform.Translate (Vector3.left * HorizontalSpeed * Time.deltaTime);
    // 更新垂直位置
    this.transform.Translate (Vector3.up * VerticalSpeed * Time.deltaTime);
}
```

### 方法3：使用 Vector3.MoveTowards 方法
```csharp
public float HorizontalSpeed = 1.0f; // 水平初速度
public float VerticalSpeed = 1.0f; // 垂直初速度
public float Acceleration = 1.0f; // 抛体运动加速度

void Update () {
    // 更新垂直速度
    VerticalSpeed -= Acceleration * Time.deltaTime;
    // 计算下一位置
    Vector3 NextPosition = this.transform.position + Vector3.left * HorizontalSpeed * Time.deltaTime;
    NextPosition += Vector3.up * VerticalSpeed * Time.deltaTime;
    // 移动到下一位置
    this.transform.position = Vector3.MoveTowards (this.transform.position, NextPosition, 100);
}
```

### 方法4：添加 Rigidbody 组件
添加 Rigidbody 组件后，勾选 Use Gravity 属性，游戏对象便能受到重力作用。此时额外增加一个水平速度，即可实现游戏对象的平抛运动。
{% asset_img "Rigidbody.png" "Rigidbody" %}
```csharp
public float speed = 1.0f;

void Update () {
    // 水平方向匀速运动
    this.transform.position += Vector3.left * speed * Time.deltaTime;
}
```
## 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。

### 游戏对象的层次结构
{% asset_img Hierarchy.png Hierarchy %}
八大行星围绕太阳公转，因此应该设计为太阳的子对象。而月球的轨迹只与地球公转相关，与地球自转无关，因此不能将月球直接设为地球的子对象。解决办法是，新建一个空对象 EarthShadow，使它的位置与地球保持一致，再将月球设为 EarthShadow 的子对象，通过控制 EarthShadow 的自转速度来控制月球绕地球的公转速度。

### 游戏对象初始位置的确定
创建 RoundSun 脚本，新建不同游戏对象的 Transform 属性，并根据八大行星位置的相对关系，在 Start 函数中给 Transform 属性赋值。
```csharp
public class RoundSun : MonoBehaviour {

    // 创建太阳和各行星的位置属性
    public Transform Mercury;
    public Transform Venus;
    public Transform Earth;
    public Transform Mars;
    public Transform Jupiter;
    public Transform Saturn;
    public Transform Uranus;
    public Transform Neptune;
    public Transform EarthShadow;

    // Use this for initialization
    void Start () {
        // 初始化太阳和各行星的位置
        Sun.position = Vector3.zero;
        Mercury.position = new Vector3 (7, 0, 0);
        Earth.position = new Vector3 (12, 0, 0);
        Mars.position = new Vector3 (15, 0, 0);
        Jupiter.position = new Vector3 (20, 0, 0);
        Saturn.position = new Vector3 (28, 0, 0);
        Uranus.position = new Vector3 (35, 0, 0);
        Neptune.position = new Vector3 (40, 0, 0);
        EarthShadow.position = new Vector3 (12, 0, 0);
    }
}
```
### 公转和自转
接下来，在 Update 函数中用 RotateAround 方法实现行星的公转，用 Rotate 实现行星的自转。根据太阳系相关数据设定参数。
```csharp
public class RoundSun : MonoBehaviour {

  // Update is called once per frame
    void Update () {
        // 公转
        Mercury.RotateAround (Sun.position, new Vector3(0, 10, 1), 47 * Time.deltaTime)；
        Venus.RotateAround (Sun.position, new Vector3(0, 15, -1), 35 * Time.deltaTime);
        Earth.RotateAround (Sun.position, Vector3.up, 30 * Time.deltaTime);
        Mars.RotateAround (Sun.position, new Vector3(0, 18, 2), 24 * Time.deltaTime);
        Jupiter.RotateAround (Sun.position, new Vector3(0, 16, 1), 13 * Time.deltaTime);
        Saturn.RotateAround (Sun.position, new Vector3(0, 20, -1), 9 * Time.deltaTime);
        Uranus.RotateAround (Sun.position, new Vector3(0, 25, -1), 6 * Time.deltaTime);
        Neptune.RotateAround (Sun.position, new Vector3(0, 30, 1), 5 * Time.deltaTime);
        EarthShadow.RotateAround (Sun.position, Vector3.up, 30 * Time.deltaTime);
        // 自转
        Mercury.Rotate (Vector3.down * 6 * Time.deltaTime);
        Venus.Rotate (Vector3.down * 1 * Time.deltaTime);
        Earth.Rotate (Vector3.down * 300 * Time.deltaTime);
        Mars.Rotate (Vector3.up * 300 * Time.deltaTime);
        Jupiter.Rotate (Vector3.up * 600 * Time.deltaTime);
        Saturn.Rotate (Vector3.up * 400 * Time.deltaTime);
        Uranus.Rotate (Vector3.up * 500 * Time.deltaTime);
        Neptune.Rotate (Vector3.up * 500 * Time.deltaTime);
        EarthShadow.Rotate (Vector3.down * 100 * Time.deltaTime);
    }
}
```
### 效果
最后，将脚本挂在 MainCamera 上，将游戏对象拖入到相应的 Transform 属性中，并将图片素材拖放到行星上。运行效果如下：
{% asset_img Solar-System1.png Solar System1 %}
{% asset_img Solar-System2.png Solar System2 %}
{% asset_img "Solar System.gif" Solar System %}
