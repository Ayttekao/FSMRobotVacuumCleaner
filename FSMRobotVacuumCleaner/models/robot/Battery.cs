namespace FSMRobotVacuumCleaner.models.robot;

public class Battery
{
    private int _currentChargeLevel;
    private readonly int _maxChargeLevel;

    public Battery(int currentChargeLevel, int maxChargeLevel)
    {
        _currentChargeLevel = currentChargeLevel;
        _maxChargeLevel = maxChargeLevel;
    }
    
    public bool IsDischarged() => _currentChargeLevel < _maxChargeLevel;

    public bool IsFullDischarged() => _currentChargeLevel == 0;
    
    public bool IsFullCharged() => _currentChargeLevel == _maxChargeLevel;

    public int GetCurrentChargeLevel() => _currentChargeLevel;

    public void Discharge(int expenditure)
    {
        if (IsFullDischarged())
        {
            throw new InvalidOperationException("The battery is low");
        }
        else
        {
            _currentChargeLevel -= expenditure;
        }
    }

    public void Charging(int number)
    {
        if (!IsFullCharged())
        {
            _currentChargeLevel += number;
        }
    }
}