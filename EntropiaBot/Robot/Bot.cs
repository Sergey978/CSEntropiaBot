using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EntropiaBot;
using EntropiaBot.EntropiaWindow;
using System.ComponentModel;
using AutoIt;


namespace EntropiaBot.Robot
{
    
    public class Bot
    {

        private static Bot instance;
        private EntropiaHandler gameWindow;

        public bool HealthMin { get; set; } 
        public bool MobDry { get; set; }
        public BackgroundWorker Worker { get; set; }
        private  BackgroundWorker watchingWindowWorker = new BackgroundWorker(); 
               
        private bool gameWindowActive;
        public bool GameWindowActive 
        {
            get { return gameWindowActive; } 
            set
                    {
                        if (value == false)
                        {
                           
                            gameWindowActive = false;
                            if (watchingWindowWorker.WorkerSupportsCancellation == true)
                            {
                                watchingWindowWorker.CancelAsync();
                            }

                            if (Worker.WorkerSupportsCancellation == true)
                            {
                                Worker.CancelAsync();
                            }
                        }
                        else gameWindowActive = true;
                    }
        }
        private Bot()
        {
            gameWindow = EntropiaHandler.getInstance();
            watchingWindowWorker = new BackgroundWorker();
            watchingWindowWorker.WorkerSupportsCancellation = true;
            watchingWindowWorker.WorkerReportsProgress = true;
            watchingWindowWorker.DoWork += DoWork;
          //  watchingWindowWorker.ProgressChanged += ProgressChanged;
          //  watchingWindowWorker.RunWorkerCompleted += RunWorkerCompleted;

            
        }
        public static Bot getInstance()
        {
            if (instance == null)
                instance = new Bot();
            return instance;
        }
        public void Stop()
        {
           

        }
        //метод выполняемый в фоне
        private  void DoWork(object sender, DoWorkEventArgs e)
        {
            watchingWindowEntropia();

        }

        public  void Start()
        {
            
            if (gameWindow.WinHandle.ToInt32() != 0)
            {
                GameWindowActive = true;
                
                //запускаем поток следящий, за активностью окна Ентропии
                AutoItX.WinActivate(gameWindow.WinHandle);
                watchingWindowWorker.RunWorkerAsync();
                if (Worker.WorkerReportsProgress)
                {
                    Worker.ReportProgress(1, "Бот стартовал");
                }
                
                sweat();
 
            }
            else
            {
                Worker.ReportProgress(1, "Не найдено окно Ентропии");
                if (Worker.WorkerSupportsCancellation == true)
                {
                    Worker.CancelAsync();
                }
            }
          
        }

       
        //Следит, чтоб окно было активным 
         void watchingWindowEntropia()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (AutoItX.WinActive(gameWindow.WinHandle) != 1)
                {
                    GameWindowActive = false;
                    break;
                }
                    
            }
            

        }

        // метод свитинга
         void sweat()
         {
             while (GameWindowActive)
             {
                 
                 Thread.Sleep(1000);
                 Worker.ReportProgress(1, "Бот свитит");
             }
         }

    }

    
}
