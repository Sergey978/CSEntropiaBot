using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using EntropiaBot.Robot;

namespace EntropiaBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker;
        Bot bot;
        private const int MAX_LOG_LIST = 10;
        Queue<String> LogList = new Queue<string>();
        //Преобразует очередь в строку
          public String  GetMessageList()
            {
                StringBuilder list = new StringBuilder() ;
                foreach (String msg in LogList)
                    list = list.AppendLine(msg);
                        
                return list.ToString();
            }
        // Добавляет строку лога в список 
        public void AddMessageList(String msg)
        {
            if (LogList.Count == MAX_LOG_LIST)
            {
                LogList.Dequeue();
                LogList.Enqueue(msg);
            }
            else
                LogList.Enqueue(msg);
        }
        
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)this.FindResource("backgroundWoker");
            bot = Bot.getInstance();
            bot.Worker = backgroundWorker;
            
        }
        // обработка кнопки старт
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
           
            this.StartButton.IsEnabled = false;
            this.StopButton.IsEnabled = true;
            
            backgroundWorker.RunWorkerAsync();

        }



        //Запуск фоновой работы
        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
                   
            bot.Start();
                  
        }
        // Завершение фонового процесса
        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                bot.Stop();
                this.StartButton.IsEnabled = true;
                this.StopButton.IsEnabled = false;
                this.AddMessageList("Бот Остановился");
                refreshLogTextList();

            }

            else if (!(e.Error == null))
            {
                bot.Stop();
                this.StartButton.IsEnabled = true;
                this.StopButton.IsEnabled = false;
                this.AddMessageList("Ошибка");
                refreshLogTextList();
            }
            else
            {
                bot.Stop();
                this.StartButton.IsEnabled = true;
                this.StopButton.IsEnabled = false;
                this.AddMessageList("Бот Остановился");
                refreshLogTextList();
            }
            

        }
        // Запускается при измении прогресса вызов worker.ReportProgress(1,"Бот стартовал");
        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //Изменение прогесса
            AddMessageList((String)e.UserState);
            refreshLogTextList();
        }

        private void refreshLogTextList()
        {
            LogTextBox.Text = GetMessageList();

        }
       // Обработка кнопки стоп
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                this.StartButton.IsEnabled = true;
                backgroundWorker.CancelAsync();
                this.StopButton.IsEnabled = false;
            }
        }
    }
}
