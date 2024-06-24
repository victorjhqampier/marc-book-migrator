namespace Domain.Interfaces;

public interface IBookTrackerQueryInfrastructure
{
    public bool ProccessedYet();
    public int CountTotalSuccesss();
    public int CountTotalError();
    public int CountTotalWarning();
    public List<int> GetIdsError(int offset, int limit = 50);
}
