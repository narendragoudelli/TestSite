//-----------------------------------------------------------------------
// <summary>
// This file defines the GridHelpers class.
// </summary>
// <copyright file="GridHelpers.cs" company="Flow International">
//   Copyright (c) Flow International. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NoahGUI
{
    /// <summary>
    /// The GridHelper class
    /// </summary>
    public class GridHelpers
    {
        #region Properties
        /// <summary>
        /// Adds the specified number of Rows to RowDefinitions. 
        /// Default Height is Auto
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached(
                "RowCount", typeof (int), typeof (GridHelpers),
                new PropertyMetadata(-1, RowCountChanged));

        /// <summary>
        /// Adds the specified number of Columns to ColumnDefinitions. 
        /// Default Width is Auto
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, ColumnCountChanged));

        /// <summary>
        /// Makes the specified Column's Width equal to Star. 
        /// Can set on multiple Columns
        /// </summary>
        public static readonly DependencyProperty StarColumnsProperty =
            DependencyProperty.RegisterAttached(
                "StarColumns", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarColumnsChanged));

        /// <summary>
        /// Makes the specified Row's Height equal to Star. 
        /// Can set on multiple Rows
        /// </summary>
        public static readonly DependencyProperty StarRowsProperty =
            DependencyProperty.RegisterAttached(
                "StarRows", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarRowsChanged));
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the row count
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The row count</returns>
        public static int GetRowCount(DependencyObject obj)
        {
            return (int) obj.GetValue(RowCountProperty);
        }

        /// <summary>
        /// Sets the row count
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The row count</param>
        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        /// <summary>
        /// Handles the row count change event
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="e">The dependency property change event arguments</param>
        // Change Event - Adds the Rows
        public static void RowCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
            {
                return;
            }

            Grid grid = (Grid) obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            SetStarRows(grid);
        }


        /// <summary>
        /// Gets the column count
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The column count</returns>
        public static int GetColumnCount(DependencyObject obj)
        {
            return (int) obj.GetValue(ColumnCountProperty);
        }

        /// <summary>
        /// Sets the column count
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The column count</param>
        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        /// <summary>
        /// Handles the column count changes
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="e">The dependency property change event</param>
        public static void ColumnCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int) e.NewValue < 0)
            {
                return;
            }

            Grid grid = (Grid) obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int) e.NewValue; i++)
            {
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() {Width = GridLength.Auto});
            }

            SetStarColumns(grid);
        }

        /// <summary>
        /// Gets the start rows
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The start rows</returns>
        public static string GetStarRows(DependencyObject obj)
        {
            return (string) obj.GetValue(StarRowsProperty);
        }

        /// <summary>
        /// Sets the start rows
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The start rows</param>
        public static void SetStarRows(DependencyObject obj, string value)
        {
            obj.SetValue(StarRowsProperty, value);
        }

        /// <summary>
        /// Sets the start rows. Makes the specific height to start
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="e">The dependency property change event</param>
        public static void StarRowsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                return;
            }

            SetStarRows((Grid) obj);
        }

        /// <summary>
        /// Gets the start columns
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <returns>The start columns</returns>
        public static string GetStarColumns(DependencyObject obj)
        {
            return (string) obj.GetValue(StarColumnsProperty);
        }

        /// <summary>
        /// Sets the star columns
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="value">The start columns</param>
        public static void SetStarColumns(DependencyObject obj, string value)
        {
            obj.SetValue(StarColumnsProperty, value);
        }

        /// <summary>
        /// Handles the start column changes. Makes the specified column width equals to start
        /// </summary>
        /// <param name="obj">The dependency object</param>
        /// <param name="e">The dependency property change event</param>
        public static void StarColumnsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                return;
            }

            SetStarColumns((Grid) obj);
        }

        /// <summary>
        /// Sets the start columns to the Grid
        /// </summary>
        /// <param name="grid">The grid</param>
        private static void SetStarColumns(Grid grid)
        {
            string[] starColumns =
                GetStarColumns(grid).Split(',');

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                if (starColumns.Contains(i.ToString()))
                {
                    grid.ColumnDefinitions[i].Width =
                        new GridLength(1, GridUnitType.Star);
                }
            }
        }

        /// <summary>
        /// Sets the start rows to the grid
        /// </summary>
        /// <param name="grid">The grid</param>
        private static void SetStarRows(Grid grid)
        {
            string[] starRows =
                GetStarRows(grid).Split(',');

            for (var i = 0; i < grid.RowDefinitions.Count; i++)
            {
                if (starRows.Contains(i.ToString()))
                {
                    grid.RowDefinitions[i].Height =
                        new GridLength(1, GridUnitType.Star);
                }
            }
        }
        #endregion
    }
}