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
                
                go();
 
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

       
        //Следит, за окном чтоб окно было активным 
         void watchingWindowEntropia()
        {
            while (true)
            {
                Thread.Sleep(1000);
                //активно окно
                if (AutoItX.WinActive(gameWindow.WinHandle) != 1)
                {
                    GameWindowActive = false;
                    break;
                }
                // mob dry
                if (gameWindow.IsPixelExist(Radar.MobDryAr, Radar.YellowColor))
                {
                    MobDry = true;
                }
                    
            }
            

        }

        // метод выполнения
         void go()
         {
             while (GameWindowActive )
             {
                 
                
            //     Worker.ReportProgress(1, "Бот свитит");
                 GoToMob();
            //     GoSweat();
            //     GoToTP();
            //    WaitForNormHealth();
             }
         }

         public void GoToMob()
         {
             string pointInArea = "";
             bool mobFound = false;
             bool nearMob = false;
             Worker.ReportProgress(1, "Начинаю поиск моба");
             while (!mobFound && GameWindowActive)
             {
                
                 if (gameWindow.IsPixelExist(Radar.RadarAr, Radar.MobColor))
                 {
                     //mob was found
                     mobFound = true;
                     Worker.ReportProgress(1, "Моб найден");
                 }
                 else
                 {
                     Worker.ReportProgress(1, "моба нет , иду вперед");
                     GoForward(1000);
                 }

             }

             while (!nearMob && GameWindowActive)
             {
                 Worker.ReportProgress(1, "вижу моба  , иду к нему");
                 
                 foreach (Area ar in Radar.AreaArr)
                 {
                     if (gameWindow.IsPixelExist(ar, Radar.MobColor))
                     {
                         pointInArea = ar.Name;
                         break;

                     }
                 }
                 switch (pointInArea)
                 {
                     case "AR1":
                         {
                             nearMob = true;
                             break;
                         }
                     case "AR2":
                         {
                             GoForward(1000);
                             break;
                         }
                     case "AR3":
                         {
                             TurnLeft(200);
                             break;
                         }
                     case "AR4":
                         {
                             TurnRight(200);
                             break;
                         }
                     case "AR5":
                         {
                             TurnLeft(600);
                             break;
                         }
                     case "AR6":
                         {
                             TurnRight(600);
                             break;
                         }
                     case "AR7":
                         {
                             TurnLeft(3000);
                             break;
                         }
                     case "AR8":
                         {
                             TurnRight(3000);
                             break;
                         }
                     default: GoForward(1000);
                                break;

                 }
                    

                
             }


         }

         public void GoForward(int timePress)
         {
             AutoItX.AutoItSetOption("SendKeyDownDelay", timePress);
             AutoItX.Send("w");
             AutoItX.AutoItSetOption("SendKeyDownDelay", 5);

           
         }

         public void TurnLeft(int timePress)
         {
             AutoItX.AutoItSetOption("SendKeyDownDelay", timePress);
             AutoItX.Send("z");
             AutoItX.AutoItSetOption("SendKeyDownDelay", 5);

         }
         public void TurnRight(int timePress)
         {
             AutoItX.AutoItSetOption("SendKeyDownDelay", timePress);
             AutoItX.Send("c");
             AutoItX.AutoItSetOption("SendKeyDownDelay", 5);

         }

    }

    
}
