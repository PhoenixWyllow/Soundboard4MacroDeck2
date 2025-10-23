namespace Soundboard4MacroDeck.Views.Common;

/// <summary>
/// Fluent builder for configuring DataGridView columns with type-safe, declarative syntax.
/// Reduces boilerplate code when setting up grid columns.
/// </summary>
/// <typeparam name="T">The type of items in the grid.</typeparam>
internal sealed class DataGridColumnBuilder<T> where T : class
{
    private readonly DataGridView _grid;

    /// <summary>
    /// Initializes a new instance of DataGridColumnBuilder.
    /// </summary>
    /// <param name="grid">The DataGridView to configure columns for.</param>
    internal DataGridColumnBuilder(DataGridView grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        _grid = grid;
    }

    /// <summary>
    /// Adds a text box column with flexible configuration options.
    /// </summary>
    /// <param name="propertyName">The property name to bind to (used for Name and DataPropertyName).</param>
    /// <param name="headerText">Optional header text (defaults to property name).</param>
    /// <param name="readOnly">Whether the column is read-only (default: false).</param>
    /// <param name="autoSize">Auto-size mode (default: Fill for editable columns, AllCells for read-only).</param>
    /// <param name="minimumWidth">Minimum width for the column (default: 200 for Fill mode, 0 for AllCells).</param>
    /// <returns>This builder for method chaining.</returns>
    public DataGridColumnBuilder<T> AddTextColumn(
        string propertyName,
        string? headerText = null,
        bool readOnly = false,
        DataGridViewAutoSizeColumnMode? autoSize = null,
        int? minimumWidth = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        // Smart defaults based on read-only state
        var sizeMode = autoSize ?? (readOnly ? DataGridViewAutoSizeColumnMode.ColumnHeader : DataGridViewAutoSizeColumnMode.Fill);
        var minWidth = minimumWidth ?? (sizeMode == DataGridViewAutoSizeColumnMode.Fill ? 200 : 5);

        AddColumn<DataGridViewTextBoxColumn>(column =>
        {
            column.Name = propertyName;
            column.DataPropertyName = propertyName;
            column.HeaderText = headerText ?? propertyName;
            column.ReadOnly = readOnly;
            column.Resizable = readOnly ? DataGridViewTriState.False : DataGridViewTriState.True;
            column.MinimumWidth = minWidth;
            column.AutoSizeMode = sizeMode;
        });

        return this;
    }

    /// <summary>
    /// Adds a combo box column for dropdown selection.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="propertyName">The property name to bind to.</param>
    /// <param name="headerText">Optional header text (defaults to column name).</param>
    /// <param name="dataSource">The data source for the combo box.</param>
    /// <param name="displayMember">The property to display in the dropdown.</param>
    /// <param name="valueMember">The property to use as the value.</param>
    /// <param name="displayStyle">The display style (default: Nothing - only show dropdown when editing).</param>
    /// <returns>This builder for method chaining.</returns>
    public DataGridColumnBuilder<T> AddComboBoxColumn(
        string columnName,
        string propertyName,
        object dataSource,
        string displayMember,
        string valueMember,
        string? headerText = null,
        DataGridViewComboBoxDisplayStyle displayStyle = DataGridViewComboBoxDisplayStyle.Nothing)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(columnName);
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentNullException.ThrowIfNull(dataSource);
        ArgumentException.ThrowIfNullOrWhiteSpace(displayMember);
        ArgumentException.ThrowIfNullOrWhiteSpace(valueMember);

        AddColumn<DataGridViewComboBoxColumn>(column =>
        {
            column.Name = columnName;
            column.DataPropertyName = propertyName;
            column.HeaderText = headerText ?? columnName;
            column.DisplayMember = displayMember;
            column.ValueMember = valueMember;
            column.DataSource = dataSource;
            column.DisplayStyle = displayStyle;
        });

        return this;
    }

    /// <summary>
    /// Adds a custom column with full configuration control via a configurator action.
    /// </summary>
    /// <typeparam name="TColumn">The type of column to add.</typeparam>
    /// <param name="columnConfigurator">Action to configure the column instance.</param>
    /// <returns>This builder for method chaining.</returns>
    public DataGridColumnBuilder<T> AddColumn<TColumn>(Action<TColumn> columnConfigurator) where TColumn : DataGridViewColumn, new()
    {
        ArgumentNullException.ThrowIfNull(columnConfigurator);

        var column = new TColumn();
        columnConfigurator(column);
        _grid.Columns.Add(column);

        return this;
    }
}
