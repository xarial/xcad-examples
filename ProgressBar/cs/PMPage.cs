using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.Examples.ProgressBar.Properties;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.ProgressBar
{
    [ComVisible(true)]
    [Title("Progress Bar Page")]
    [Description("Click start/stop button to run a progress bar")]
    [Icon(typeof(Resources), nameof(Resources.progres_bar))]
    public class PMPage : SwPropertyManagerPageHandler
    {
        [CustomControl(typeof(ProgressBarControl))]
        [ControlOptions(height: 15)]
        public ProgressVM Progress { get; set; }

        [Title("Start/Stop")]
        [ControlOptions(width: 50)]
        public Action StartStopAction { get; set; }

        private bool m_IsStarted;

        private readonly IXApplication m_App;
        private CancellationTokenSource m_CurrentJobCancellationTokenSource;

        public PMPage() 
        {
        }

        public PMPage(IXApplication app) 
        {
            m_App = app;
            Progress = new ProgressVM();
            StartStopAction = new Action(StartStopProgress);
        }

        private async void StartStopProgress() 
        {
            try
            {
                m_IsStarted = !m_IsStarted;

                if (m_IsStarted)
                {
                    m_CurrentJobCancellationTokenSource = new CancellationTokenSource();
                    var token = m_CurrentJobCancellationTokenSource.Token;

                    using (var appPrg = m_App.CreateProgress())
                    {
                        appPrg.SetStatus("Doing work...");

                        for (int i = 0; i < 100; i++)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(0.5), token);

                            token.ThrowIfCancellationRequested();

                            var prg = (i + 1) / 100d;
                            Progress.Progress = prg;
                            appPrg.Report(prg);
                        }
                    }
                }
                else
                {
                    m_CurrentJobCancellationTokenSource.Cancel();
                }
            }
            catch (TaskCanceledException)
            {
                m_App.ShowMessageBox("Work cancelled", MessageBoxIcon_e.Info);
            }
            catch (Exception) 
            {
                //handling as this function is async void and exception will be swallowed
            }
            finally
            {
                Progress.Progress = 0.0;
            }
        }
    }
}
