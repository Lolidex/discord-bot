using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kẍaa.Modules
{
    internal class Search
    {
        [Command("Search")]
        public async Task SearchFct(CommandContext ctx, params string[] arg)
        {
            var identificator = new Identificator.Identificator();
            string name = String.Join(" ", arg);
            string[] tags = (await identificator.CorrectName(name)).ToArray();
            bool wrongName = !tags.Contains(name.ToLower());
            if (tags.Length == 0)
                await ctx.RespondAsync("I don't know anyone with this tag.");
            else if (tags.Length > 1 && wrongName)
                await ctx.RespondAsync("I don't know anyone with this tag, here are some suggestions: `" + String.Join(", ", tags) + "`");
            else
            {
                if (wrongName)
                    name = tags[0];
                await ctx.RespondAsync("Please wait, this can take some time.");
                DateTime start = DateTime.Now;
                var result = await identificator.GetAnime(name);
                TimeSpan res = DateTime.Now.Subtract(start);
                BooruSharp.Search.Post.SearchResult? image;
                try
                {
                    image = await new BooruSharp.Booru.Safebooru().GetRandomImage("solo", "1girl", name, result.eyesColor + "_eyes", result.hairColor + "_hair");
                }
                catch (BooruSharp.Search.InvalidTags)
                {
                    image = null;
                }
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                {
                    Title = name,
                    Color = DiscordColor.Green
                };
                if (image.HasValue)
                    embed.ImageUrl = image.Value.fileUrl.AbsolutePath;
                if (result.eyesColor != null)
                    embed.AddField("Eyes Color", result.eyesColor);
                if (result.hairColor != null)
                    embed.AddField("Hair Color", result.hairColor);
                if (result.source != null)
                    embed.AddField("Source", result.source);
                embed.WithFooter("Request get in " + res.TotalSeconds + " seconds.");
                await ctx.RespondAsync("", false, embed.Build());
            }
        }
    }
}
