namespace FSMRobotVacuumCleaner.models.robot;

public class DustCollector
{
    private int _capacity;
    private int _currentOccupancy;

    public DustCollector(int currentOccupancy, int capacity)
    {
        _currentOccupancy = currentOccupancy;
        _capacity = capacity;
    }

    public bool IsFull() => _capacity == _currentOccupancy;

    public void Fill(int amountOfDust)
    {
        if (IsFull())
        {
            throw new OverflowException("Dust collector is full");
        }
    }

    public void Clean() => _currentOccupancy = 0;
}