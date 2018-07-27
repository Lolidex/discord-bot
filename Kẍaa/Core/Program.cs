using DSharpPlus;
using System.IO;
using System.Threading.Tasks;

namespace Kẍaa.Core
{
    class Program
    {
        static void Main(string[] args)
            => new Program().RunAsync().Wait();

        private DiscordClient _client;

        public Program()
        {
            _client = new DiscordClient(new DiscordConfiguration()
            {
                AutoReconnect = true,
                EnableCompression = true,
                LogLevel = LogLevel.Debug,
                Token = File.ReadAllText("Keys/token.dat"),
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true
            });
        }

        public async Task RunAsync()
        {
            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
