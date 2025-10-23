namespace Soundboard4MacroDeck.Views.Common;

/// <summary>
/// Controller that orchestrates DataGridView operations by combining data management and toolbar operations.
/// Provides a unified interface for grid data binding, CRUD operations, and event handling.
/// </summary>
/// <typeparam name="T">The type of items in the grid.</typeparam>
internal sealed class DataGridController<T> where T : class
{
    private readonly DataGridHelper<T> _gridHelper;
    private readonly DataGridToolbarHelper<T> _toolbarHelper;
    private OperationLogger? _toolbarLogger;
    private string? _itemName;

    /// <summary>
    /// Gets the data helper for direct access to grid data operations.
    /// </summary>
    public DataGridHelper<T> Grid => _gridHelper;

    /// <summary>
    /// Initializes a new instance of DataGridController.
    /// </summary>
    /// <param name="grid">The DataGridView to manage.</param>
    public DataGridController(DataGridView grid)
    {
        ArgumentNullException.ThrowIfNull(grid);

        _gridHelper = new DataGridHelper<T>(grid);
        _toolbarHelper = new DataGridToolbarHelper<T>(_gridHelper);
    }

    public void SetLogger(OperationLogger toolbarLogger, string itemName)
    { 
        ArgumentNullException.ThrowIfNull(toolbarLogger);
        ArgumentException.ThrowIfNullOrWhiteSpace(itemName);
        if (_toolbarLogger is not null)
        {
            throw new InvalidOperationException("Toolbar logger has already been set.");
        }
        _toolbarLogger = toolbarLogger;
        _itemName = itemName;
    }

    /// <summary>
    /// Attaches a click handler to the add button.
    /// </summary>
    /// <param name="addButton">The button that triggers add operations.</param>
    /// <param name="itemFactory">Factory function to create a new item instance.</param>
    /// <param name="addToViewModel">Function that adds the item to the ViewModel/database. Returns true on success.</param>
    /// <param name="itemName">Name of the item type for logging (e.g., "Audio category").</param>
    /// <param name="logSuccess">Optional callback to log success messages.</param>
    /// <param name="logFailure">Optional callback to log failure messages.</param>
    public void AttachAddButton(
        ToolStripButton addButton,
        Func<T?> itemFactory,
        Func<T, bool> addToViewModel)
    {
        ArgumentNullException.ThrowIfNull(addButton);
        ArgumentNullException.ThrowIfNull(itemFactory);
        ArgumentNullException.ThrowIfNull(addToViewModel);

        AttachAddButton(
            addButton, 
            _ => HandleAdd(
                itemFactory,
                addToViewModel
            )
        );
    }

    /// <summary>
    /// Attaches a click handler to the add button with a custom operation (e.g., with dialog).
    /// </summary>
    /// <param name="addButton">The button that triggers add operations.</param>
    /// <param name="onAddClick">The custom action to execute when the button is clicked.</param>
    public void AttachAddButton(ToolStripButton addButton, Action<DataGridController<T>> onAddClick)
    {
        ArgumentNullException.ThrowIfNull(addButton);
        ArgumentNullException.ThrowIfNull(onAddClick);

        addButton.Click += (sender, e) => onAddClick(this);
    }

    /// <summary>
    /// Handles the add operation with logging callbacks.
    /// </summary>
    /// <param name="itemFactory">Factory function to create a new item instance.</param>
    /// <param name="addToViewModel">Function that adds the item to the ViewModel/database. Returns true on success.</param>
    /// <param name="itemName">Name of the item type for logging (e.g., "Audio category").</param>
    /// <param name="logSuccess">Optional callback to log success message.</param>
    /// <param name="logFailure">Optional callback to log failure message.</param>
    public void HandleAdd(
        Func<T?> itemFactory,
        Func<T, bool> addToViewModel)
    {
        HandleAddWithCallbacks(
            itemFactory,
            addToViewModel,
            onSuccess: _ => _toolbarLogger?.Info($"{_itemName} added successfully."),
            onFailure: _ => _toolbarLogger?.Error($"Failed to add {_itemName}.")
        );
    }

    /// <summary>
    /// Handles the add operation with custom success/failure callbacks for advanced scenarios.
    /// </summary>
    /// <param name="itemFactory">Factory function to create a new item instance.</param>
    /// <param name="addToViewModel">Function that adds the item to the ViewModel/database. Returns true on success.</param>
    /// <param name="onSuccess">Optional callback invoked when the item is added successfully.</param>
    /// <param name="onFailure">Optional callback invoked when the item fails to add.</param>
    public void HandleAddWithCallbacks(
        Func<T?> itemFactory,
        Func<T, bool> addToViewModel,
        Action<T>? onSuccess = null,
        Action<T?>? onFailure = null)
    {
        _toolbarHelper.HandleAdd(itemFactory, addToViewModel, onSuccess, onFailure);
    }

    /// <summary>
    /// Attaches a click handler to the remove button.
    /// </summary>
    /// <param name="removeButton">The button that triggers remove operations.</param>
    /// <param name="removeFromViewModel">Function that removes the item from the ViewModel/database. Returns true on success.</param>
    /// <param name="itemName">Name of the item type for logging (e.g., "Audio category").</param>
    /// <param name="logSuccess">Optional callback to log success messages.</param>
    public void AttachRemoveButton(
        ToolStripButton removeButton,
        Func<T, bool> removeFromViewModel)
    {
        ArgumentNullException.ThrowIfNull(removeButton);
        ArgumentNullException.ThrowIfNull(removeFromViewModel);

        AttachRemoveButton(
            removeButton,
            _ => HandleRemove(
                removeFromViewModel
            )
        );
    }


    /// <summary>
    /// Attaches a click handler to the remove button with a custom operation (e.g., with dialog).
    /// </summary>
    /// <param name="removeButton">The button that triggers remove operations.</param>
    /// <param name="onRemoveClick">The custom action to execute when the button is clicked.</param>
    public void AttachRemoveButton(ToolStripButton removeButton, Action<DataGridController<T>> onRemoveClick)
    {
        ArgumentNullException.ThrowIfNull(removeButton);
        ArgumentNullException.ThrowIfNull(onRemoveClick);

        removeButton.Click += (sender, e) => onRemoveClick(this);
    }


    /// <summary>
    /// Handles the remove operation with logging callbacks.
    /// </summary>
    /// <param name="removeFromViewModel">Function that removes the item from the ViewModel/database. Returns true on success.</param>
    /// <param name="itemName">Name of the item type for logging (e.g., "Audio category").</param>
    /// <param name="logSuccess">Optional callback to log success message.</param>
    /// <param name="logNoSelection">Optional callback to log when no item is selected.</param>
    private void HandleRemove(
        Func<T, bool> removeFromViewModel)
    {
        HandleRemoveWithCallbacks(
            removeFromViewModel,
            onSuccess: _ => _toolbarLogger?.Info($"{_itemName} removed successfully.")
        );
    }

    /// <summary>
    /// Handles the remove operation with custom callbacks for advanced scenarios.
    /// </summary>
    /// <param name="removeFromViewModel">Function that removes the item from the ViewModel/database. Returns true on success.</param>
    /// <param name="onSuccess">Optional callback invoked when the item is removed successfully.</param>
    /// <param name="onNoSelection">Optional callback invoked when no item is selected.</param>
    public void HandleRemoveWithCallbacks(
        Func<T, bool> removeFromViewModel,
        Action<T>? onSuccess = null,
        Action? onNoSelection = null)
    {
        _toolbarHelper.HandleRemove(removeFromViewModel, onSuccess, onNoSelection);
    }
}
