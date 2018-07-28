using DSharpPlus;
using DSharpPlus.CommandsNext;
using Kẍaa.Modules;
using System.IO;
using System.Threading.Tasks;

namespace Kẍaa.Core
{
    class Program
    {
        static void Main(string[] args)
            => new Program().RunAsync().Wait();

        private DiscordClient _client;
        private CommandsNextModule _cnext;

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

            _cnext = _client.UseCommandsNext(new CommandsNextConfiguration()
            {
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableDms = true,
                EnableMentionPrefix = true,
                StringPrefix = "k.",
                IgnoreExtraArguments = true
            });
            _cnext.RegisterCommands<Search>();
        }

        public async Task RunAsync()
        {
            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
