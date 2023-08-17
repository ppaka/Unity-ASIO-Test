using NAudio.Wave;
using UnityEngine;

public class Test : MonoBehaviour
{
    private AudioFileReader _file;
    private AsioOut _asioOut;
    
    private void Start()
    {
        if (!AsioOut.isSupported())
        {
            print("ASIO NOT SUPPORTED");
            return;
        }
        
        print("Finding ASIO Drivers");
        var names = AsioOut.GetDriverNames();
        
        foreach (var driverName in names)
        {
            print(driverName);
        }

        if (names.Length == 0)
        {
            print("CANNOT FIND DRIVERS");
            return;
        }
        
        var drvName = names[0];
        _asioOut = new AsioOut(drvName);
        using (_file = new AudioFileReader("FILENAME"))
        {
            _file.Volume = 0.05f;
            _asioOut.Init(_file);
        }
        
        print("Playing...");
        _asioOut.Play();
    }

    private void OnDestroy()
    {
        _file.Dispose();
        _asioOut.Stop();
        _asioOut.Dispose();
    }
}
