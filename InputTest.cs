using Godot;
using System;

public partial class InputTest : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//鼠标设置：显示，隐藏，限制在游戏窗口内
		//Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//按键按下(这种按键的检测是每一帧都在进行的，对有些只需要监测一次按键的逻辑还需要额外处理)
		if (Input.IsKeyPressed(Key.B))
		{
			GD.Print("按下B键");
		}

		//! 配合虚拟按键，是实现按键事件最好用的方法(代码更简洁，支持多种输入方式，只需要在项目设置-》输入映射中进行配置即可)
		//todo 按下跳跃键
		if (Input.IsActionJustPressed("Jump"))
		{
			GD.Print("Pressed Jump");
		}
		//todo 跳跃中
		if (Input.IsActionPressed("Jump"))
		{
			GD.Print("Jumping");
		}
		//todo 结束跳跃
		if (Input.IsActionJustReleased("Jump"))
		{
			GD.Print("Released Jump");
		}
		//todo 获取按键的力度（一个0-1的浮点数）
		float s = Input.GetActionStrength("Jump");
		//GD.Print(s);
		//todo 获取一个水平轴（用于实现左右移动的方向,需要接受两个虚拟按键）
		float horizontal = Input.GetAxis("Left", "Right");
		//GD.Print(horizontal);
		//todo 获取上下左右十字方向轴（2D游戏常用）
		Vector2 dir = Input.GetVector("Left", "Right", "Up", "Down");
		GD.Print(dir);
	}

	// 生命周期方法中提供的按键监测方法
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		// 如果是键盘按键
		if (@event is InputEventKey)
		{
			//转成键盘按键事件进行处理
			var key = @event as InputEventKey;
			//判断当前是否按下V键
			if (key.Keycode == Key.V)
			{

				if (key.IsEcho())   //判断按键是否是持续按压
				{
					GD.Print("持续按压V");
				}
				else if (key.IsPressed())   //判断是否是按下的瞬间
				{
					GD.Print("按下V的瞬间");
				}
				else if (key.IsReleased())  //判断是否是抬起的瞬间
				{
					GD.Print("释放V的瞬间");
				}

			}
		}

		// 如果是鼠标按键
		if (@event is InputEventMouse)
		{
			//转成鼠标按键事件进行处理
			var mouse = @event as InputEventMouse;
			if (mouse.IsPressed())
			{
				//打印鼠标位置
				GD.Print(mouse.Position);
				//打印鼠标按键
				GD.Print(mouse.ButtonMask);
			}
		}
	}
}
