using TodoApi.Models;

namespace TodoApi.Managers;

public class ItemsManager {
	private int _nextId = 0;
	private readonly List<Item> _items = new List<Item> {
		new Item {
			Id = 1,
			Assignedto = "User1",
			Priority = Priority.LOW,
			TaskStatus = "In Progress",
			TaskSummary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et ante euismod, rhoncus mauris non, facilisis mauris. Praesent in tellus risus. Ut euismod sapien eu tincidunt efficitur.",
		},
		new Item {
			Id = 2,
			Assignedto = "User2",
			Priority = Priority.HIGH,
			TaskStatus = "To Do",
			TaskSummary = "Donec sit amet semper velit. Nulla tincidunt luctus porttitor. Nam vitae ante quis ipsum tincidunt sagittis. Suspendisse potenti. In porttitor lectus nibh, ut venenatis purus commodo a. ",
		},
		new Item {
			Id = 3,
			Assignedto = "User1",
			Priority = Priority.MEDIUM,
			TaskStatus = "Done",
			TaskSummary = "Etiam vestibulum luctus urna eu mollis. Aliquam vitae purus vitae mi efficitur dignissim eu ac risus. Fusce facilisis enim vel faucibus interdum.",
		},
		new Item {
			Id = 4,
			Assignedto = "User3",
			Priority = Priority.UNASSIGNED,
			TaskStatus = "In Progress",
			TaskSummary = "Praesent consequat turpis a risus elementum vestibulum. Quisque vehicula augue et diam suscipit aliquet. Fusce euismod diam vel eros euismod, a lacinia mauris sagittis",
		},
		new Item {
			Id = 5,
			Assignedto = "User1",
			Priority = Priority.UNASSIGNED,
			TaskStatus = "Done",
			TaskSummary = "Curabitur posuere ante non lacus consectetur, at blandit enim vestibulum. Suspendisse ornare urna eu blandit finibus. Maecenas interdum tellus id posuere lacinia.",
		},
	};

	public ItemsManager() {
		_nextId = 0;
		//Ensure that _nextId will always produce a higher number than any existing id
		foreach (var item in _items) {
			if (item.Id <= _nextId) continue;
			_nextId = item.Id;
		}
	}

	/// <summary>
	/// Get a list of all the stored items.
	/// </summary>
	/// <returns>Collection of all the items.</returns>
	public IReadOnlyCollection<Item> GetItems() {
		return _items;
	}

	/// <summary>
	/// Get a specific item
	/// </summary>
	/// <returns>Item with the given id</returns>
	/// <returns>null, if no task exists with the given id</returns>
	public Item? GetItem(int itemId) {
		return _items.Find(i => i.Id == itemId)?.Clone();
	}

	/// <summary>
	///
	/// </summary>
	/// <param name="descriptor"></param>
	/// <returns></returns>
	public Item AddItem(ItemDescriptor descriptor) {
		var item = DescriptorToItem(descriptor);
		_items.Add(item);
		return item.Clone();
	}

	/// <summary>
	/// Delete an item from the store
	/// </summary>
	/// <param name="taskId">The ID of the task to delete</param>
	/// <returns>true, if a task was deleted</returns>
	/// <returns>false, if no tasks were deleted (no tasks with given id)</returns>
	public bool DeleteItem(int taskId) {
		return _items.RemoveAll(i => i.Id == taskId) > 0;
	}

	/// <summary>
	/// Update a task in the store
	/// </summary>
	/// <param name="taskId">Id of the task to create</param>
	/// <param name="descriptor">Description of the item with the changes applied</param>
	/// <returns>The updated object</returns>
	public Item UpdateItem(int taskId, ItemDescriptor descriptor) {
		//Attempt to find the item
		var item = _items.Find(i => i.Id == taskId);
		if (item == null) {
			throw new KeyNotFoundException($"Couldn't find task with Id {taskId}");
		}
		//Update the item with the new data
		item.Assignedto = descriptor.Assignedto;
		item.Priority = descriptor.Priority;
		item.TaskStatus = descriptor.TaskStatus;
		item.TaskSummary = descriptor.TaskSummary;
        //Return the updated item
		return item.Clone();
	}

	/// <summary>
	/// Update a task in the store
	/// </summary>
	/// <param name="descriptor">The modified item</param>
	/// <returns>The updated object</returns>
	public Item UpdateItem(Item descriptor) {
		//Attempt to find the item
		var item = _items.Find(i => i.Id == descriptor.Id);
		if (item == null) {
			throw new KeyNotFoundException($"Couldn't find task with Id {descriptor.Id}");
		}
		//Update the item with the new data
		item.Assignedto = descriptor.Assignedto;
		item.Priority = descriptor.Priority;
		item.TaskStatus = descriptor.TaskStatus;
		item.TaskSummary = descriptor.TaskSummary;
        //Return the updated item
		return item.Clone();
	}

	/// <summary>
	/// Check whether an item with a given id exists
	/// </summary>
	/// <param name="taskId">Id of the object to check</param>
	/// <returns>true, if the object was found</returns>
	/// <returns>false, if no items have the matching id</returns>
	public bool Exists(int taskId) {
		return _items.Exists(i => i.Id == taskId);
	}

	/// <summary>
	/// Convert an item descriptor to an Item object, incrementing the ID.
	/// </summary>
	/// <param name="item">Descriptor from which to create the item.</param>
	/// <returns>The created item object</returns>
	private Item DescriptorToItem(ItemDescriptor item) {
		return new Item {
			Id = _nextId++,
			Assignedto = item.Assignedto,
			Priority = item.Priority,
			TaskStatus = item.TaskStatus,
			TaskSummary = item.TaskSummary,
		};
	}
}
