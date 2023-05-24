using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KirimEmailOtomatis
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer _timer;
        DateTime _sceduleTime;
        Thread[] Thr;

        public Service1()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
        }

        public void onDebug()
        {
            OnStart(null);
        }




        protected override void OnStart(string[] args)
        {
            _timer.Enabled = true;
            int _interval = 5 * 60 * 1000; // 5 menit
                                           // double _interval = (double)GetNextInterval();
            _timer.Interval = _interval;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(ServiceTimer_Tick);
            ServiceLog.WriteErrorLog("Daily Reporting service started");
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            ServiceLog.WriteErrorLog("Daily Reporting service stopped");
        }

        private void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SettingLibMulti.Class1 NewEncrypt = new SettingLibMulti.Class1();
                if (NewEncrypt.GetJmlKoneksi() > 0)
                {
                    if (Thr == null)
                    {
                        //REDIM THREAD
                        Thr = new Thread[NewEncrypt.GetJmlKoneksi()];

                        for (int x = 0; x < NewEncrypt.GetJmlKoneksi(); x++)
                        {
                            Thr[x] = new Thread(() => Proses())
                            {
                                Name = x.ToString()
                            };
                        }
                    }
                    else
                    {
                        if (NewEncrypt.GetJmlKoneksi() != Thr.Length)
                        {
                            Thr = new Thread[NewEncrypt.GetJmlKoneksi()];

                            for (int x = 0; x < NewEncrypt.GetJmlKoneksi(); x++)
                            {
                                Thr[x] = new Thread(() => Proses())
                                {
                                    Name = x.ToString()
                                };
                            }
                        }
                    }
                    _Start();
                }
                else
                {
                    ServiceLog.WriteErrorLog("Executing DC NOT FOUND");
                }
            }
            catch (Exception ex)
            {
                Thr = null;
                ServiceLog.WriteErrorLog(ex);
            }

        }

        private void _Start()
        {
            ambillagi:
            try
            {
                SettingLibMulti.Class1 NewEncrypt = new SettingLibMulti.Class1();
                if (NewEncrypt.GetJmlKoneksi() > 0)
                {
                    for (int x = 0; x < NewEncrypt.GetJmlKoneksi(); x++)
                    {
                        try
                        {
                            if (!Thr[x].IsAlive)
                            {
                                ServiceLog.WriteErrorLog("Thread - " + Thr[x].Name + "started");
                                Thr[x] = new Thread(() => Proses())
                                {
                                    Name = x.ToString(),
                                    IsBackground = true
                                };
                                Thr[x].Start();
                            }
                            else
                                ServiceLog.WriteErrorLog("Thread - " + Thr[x].Name + "Is Alive");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    ServiceLog.WriteErrorLog("Executing DC NOT FOUND");
                }
            }catch(Exception ex)
            {
                ServiceLog.WriteErrorLog(ex);
                goto ambillagi;
            }
        }


        private void Proses()
        {
            Thread t = Thread.CurrentThread;
            try
            {
                string x = t.Name.ToString();
                ServiceLog.WriteErrorLog("add " + x + "");
                ServiceLog Serv = new ServiceLog(x);
                Serv.Process();
            }
            catch(Exception ex)
            {
                ServiceLog.WriteErrorLog(ex);
            }
        }
    }
}
