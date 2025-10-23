namespace Soundboard4MacroDeck.Views.Common;

/// <summary>
/// Helper class to manage common add/remove toolbar operations with DataGridHelper.
/// Encapsulates the pattern of creating, adding, removing items with optional callbacks.
/// </summary>
/// <typeparam name="T">The type of items in the grid.</typeparam>
internal sealed class DataGridToolbarHelper<T> where T : class
{
    private readonly DataGridHelper<T> _gridHelper;

    /// <summary>
    /// Initializes a new instance of DataGridToolbarHelper.
    /// </summary>
    /// <param name="gridHelper">The DataGridHelper to work with.</param>
    public DataGridToolbarHelper(DataGridHelper<T> gridHelper)
    {
        ArgumentNullException.ThrowIfNull(gridHelper);
        _gridHelper = gridHelper;
    }

    /// <summary>
    /// Handles the add operation: creates a new item, validates via ViewModel, adds to grid.
    /// </summary>
    /// <param name="itemFactory">Factory function to create a new item instance.</param>
    /// <param name="addToViewModel">Function that adds the item to the ViewModel/database. Returns true on success.</param>
    /// <param name="onSuccess">Optional callback invoked when the item is added successfully.</param>
    /// <param name="onFailure">Optional callback invoked when the item fails to add.</param>
    public void HandleAdd(
        Func<T?> itemFactory,
        Func<T, bool> addToViewModel,
        Action<T>? onSuccess = null,
        Action<T?>? onFailure = null)
    {
        ArgumentNullException.ThrowIfNull(itemFactory);
        ArgumentNullException.ThrowIfNull(addToViewModel);

        var newItem = itemFactory();
        if (newItem is null)
        {
            // Item creation cancelled or failure handled
            return;
        }

        if (addToViewModel(newItem))
        {
            _gridHelper.Add(newItem);
            onSuccess?.Invoke(newItem);
            return;
        }

        onFailure?.Invoke(newItem);
    }

    /// <summary>
    /// Handles the remove operation: gets selected item, validates via ViewModel, removes from grid.
    /// </summary>
    /// <param name="removeFromViewModel">Function that removes the item from the ViewModel/database. Returns true on success.</param>
    /// <param name="onSuccess">Optional callback invoked when the item is removed successfully.</param>
    /// <param name="onNoSelection">Optional callback invoked when no item is selected.</param>
    public void HandleRemove(
        Func<T, bool> removeFromViewModel,
        Action<T>? onSuccess = null,
        Action? onNoSelection = null)
    {
        ArgumentNullException.ThrowIfNull(removeFromViewModel);

        var selectedItem = _gridHelper.GetSelectedItem();
        if (selectedItem is null)
        {
            onNoSelection?.Invoke();
            return;
        }

        if (removeFromViewModel(selectedItem))
        {
            _gridHelper.Remove(selectedItem);
            onSuccess?.Invoke(selectedItem);
        }
    }
}
