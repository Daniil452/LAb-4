using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Supabase;
using Supabase.Realtime;
using Client = Supabase.Client;

namespace AvaloniaDatabase.Model
{

    public class Database : INotifyPropertyChanged
    {
        public Database()
        {
            // В конструкторе создаем массив со студентами,
            // в котором будут храниться все строки из таблицы
            Table = new List<Users>();

            // Подключаемся к базе данных
            string url = "https://yhbrhexkmxozjdelxlmi.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InloYnJoZXhrbXhvempkZWx4bG1pIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NDg4MDI5MTIsImV4cCI6MTk2NDM3ODkxMn0.4SbGHkMkhomCSjvAq94bN6E1ZYUlq1b3eruNrM5FkmY";

            Client.InitializeAsync(url, key, new SupabaseOptions
            {
                AutoConnectRealtime = true,
                ShouldInitializeRealtime = true
            });

            // Получаем экземпляр клиента
            Client = Client.Instance;

            // И подписываемся на события изменения в базе данных
            Client.From<Users>().On(Client.ChannelEventType.All, StudentsChanged);
        }

        // Клиент для обращения к базе данных
        private Client Client { get; }


        // Массив с пользователями из базы

        public IEnumerable<Users> Table { get; set; }

        // Событие изменения массива для обновления интерфейса
        public event PropertyChangedEventHandler? PropertyChanged;

        // При изменении данных в талице на сервере просто подгружаем данные из нее
        private void StudentsChanged(object sender, SocketResponseEventArgs a)
        {
            LoadData();
        }

        // А вот так просходит загрузка данных из талицы
        // на сервере Supabase в массив нашей программы
        public async void LoadData()
        {
            // Берем данные из таблицы и помещаем их в массив
            var data = await Client.From<Users>().Get();
            Table = data.Models;
            // Вызов этой функции необходим для автоматического обновления
            // интерфейса программы при изменении данных в массиве со студентами
            OnPropertyChanged(nameof(Table));
        }

        // Реализация интерфейса INotifyPropertyChanged необходима для обновления формы программы
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Send(Users e)
        {
            await Client.From<Users>().Insert(e);
        }
    }
}