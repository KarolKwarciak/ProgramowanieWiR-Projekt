using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

class WorkerNode
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        Console.WriteLine("Połączono z Masterem.");

        byte[] buffer = new byte[1024];
        int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
        string[] data = Encoding.ASCII.GetString(buffer, 0, bytesRead).Split('|');

        string range = data[0];   
        string targetHash = data[1]; 
        
        char startChar = range.Split('-')[0][0];
        char endChar = range.Split('-')[1][0];

        Console.WriteLine($"Rozpoczynam pracę nad zakresem: {startChar} do {endChar}");

        var alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        bool found = false;

        Parallel.ForEach(alphabet, (c1, state) =>
        {
            if (c1 < startChar || c1 > endChar) return;

            foreach (char c2 in alphabet)
            {
                foreach (char c3 in alphabet)
                {
                    if (found) state.Stop();

                    string attempt = $"{c1}{c2}{c3}";
                    if (ComputeMD5(attempt) == targetHash)
                    {
                        Console.WriteLine($"ZNALAZŁEM: {attempt}");
                        byte[] result = Encoding.ASCII.GetBytes("FOUND:" + attempt);
                        client.GetStream().Write(result, 0, result.Length);
                        found = true;
                        state.Stop();
                    }
                }
            }
        });

        Console.WriteLine("Praca zakończona. Naciśnij klawisz...");
        Console.ReadKey();
    }

    static string ComputeMD5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}