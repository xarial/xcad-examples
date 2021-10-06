# Progress Bar

This example demonstrates how to create and run progress bar from Property Manager page and the application.

![Progress bar](progress-bar.png)

* Open the **Progress Bar Example->Show Progress Page** command from the menu
* Click **Start/Stop** button. Background work will be emulated and progress bar will be updated accordingly in the Proeprty Manager page as well as the SOLIDWORKS' status bar (in the bottom left corner) and SOLIDWORKS application icon in the Windows Task bar
* Click **Start/Stop** to cancel the job.

> Note, this is a simple example and it does not consider race conditions. Use the required practices to enable proper concurrency.