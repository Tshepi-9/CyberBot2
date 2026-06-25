using System.Collections.Generic;

public class TaskManager
{
    private TaskStorageHelper storage;

    public TaskManager()
    {
        storage = new TaskStorageHelper();
    }

    public string AddTask(string title,
                          string description,
                          string reminder)
    {
        storage.AddTask(title, description, reminder);

        return "Task added successfully.";
    }

    public List<CyberTask> GetAllTasks()
    {
        return storage.LoadTasks();
    }

    public void MarkAsComplete(int id)
    {
        storage.MarkAsComplete(id);
    }

    public void DeleteTask(int id)
    {
        storage.DeleteTask(id);
    }
}