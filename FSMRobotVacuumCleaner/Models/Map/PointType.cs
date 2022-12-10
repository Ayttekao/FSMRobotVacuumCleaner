namespace FSMRobotVacuumCleaner.Models.Map;

public enum PointType
{
    StartPosition = 0,
    EmptySpace = -1,
    Destination = -2,
    Path = -3,
    Barrier = -4
}