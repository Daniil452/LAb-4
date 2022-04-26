using Avalonia.Controls;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaDatabase.Model;
using Client = Supabase.Client;


namespace AvaloniaDatabase
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Button_Send(object? sender, RoutedEventArgs e)
        {

            string name1 = this.FindControl<TextBox>("Name1").Text;
            string text1 = this.FindControl<TextBox>("Text1").Text;
            if (name1.Length > 0)
            {
                if (text1.Length > 0)
                {
                    Users data1 = new Users() { name = name1, text = text1 };
                    var database = DataContext as Database;
                    database?.Send(data1);
                    database?.LoadData();
                }
            }

        }

        

        

    }
}