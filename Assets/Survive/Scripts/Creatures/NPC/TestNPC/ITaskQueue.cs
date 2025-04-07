public interface ITaskQueue
{
    void Insert(Task task);
    Task ExtractMin();
    void DecreaseKey(Task task, int newPriority);
    bool IsEmpty();
}