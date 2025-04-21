using System;

public class Task
{
    public string Name;
    public int Priority;
    public Func<bool> Condition;
    public Action Action;
    public Task(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }
}