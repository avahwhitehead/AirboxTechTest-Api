using Microsoft.AspNetCore.Mvc;
using TodoApi.Managers;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase {
	private readonly ILogger<TasksController> _logger;
	private readonly ItemsManager _itemsManager;

	public TasksController(
		ILogger<TasksController> logger,
		ItemsManager itemsManager
	) {
		_logger = logger;
		_itemsManager = itemsManager;
	}

	/// <summary>
	/// Endpoint to get all the tasks
	/// </summary>
	/// <returns>200 Ok, with a list of all the created tasks</returns>
	[HttpGet(Name = "GetTasks")]
	public ActionResult<IEnumerable<Item>> GetItems() {
		_logger.LogTrace("Listing tasks");
		return _itemsManager.GetItems().ToList();
	}

	/// <summary>
	/// Get task details
	/// </summary>
	/// <returns>404 Not found, if no task exists with the given id</returns>
	/// <returns>200 Ok, with the item fetched</returns>
	[HttpGet("{taskId:int}", Name = "GetTask")]
	public ActionResult<Item> GetItem(int taskId) {
		_logger.LogTrace("Fetching task with ID {id}", taskId);
		var item = _itemsManager.GetItem(taskId);

		if (item == null) return NotFound();
		return item;
	}

	/// <summary>
	/// Endpoint to create a set of tasks
	/// </summary>
	/// <returns>200 Ok, with a list of the newly created tasks</returns>
	[HttpPost(Name = "CreateTasks")]
	public ActionResult<IEnumerable<Item>> CreateItems([FromBody] IReadOnlyCollection<ItemDescriptor> items) {
		_logger.LogTrace("Adding {count} items", items.Count);
		return items.Select(itemDescriptor => _itemsManager.AddItem(itemDescriptor)).ToList();
	}

	/// <summary>
	/// Endpoint to update an existing task
	/// </summary>
	/// <returns>404 Not found, if no task exists with the given id</returns>
	/// <returns>200 Ok, with the updated task object</returns>
	[HttpPut("{taskId:int}", Name = "UpdateTask")]
	public ActionResult<Item?> UpdateItem(int taskId, [FromBody] ItemDescriptor item) {
		//Check that the task exists
		if (!_itemsManager.Exists(taskId)) {
			_logger.LogTrace("Couldn't find item with Id {id}", taskId);
			return NotFound();
		}

		//Update the item
		//And return the update item
		_logger.LogTrace("Updating item with Id {id}", taskId);
		return _itemsManager.UpdateItem(taskId, item);
	}

	/// <summary>
	/// Endpoint to delete an existing task
	/// </summary>
	/// <returns>No content</returns>
	[HttpDelete("{taskId:int}", Name = "DeleteTask")]
	public ActionResult DeleteItem(int taskId) {
		_logger.LogTrace("Deleting item with Id {id}", taskId);
		//Delete the item, if it exists
		_itemsManager.DeleteItem(taskId);
		//Success
		return NoContent();
	}
}
