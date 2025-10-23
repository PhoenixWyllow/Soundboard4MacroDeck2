using System.ComponentModel;

namespace Soundboard4MacroDeck.Views.Common;

/// <summary>
/// Helper class to manage DataGridView operations with type-safe binding.
/// Encapsulates common grid operations like Add, Remove, Refresh, and GetSelected.
/// </summary>
/// <typeparam name="T">The type of items in the grid.</typeparam>
internal sealed class DataGridHelper<T> where T : class
{
    private readonly DataGridView _grid;
    private readonly BindingList<T> _dataSource;
    private bool _isDataBound;

    /// <summary>
    /// Gets the underlying BindingList data source.
    /// </summary>
    public BindingList<T> DataSource => _dataSource;

    /// <summary>
    /// Gets the count of items in the grid.
    /// </summary>
    public int Count => _dataSource.Count;

    /// <summary>
    /// Gets the column builder for fluent column configuration.
    /// </summary>
    public DataGridColumnBuilder<T> Columns { get; }

    /// <summary>
    /// Initializes a new instance of DataGridHelper.
    /// Data binding is deferred until BindData is called.
    /// </summary>
    /// <param name="grid">The DataGridView to manage.</param>
    public DataGridHelper(DataGridView grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        _grid = grid;
        _dataSource = new BindingList<T>();
        _isDataBound = false;
        Columns = new DataGridColumnBuilder<T>(grid);
    }

    /// <summary>
    /// Binds the grid to the data source with the provided items.
    /// This should be called after all columns are configured.
    /// </summary>
    /// <param name="items">The initial collection of items to display.</param>
    public void BindData(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        _dataSource.Clear();
        foreach (var item in items)
        {
            _dataSource.Add(item);
        }

        if (!_isDataBound)
        {
            _grid.DataSource = _dataSource;
            _isDataBound = true;
        }
    }

    /// <summary>
    /// Adds an item to the grid.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _dataSource.Add(item);
    }

    /// <summary>
    /// Removes an item from the grid.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <returns>True if the item was removed; otherwise, false.</returns>
    public bool Remove(T item)
    {
        return item is not null && _dataSource.Remove(item);
    }

    /// <summary>
    /// Gets the currently selected item from the grid.
    /// </summary>
    /// <returns>The selected item, or null if no item is selected.</returns>
    public T? GetSelectedItem()
    {
        if (_grid.SelectedRows.Count == 0)
        {
            return null;
        }

        var row = _grid.SelectedRows[0];
        return row.DataBoundItem as T;
    }

    /// <summary>
    /// Refreshes the grid binding without changing the data.
    /// Forces the grid to re-read all values from the data source.
    /// </summary>
    public void RefreshBinding()
    {
        _dataSource.ResetBindings();
    }

    /// <summary>
    /// Clears all items from the grid.
    /// </summary>
    public void Clear()
    {
        _dataSource.Clear();
    }

    /// <summary>
    /// Checks if the grid contains a specific item.
    /// </summary>
    /// <param name="item">The item to check for.</param>
    /// <returns>True if the item exists in the grid; otherwise, false.</returns>
    public bool Contains(T item)
    {
        return item is not null && _dataSource.Contains(item);
    }

    /// <summary>
    /// Gets an item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <returns>The item at the specified index.</returns>
    public T? GetItemAt(int index)
    {
        if (index < 0 || index >= _dataSource.Count)
        {
            return null;
        }

        return _dataSource[index];
    }

    /// <summary>
    /// Attaches a CellEndEdit event handler that updates an item when editing is complete.
    /// Includes basic error handling with the provided error callback.
    /// </summary>
    /// <param name="updateAction">The action to perform on the edited item (e.g., save to database).</param>
    /// <param name="onError">Optional callback invoked when an error occurs. Receives the exception.</param>
    public void OnCellEndEdit(Action<T> updateAction, Action<Exception>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(updateAction);

        _grid.CellEndEdit += (sender, e) =>
        {
            try
            {
                var editedRow = _grid.Rows[e.RowIndex];
                T editedItem = (T)editedRow.DataBoundItem;
                updateAction(editedItem);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        };
    }

    /// <summary>
    /// Attaches a CellFormatting event handler to format cell values for a specific column.
    /// </summary>
    /// <param name="columnName">The name of the column to format.</param>
    /// <param name="formatter">Function that takes the item and returns the formatted value to display.</param>
    public void OnCellFormatting(string columnName, Func<T, object?> formatter)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(columnName);
        ArgumentNullException.ThrowIfNull(formatter);

        _grid.CellFormatting += (sender, e) =>
        {
            if (_grid.Columns[e.ColumnIndex].Name != columnName)
            {
                return;
            }

            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
            {
                return;
            }

            var row = _grid.Rows[e.RowIndex];
            if (row.DataBoundItem is not T item)
            {
                return;
            }

            e.Value = formatter(item);
            e.FormattingApplied = true;
        };
    }

    /// <summary>
    /// Attaches a DataError event handler to handle data binding errors gracefully.
    /// Automatically cancels the error and prevents exceptions from propagating.
    /// </summary>
    /// <param name="onError">Callback invoked when a data error occurs. Receives column name, row index, and exception.</param>
    public void OnDataError(Action<string, int, Exception?>? onError = null)
    {
        _grid.DataError += (sender, e) =>
        {
            e.Cancel = true;
            e.ThrowException = false;

            string columnName = _grid.Columns[e.ColumnIndex]?.HeaderText ?? "Unknown";
            onError?.Invoke(columnName, e.RowIndex, e.Exception);
        };
    }

    /// <summary>
    /// Ends any edit operation currently in progress.
    /// </summary>
    public void EndEdit()
    {
        _grid.EndEdit();
    }
}
