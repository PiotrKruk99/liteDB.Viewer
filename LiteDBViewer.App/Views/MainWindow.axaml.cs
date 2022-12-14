using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Avalonia.Data;
using Avalonia.Themes.Simple;
using LiteDBViewer.Models;

namespace LiteDBViewer.Views
{
    public class MainWindow : Window
    {
        private TextBlock testOutputTB;
        private ComboBox tableNamesCB;
        private DataGrid tableOutputDG;
        private RadioButton lightMode;
        private RadioButton darkMode;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);

#if DEBUG
            this.AttachDevTools();
#endif

            testOutputTB = this.FindControl<TextBlock>("testOutputTB")!;
            tableNamesCB = this.FindControl<ComboBox>("tableNamesCB")!;
            tableOutputDG = this.FindControl<DataGrid>("tableOutputDG")!;
            lightMode = this.FindControl<RadioButton>("lightMode")!;
            darkMode = this.FindControl<RadioButton>("darkMode")!;
        }

        private void FillTableOutputDG(string tableName)
        {
            var resultTable = LiteDBOper.GetTable(tableName) as List<List<KeyValuePair<string, object>>>;
            DataTable dataTable = new DataTable();

            tableOutputDG.Columns.Clear();

            if (resultTable != null && resultTable.Count > 0)
            {
                foreach (var cell in resultTable[0])
                {
                    DataColumn dc = new(cell.Key);
                    dataTable.Columns.Add(dc);
                }

                foreach (var row in resultTable)
                {
                    DataRow dr = dataTable.NewRow();

                    foreach (var cell in row)
                    {
                        dr[cell.Key] = cell.Value;
                    }

                    dataTable.Rows.Add(dr);
                }

                var cols = dataTable.Columns;

                for (var i = 0; i < cols.Count; i++)
                {
                    tableOutputDG.Columns.Add(new DataGridTextColumn()
                    {
                        Header = cols[i].ColumnName,
                        Binding = new Binding($"Row.ItemArray[{i}]"),
                        CanUserSort = true
                    });
                }

                tableOutputDG.Items = dataTable.DefaultView;
            }
        }

        public async void OnOpenClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Title = "database file openning"
            };

            var filePath = await openDialog.ShowAsync(this);

            if (filePath != null && filePath.Length > 0)
            {
                LiteDBOper.OpenLDB(filePath[0]);
            }

            var tablesNames = LiteDBOper.GetTablesNames();

            tableNamesCB.Items = tablesNames;

            if (tableNamesCB.ItemCount > 0)
            {
                tableNamesCB.SelectedIndex = 0;
            }
        }

        public async void OnOpenWithPasswordClick(object sender, RoutedEventArgs e)
        {
            //this.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Colors.Aqua);

            var openDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Title = "database file openning"
            };

            var filePath = await openDialog.ShowAsync(this);

            if (filePath != null && filePath.Length > 0)
            {
                var dialog = new PasswordWindow();
                dialog.FontSize = this.FontSize;
                this.IsEnabled = false;
                string? password = await dialog.ShowDialog<string?>(this);
                this.IsEnabled = true;

                if (password != null)
                    LiteDBOper.OpenLDB(filePath[0], password);
            }

            var tablesNames = LiteDBOper.GetTablesNames();

            tableNamesCB.Items = tablesNames;

            if (tableNamesCB.ItemCount > 0)
            {
                tableNamesCB.SelectedIndex = 0;
            }
        }

        public void OnCloseClick(object sender, RoutedEventArgs e)
        {
            LiteDBOper.CloseLDB();

            tableNamesCB.Items = new List<string>();

            //(App.Current!.Styles[0] as Avalonia.Themes.Fluent.FluentTheme)!.Mode = Avalonia.Themes.Fluent.FluentThemeMode.Dark; //this is how to change theme to dark
            //(this.DataContext as MainWindowViewModel)!._appLifetime.Shutdown(); //this is how get to viewmodel
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillTableOutputDG((tableNamesCB.SelectedItem as string)!);
        }

        public async void OnTestButtonClick(object sender, RoutedEventArgs e)
        {
            //this.Background = Avalonia.Media.Brushes.LightBlue;

            var dialog = new PasswordWindow();
            dialog.FontSize = this.FontSize;
            this.IsEnabled = false;
            string? result = await dialog.ShowDialog<string?>(this);
            this.IsEnabled = true;

            if (result != null)
                testOutputTB.Text = result;
        }

        public void ChangeMode(string mode)
        {
            switch (mode)
            {
                case "light":
                    (App.Current!.Styles[0] as SimpleTheme)!.Mode = SimpleThemeMode.Light;
                    lightMode.IsChecked = true;
                    darkMode.IsChecked = false;
                    tableOutputDG.AlternatingRowBackground = Avalonia.Media.Brushes.LightGray;
                    break;
                case "dark":
                    (App.Current!.Styles[0] as SimpleTheme)!.Mode = SimpleThemeMode.Dark;
                    lightMode.IsChecked = false;
                    darkMode.IsChecked = true;
                    tableOutputDG.AlternatingRowBackground = Avalonia.Media.Brushes.Gray;
                    break;
            }
        }

        public void ShortColumns()
        {
            tableOutputDG.MaxColumnWidth = 200;
        }

        public void LongColumns()
        {
            tableOutputDG.MaxColumnWidth = double.PositiveInfinity;
        }
    
        public void ChangeFontSize(int size)
        {
            this.FontSize = size;
        }
    }
}