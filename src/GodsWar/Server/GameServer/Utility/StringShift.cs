using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utility
{
    public class StringShift
    {
        static int[] key_shift = new int[] { 0, 1, -1, -3, 2, 7, 1, 2, -2, 7, 2, 4, 1, 2, 7, 23, 2, 1, 2, 7, 1, 2, 2, 4, 2, 7, 15, -2, 2, 1, 11 };
        static int[] key_shifi = new int[] { 0, -1, -3, 3, -2, 3, -1, -2, 4, 3, -2, 2, -1, -2, 3, 3, -2, -1, -2, 3, -1, 0, 0, 2, -2, 3, -5, -6, -2, -1, 1};
        static char[] key_f = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] key_i = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public string Parser(string dt)
        {
            try
            {
                string tmpdata = dt.Replace("\u0000", System.String.Empty);
                string fulldata = "";

                int lng = tmpdata.Length;
                foreach (char dd in tmpdata)
                {
                    int number = 0;
                    bool isnumber = false;
                    try
                    {
                        number = int.Parse(dd + "");
                        isnumber = true;
                    }
                    catch { isnumber = false; }

                    if (!isnumber)
                    {
                        int pos = 0;
                        bool found = false;
                        for (int x = 0; x < key_f.Length; x++)
                        {
                            if (dd == key_f[x])
                            {
                                pos = x;
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            int key = key_shift[lng];
                            int bf = key_f.Length - pos;
                            char nwt = ' ';
                            if ((bf - key) <= 0)
                            {
                                int find = bf - key;
                                if (find < 0)
                                {
                                    int nff = Math.Abs(find);
                                    nwt = key_f[nff];
                                }
                                else
                                {
                                    nwt = key_f[find];
                                }
                            }
                            else
                            {
                                nwt = key_f[pos + key];
                            }
                            fulldata += nwt;
                        }
                        else
                        {
                            fulldata += dd;
                        }
                    }
                    else
                    {
                        int pos = 0;
                        for (int x = 0; x < key_i.Length; x++)
                        {
                            if (dd == key_i[x])
                            {
                                pos = x;
                                break;
                            }
                        }
                        int key = key_shifi[lng];
                        int bf = key_i.Length - pos;
                        char nwt = ' ';
                        if ((bf - key) <= 0)
                        {
                            int find = bf - key;
                            if (find < 0)
                            {
                                int nff = Math.Abs(find);
                                nwt = key_i[nff];
                            }
                            else
                            {
                                nwt = key_i[find];
                            }
                        }
                        else
                        {
                            if ((pos + key) < 0)
                            {
                                int nff = Math.Abs(pos + key);
                                nwt = key_i[nff];
                            }
                            else
                            {
                                nwt = key_i[pos + key];
                            }
                        }
                        fulldata += nwt;
                    }
                }
                return fulldata;
            }
            catch (Exception ep)
            {
                LogDebug.Show(ep.ToString());
            }
            return dt;
        }
    }
}
