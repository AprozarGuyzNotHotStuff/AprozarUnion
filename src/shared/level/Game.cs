using Godot;
using System;

namespace AprozarUnion.Shared.Level;

public partial class Game : Node2D
{
    [Export] private float timeScale = 2.0f / 3.0f; // In-game minutes passing per 1 real-world second
    [Export] private int startHour = 6;             // Store opening time (6:00 AM)
    [Export] private int endHour = 18;              // Store closing time (6:00 PM)

    private double _accumulatedSeconds = 0;
    private int _currentDay = 1;
    private int _currentHour;
    private int _currentMinute;
    private bool _isDayActive = true;

    public override void _Ready()
    {
        ResetDay();
    }

    public override void _Process(double delta)
    {
        if (!_isDayActive) return;
        
        _accumulatedSeconds += delta * timeScale;

        if (_accumulatedSeconds >= 1.0)
        {
            int minutesToAdd = (int)_accumulatedSeconds;
            _accumulatedSeconds -= minutesToAdd;
            
            AdvanceTime(minutesToAdd);
        }
    }

    private void AdvanceTime(int minutes)
    {
        _currentMinute += minutes;
        
        while (_currentMinute >= 60)
        {
            _currentMinute -= 60;
            _currentHour++;
            
            if (_currentHour >= endHour)
            {
                CloseDay();
                return;
            }
        }

        GD.Print($"Day {_currentDay} - Time: {_currentHour:D2}:{_currentMinute:D2}");
    }

    private void ResetDay()
    {
        _currentHour = startHour;
        _currentMinute = 0;
        _accumulatedSeconds = 0;
        _isDayActive = true;
        GD.Print($"Day {_currentDay} Started ({_currentHour:D2}:00)");
    }

    private void CloseDay()
    {
        _isDayActive = false;
        GD.Print($"Day {_currentDay} Ended");
        
        _currentDay++;
        if (_currentDay > 55)
        {
            GD.Print("All 55 days completed!");
            return;
        }

        // Here trigger the End-of-Day summary screen, family hunger/rent checks, etc.
        // Then call ResetDay() when the player clicks "Start Next Day".
        ResetDay(); 
    }
}