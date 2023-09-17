#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;

#endregion

namespace RAA_M04C
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {

            // step 1: create ribbon tab
            try
            {
                a.CreateRibbonTab("Revit Add-in Bootcamp");
            }
            catch (Exception)
            {
                Debug.Print("Tab already exists");
            }


            // step 2: create ribbon panel
            RibbonPanel curPanel = CreateRibbonPanel(a, "Revit Add-in Bootcamp", "Revit Tools");



            // Step 3: Create button data instances
            PushButtonData pData1 = new PushButtonData("Button1", "Tool1", GetAssemblyName(), "RAA_M04C.CmdTool1");
            PushButtonData pData2 = new PushButtonData("Button2", "Tool2", GetAssemblyName(), "RAA_M04C.CmdTool2");

            PushButtonData pData3 = new PushButtonData("Button3", "Tool3", GetAssemblyName(), "RAA_M04C.CmdTool3");
            PushButtonData pData4 = new PushButtonData("Button4", "Tool4", GetAssemblyName(), "RAA_M04C.CmdTool4");
            PushButtonData pData5 = new PushButtonData("Button5", "Tool5", GetAssemblyName(), "RAA_M04C.CmdTool5");

           
            PushButtonData pData6 = new PushButtonData("Button6", "Tool6", GetAssemblyName(), "RAA_M04C.CmdTool6");
            PushButtonData pData7 = new PushButtonData("Button7", "Tool7", GetAssemblyName(), "RAA_M04C.CmdTool7");

            SplitButtonData sData67 = new SplitButtonData("Button6", "Split Button");

            PushButtonData pData8 = new PushButtonData("Button8", "Tool8", GetAssemblyName(), "RAA_M04C.CmdTool8");
            PushButtonData pData9 = new PushButtonData("Button9", "Tool9", GetAssemblyName(), "RAA_M04C.CmdTool9");
            PushButtonData pData10 = new PushButtonData("Button10", "Tool10", GetAssemblyName(), "RAA_M04C.CmdTool10");

            PulldownButtonData pdData8910 = new PulldownButtonData("Button8", "More Tools");


            // Step 4: Add images
            pData1.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Blue_32);
            pData2.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_32);

            pData3.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Green_32);
            pData3.Image = BitmapToImageSource(RAA_M04C.Properties.Resources.Green_16);
            pData4.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Blue_32);
            pData4.Image = BitmapToImageSource(RAA_M04C.Properties.Resources.Blue_16);
            pData5.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_32);
            pData5.Image = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_16);

            pData6.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Yellow_32);
            pData7.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_32);


            pData8.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Blue_32);
            pData9.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Green_32);
            pData10.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_32);

            

            // Step 5: Add tooltip info
            pData1.ToolTip = "This is tool 1.";
            pData2.ToolTip = "This is tool 2.";
            pData3.ToolTip = "This is tool 3.";
            pData4.ToolTip = "This is tool 4.";
            pData5.ToolTip = "This is tool 5.";
            pData6.ToolTip = "This is tool 6.";
            pData7.ToolTip = "This is tool 7.";
            pData8.ToolTip = "This is tool 8.";
            pData9.ToolTip = "This is tool 9.";
            pData10.ToolTip = "This is tool 10.";


            // Step 6: Create buttons
            //6.1 Push Buttons
            PushButton b1 = curPanel.AddItem(pData1) as PushButton;
            PushButton b2 = curPanel.AddItem(pData2) as PushButton;
            

            //6.2 Stacked buttons
            curPanel.AddStackedItems(pData3, pData4, pData5);

            pData3.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Green_32);
            pData4.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Blue_32);
            pData5.LargeImage = BitmapToImageSource(RAA_M04C.Properties.Resources.Red_32);

            //6.3 Split Buttons
            SplitButton sButton = curPanel.AddItem(sData67) as SplitButton;

            sButton.AddPushButton(pData6);
            sButton.AddPushButton(pData7);

            //6.4 Pulldown buttons
            PulldownButton stButton = curPanel.AddItem(pdData8910) as PulldownButton;

            stButton.AddPushButton(pData8);
            stButton.AddPushButton(pData9);
            stButton.AddPushButton(pData10);


            return Result.Succeeded;
        }



        private RibbonPanel CreateRibbonPanel(UIControlledApplication a, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in a.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            RibbonPanel returnPanel = a.CreateRibbonPanel(tabName, panelName);

            return returnPanel;
        }

        private string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        private BitmapImage BitmapToImageSource(System.Drawing.Bitmap bm)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                bm.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();

                return bmi;
            }
        }
        //







        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }

    //External comand availability 
    public class CommandAvailability : IExternalCommandAvailability
    {
        //Interface member method
        public bool IsCommandAvailable(UIApplication app, CategorySet cate)
        {
            //is file share
            if (app.ActiveUIDocument.Document.IsWorkshared)
            {
                return true;
            }
            return false;
        }
    }
}
