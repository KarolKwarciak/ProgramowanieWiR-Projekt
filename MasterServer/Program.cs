using System.Net;
using System.Net.Sockets;
using System.Text;

class MasterServer
{
    static string targetHash = "900150983cd24fb0d6963f7d28e17f72"; 
    static string[] ranges = { "a-i", "j-r", "s-z" }; 
    static int currentRangeIndex = 0;

    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine(">>> Master Server uruchomiony. Czekam na Workerów...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine(">>> Worker dołączył!");

            if (currentRangeIndex < ranges.Length)
            {
                string rangeToSend = ranges[currentRangeIndex] + "|" + targetHash;
                byte[] data = Encoding.ASCII.GetBytes(rangeToSend);
                client.GetStream().Write(data, 0, data.Length);
                currentRangeIndex++;

                Task.Run(() => ListenForSolution(client));
            }
        }
    }

    static void ListenForSolution(TcpClient client)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
        string result = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        if (result.StartsWith("FOUND:"))
        {
            Console.WriteLine("\n************************************");
            Console.WriteLine($"SUKCES! Hasło odnalezione: {result.Substring(6)}");
            Console.WriteLine("************************************");
        }
    }
}