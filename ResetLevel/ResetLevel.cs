using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public partial class ResetLevel : Node
{
	private DateTime? _PressTime;
	private ColorRect _Background;
	private Label _ResetLabel;
	private Tween _TransparencyTween;
	private Tween _LabelTween;


	public override void _Ready()
	{
		base._Ready();
		_Background = GetNode<ColorRect>("Background");
		_ResetLabel = GetNode<Label>("ResetLabel");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ui_reset"))
		{
			Debug.WriteLine("RESET CANCEL");
			_PressTime = null;
			_TransparencyTween.Kill();
			_LabelTween.Kill();
			_Background.Modulate = Color.FromHsv(0, 0, 0, 0);
			_ResetLabel.Modulate = Color.FromHsv(0, 0, 100, 0);
		}

		if (Input.IsActionJustPressed("ui_reset"))
		{
			Debug.WriteLine("RESET START");
			_PressTime = DateTime.Now;
			_TransparencyTween = CreateTween();
			_LabelTween = CreateTween();
			_TransparencyTween.TweenProperty(_Background, "modulate:a", 1, 1);
			_LabelTween.TweenProperty(_ResetLabel, "modulate:a", 1, 1);
		}

		if (_PressTime != null && (DateTime.Now - _PressTime).Value.TotalSeconds > 1.5f)
		{
			Debug.WriteLine("RESET DONE");
			Debug.WriteLine(GetTree().CurrentScene.Name);
			Debug.WriteLine(GetTree().ReloadCurrentScene());
		}

	}
}
