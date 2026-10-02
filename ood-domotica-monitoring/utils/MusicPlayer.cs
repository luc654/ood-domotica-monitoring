namespace ood_domotica_monitoring;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class MusicPlayer
{
    private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");

    public void playSounds()
    {
        try
        {
            Type wmpType = Type.GetTypeFromProgID("WMPlayer.OCX.7");
            if (wmpType == null)
            {
                Program.GlobalContext.notification = "Unable to play sounds";
                return;
            }

            dynamic wmp = Activator.CreateInstance(wmpType);
            
            if (!Directory.Exists(path))
            {
                Program.GlobalContext.notification = "Sounds directory not found!";
                return;
            }

            List<string> soundFiles = Directory.GetFiles(path).ToList();
            
            if (soundFiles.Count == 0)
            {
                Program.GlobalContext.notification = "No sound files found in folder.";
                return;
            }

            terminalHelper helper = new terminalHelper();
            
            List<string> fileNamesOnly = soundFiles.Select(Path.GetFileName).ToList();
            int numberIndex = helper.handleTerminal(fileNamesOnly, "MUSIC PLAYER!", "Select a file, *.wav");
            
            if (numberIndex < 0 || numberIndex >= soundFiles.Count) return;

            
            string fullPath = soundFiles[numberIndex]; 
            
            wmp.URL = fullPath;


            return;
        }
        catch (Exception e)
        {
            Program.GlobalContext.notification = "Unable to play sounds " + e.Message;
            return;
        }
    }
}
