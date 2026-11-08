using Godot;
using System;
using System.Linq;
using AprozarUnion.Shared.Customer;
using AprozarUnion.Shared.Data.Item;
using AprozarUnion.Shared.Level.Store;

namespace AprozarUnion.Shared.Level;

public partial class Game : Node2D
{
    [Export] private float timeScale = 2.0f / 3.0f; // In-game minutes passing per 1 real-world second, use "2.0f / 3.0f" for final
    [Export] private int startHour = 6;             // Store opening time (6:00 AM)
    [Export] private int endHour = 18;              // Store closing time (6:00 PM)
    
    [Signal] public delegate void MinuteChangedEventHandler(int day, int hour, int minute);
    [Signal] public delegate void DayStartedEventHandler(int day);
    [Signal] public delegate void DayEndedEventHandler(int day);

    private double _accumulatedSeconds = 0;
    private int _currentDay = 1;
    private int _currentHour;
    private int _currentMinute;
    private bool _isDayActive = true;
    
    private TickManager _tickManager = new();

    public override void _Ready()
    {
        ItemDatabase.Load();
        StartDay();
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
        
        this._tickManager.updateTick(delta);
        if (this._tickManager.DoTickThisFrame)
        {
            this.Tick();
        }
    }

    private void AdvanceTime(int minutes)
    {
        _currentMinute += minutes;
        while (_currentMinute >= 60)
        {
            _currentMinute -= 60;
            _currentHour++;
            if (_currentHour >= endHour) { CloseDay(); return; }
        }
        EmitSignal(SignalName.MinuteChanged, _currentDay, _currentHour, _currentMinute);
        // GD.Print($"Day {_currentDay} - Time: {_currentHour:D2}:{_currentMinute:D2}");
    }

    private void StartDay()
    {
        _currentHour = startHour;
        _currentMinute = 0;
        _accumulatedSeconds = 0;
        _isDayActive = true;
        EmitSignal(SignalName.DayStarted, _currentDay);
        GD.Print($"Day {_currentDay}: {ItemDatabase.AvailableOn(_currentDay).Count()} items in stock");
    }

    private void CloseDay()
    {
        _isDayActive = false;
        EmitSignal(SignalName.DayEnded, _currentDay);
        GD.Print("Day ended");
    }

    // Called by the "Next Day" button on the summary screen
    public void StartNextDay()
    {
        _currentDay++;
        if (_currentDay > 55) { GD.Print("Game complete"); return; }
        StartDay();
    }

    #region Ticking and game
    
    private CustomerSpawner spawner = new(new Queue());

    public void Tick()
    {
        spawner.tick();
    }

    #endregion
}