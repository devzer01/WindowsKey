using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace WindowsKey
{
    class Winx
    {
        public static string LicenseCDKey
        {
            get
            {
                try
                {

                    byte[] rpk = (byte[])Registry.LocalMachine
                       .OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion")
                       .GetValue("DigitalProductId");
                    string serial = "";
                    const string possible = "BCDFGHJKMPQRTVWXY2346789";
                    for (int i = 0; i < 25; i++)
                    {
                        int accu = 0;
                        for (int a = 0; a < 15; a++)
                        {
                            accu <<= 8;
                            accu += rpk[66 - a];
                            rpk[66 - a] = (byte)(accu / 24 & 0xff);
                            accu %= 24;
                        }
                        serial = possible[accu] + serial;
                        if (i % 5 == 4 && i < 24)
                        {
                            serial = "-" + serial;
                        }
                    }
                    return serial;
                }
                catch
                {
                    return "Error";
                }
            }
        }

        public static string LicenseCDKey64
        {
            get
            {
                try
                {
                    RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                          RegistryView.Registry64);

                    string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
                    byte[] rpk = (byte[])key.OpenSubKey(keyPath).GetValue("DigitalProductId");

                    string serial = "";
                    const string possible = "BCDFGHJKMPQRTVWXY2346789";
                    for (int i = 0; i < 25; i++)
                    {
                        int accu = 0;
                        for (int a = 0; a < 15; a++)
                        {
                            accu <<= 8;
                            accu += rpk[66 - a];
                            rpk[66 - a] = (byte)(accu / 24 & 0xff);
                            accu %= 24;
                        }
                        serial = possible[accu] + serial;
                        if (i % 5 == 4 && i < 24)
                        {
                            serial = "-" + serial;
                        }
                    }
                    return serial;
                }
                catch
                {
                    return "Error 64";
                }
            }
        }
    }
}
