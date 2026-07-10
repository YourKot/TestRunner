using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public int Health = 5;
	[Export] public HBoxContainer HeartsContainer;

	public void GetInjured(int damage)
	{
		Health -= damage;
		if(Health <= 0) Dead();
		else UpdateHeartsUI();
	}

	public void Dead()
	{
		GD.Print("мертв X(");
	}

    public override void _Ready()
    {
        UpdateHeartsUI();
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void UpdateHeartsUI()
	{
		if (HeartsContainer == null)
		{
			GD.Print("HeartsContainer не назначен в Инспекторе!");
			return;
		}

		// Получаем список всех дочерних узлов (наших сердечек)
		var hearts = HeartsContainer.GetChildren();

		for (int i = 0; i < hearts.Count; i++)
		{
			if (hearts[i] is Control heartNode)
			{
				// Если индекс меньше текущего здоровья — показываем сердечко, иначе — скрываем
				heartNode.Visible = i < Health;
			}
		}
	}
}
