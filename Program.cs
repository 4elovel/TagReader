using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    public static void getString(string h)
    {

        WebRequest request = WebRequest.Create("https://community.dynamics.com/blogs/post/?postid=889d38e7-4257-4554-9d49-40c0df5175ad");
        WebResponse response = request.GetResponse();
        List<string> print = new List<string>();
        bool leaver=false;
        string he = h.Insert(1, "/");
        he = he.Remove(he.Length - 1, 1);
        he += ">";
        using (Stream stream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string str = "";
                for (int i = 0; i < h.Length; i++)
                {
                    str += (char)reader.Read();
                }
                while (!reader.EndOfStream)
                {
                    string sbuf = "";
                    if (!leaver)
                    {
                        str = str[1..];
                        str += (char)reader.Read();
                    }
                    if (str == h)
                    {
                        leaver = true;
                        Console.Write(str);
                    }
                    if(leaver)
                    {
                        str = str[1..];
                        str += (char)reader.Read();
                        Console.Write(str.Last());
                        sbuf = str;
                        sbuf += ">";

                    }
                    if (sbuf == he)
                    {
                        leaver = false;
                        Console.Write(">");
                        Console.WriteLine();
                    }
                }

            }
        }
    }
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.WriteLine("Фрагменти сторінки, які починаються з");
        Console.WriteLine("h2: ");
        getString("<h2 ");
        getString("<h2>");
        Console.WriteLine("h3: ");
        getString("<h3 ");
        getString("<h3>");
    }
}