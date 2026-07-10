using Godot;
using System;

public partial class Meteor : RigidBody2D
{
	public override void _Ready()
    {
        this.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {

        if (body is TileMapLayer tileMap)
        {
            GD.Print("Столкнулся с тайлмапом!");
        }

        if (body is Player player) 
        {
            GD.Print("Столкнулся с игроком!");
            player.GetInjured(1);
        }
    }
}
