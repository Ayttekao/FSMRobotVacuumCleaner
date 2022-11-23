﻿using System.Drawing;
using FSMRobotVacuumCleaner.models.motion;

namespace FSMRobotVacuumCleaner.models.robot;

public class RobotVacuumCleaner
{
    private FinaleStateMachine _brain;
    private Battery _battery;
    private DustCollector _dustCollector;
    private MotionControl _motionControl;
    private Navigator _navigator;
    private Point _basePoint;

    public RobotVacuumCleaner(Battery battery, DustCollector dustCollector, MotionControl motionControl)
    {
        _brain = new FinaleStateMachine();
        _brain.PushState(CheckSystems);
        _battery = battery;
        _dustCollector = dustCollector;
        _motionControl = motionControl;
        _navigator = new Navigator(motionControl.GetMap());
        _basePoint = motionControl.GetCurrentPoint();
    }

    public void Update()
    {
        _brain.Update();
        Console.WriteLine(_brain.GetStateName());
    }

    public Point GetCurrentPoint() => _motionControl.GetCurrentPoint();

    private void CheckSystems()
    {
        if (_battery.IsDischarged())
        {
            _brain.PushState(Charging);
        }
        else
        {
            _brain.PushState(Cleaning);
        }
    }

    private void Charging()
    {
        const int AmountChargeReceived = 5;

        if (_battery.IsFullCharged())
        {
            _brain.PopState();
        }
        else
        {
            _battery.Charging(AmountChargeReceived);
        }
    }

    private void Cleaning()
    {
        var requiredChargeToReturn = 5;
        if (_battery.GetCurrentChargeLevel() < requiredChargeToReturn)
        {
            Console.WriteLine("Low battery");
            _brain.PushState(MoveBack);
            _brain.PushState(Charging);
            _brain.PushState(MoveHome);
            _brain.PushState(CretePath);
        }
        else if (_dustCollector.IsFull())
        {
            Console.WriteLine("Dust collector is full");
        }
        else
        {
            _battery.Discharge(5);
            _dustCollector.Fill(0);
            _brain.PushState(Move);
        }
    }

    private void CretePath()
    {
        _navigator.CreatePath(_motionControl.GetCurrentPoint(), _basePoint);
        _brain.PopState();
    }

    private void MoveHome()
    {
        if (_navigator.IsDestinationPoint())
        {
            _brain.PopState();
        }
        else
        {
            var destinationWaypoint = _navigator.GetDestinationWaypoint();
            _motionControl.MoveToPoint(destinationWaypoint);
        }
    }

    private void MoveBack()
    {
        if (_navigator.IsStartPoint())
        {
            _brain.PopState();
        }
        else
        {
            var startWaypoint = _navigator.GetStartWaypoint();
            _motionControl.MoveToPoint(startWaypoint);
        }
    }

    private void Move()
    {
        _motionControl.Move();
        _brain.PopState();
    }
}