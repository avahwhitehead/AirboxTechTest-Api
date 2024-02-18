using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TodoApi.Models;

public class ItemDescriptor {
    /// <summary>
    /// The priority given to the task
    /// </summary>
    [JsonProperty("priority")]
    public Priority Priority { get; set; }

    /// <summary>
    /// The task status
    /// </summary>
    [JsonProperty("taskStatus")]
    public string TaskStatus { get; set; }

    /// <summary>
    /// The user id associated with the task
    /// </summary>
    [JsonProperty("assignedto")]
    public string AssignedTo { get; set; }

    /// <summary>
    /// A short summary to be displayed in task lists
    /// </summary>
    [JsonProperty("tasksummary")]
    public string TaskSummary { get; set; }

    /// <summary>
    /// Create a new object with identical properties to this one.
    /// </summary>
    /// <returns></returns>
    public ItemDescriptor Clone() {
        return new ItemDescriptor {
            Priority = Priority,
            AssignedTo = AssignedTo,
            TaskStatus = TaskStatus,
            TaskSummary = TaskSummary,
        };
    }
}

public class Item: ItemDescriptor {
    /// <summary>
    /// The Task Id used internally
    /// </summary>
    [JsonProperty("taskId")]
    public int Id { get; set; }

    /// <summary>
    /// Create a new object with identical properties to this one.
    /// </summary>
    /// <returns></returns>
    public Item Clone() {
        return new Item {
            Id = Id,
            Priority = Priority,
            AssignedTo = AssignedTo,
            TaskStatus = TaskStatus,
            TaskSummary = TaskSummary,
        };
    }
}

public enum Priority {
    UNASSIGNED,
    LOW,
    MEDIUM,
    HIGH
}