using System.Drawing;
using FSMRobotVacuumCleaner.algo;

namespace FSMRobotVacuumCleaner.models.robot;

public class RobotVacuumCleaner
{
    private FinaleStateMachine _brain;
    private Battery _battery;
    private DustCollector _dustCollector;
    private MotionControl _motionControl;

    public RobotVacuumCleaner(Battery battery, DustCollector dustCollector, MotionControl motionControl)
    {
        _brain = new FinaleStateMachine();
        _brain.PushState(CheckSystems);
        _battery = battery;
        _dustCollector = dustCollector;
        _motionControl = motionControl;
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
            _brain.PushState(Cleaning);
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
        }
        else if (_dustCollector.IsFull())
        {
            Console.WriteLine("Dust collector is full");
        }
        else
        {
            _battery.Discharge(5);
            _dustCollector.Fill(5);
            _brain.PushState(Move);
        }
    }

    private void Move()
    {
        _motionControl.Move();
        _brain.PopState();
    }
}