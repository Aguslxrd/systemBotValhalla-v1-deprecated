using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaBOT.SlashCommands
{
    public class UtilsSlashCommands : ApplicationCommandModule
    {

        [SlashCommand("alerta","Este comando envia una alerta al canal en el que estas - !alerta <msg>")]
        public async Task ipSlashCommands(InteractionContext ctx, [Option("string", "Escribe lo que quieras")] string text)
        {

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("Indexando comando..."));   //Sin esto no envia el mensaje del /

            var sendIpSlashCommands = new DiscordEmbedBuilder()
            {
                Title = text
            };

            await ctx.Channel.SendMessageAsync(embed : sendIpSlashCommands);

        }//Fin IP

    };



}
