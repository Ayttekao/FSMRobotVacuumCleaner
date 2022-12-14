namespace FSMRobotVacuumCleaner.Models.Robot;

public class DustCollector
{
    private int _capacity;
    private int _currentOccupancy;

    public DustCollector(int currentOccupancy, int capacity)
    {
        _currentOccupancy = currentOccupancy;
        _capacity = capacity;
    }

    public bool IsFull() => _capacity <= _currentOccupancy;

    public bool IsEmpty() => _currentOccupancy == 0;

    public void Fill(int amountOfDust)
    {
        if (IsFull())
        {
            throw new OverflowException("Dust collector is full");
        }
        else
        {
            _currentOccupancy += amountOfDust;
        }
    }

    public void Clean() => _currentOccupancy = 0;
}