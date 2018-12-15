namespace MarsRover.Service.Error
{
    public interface ILogger
    {
        void WriteErrorLog(string message);
    }
}
