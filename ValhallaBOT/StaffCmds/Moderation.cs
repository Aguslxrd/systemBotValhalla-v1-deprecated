using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaBOT.StaffCmds 
{
    class Moderation : ApplicationCommandModule
    {

        [SlashCommand("ban", "Banea a un usuario especifico en el servidor")]
        public async Task Ban(InteractionContext ctx, [Option("Usuario", "El usuario al cual quieres banear")] DiscordUser usuario,
                                                      [Option("Razon", "El usuario al cual quieres banear")] string razon = null)
        {
            await ctx.DeferAsync();
            if(ctx.Member.Permissions.HasPermission(Permissions.Administrator)) //Se puede cambiar por el de ban
            {
                var member = (DiscordMember)usuario; //Agregamos usuario al member sino no pdemos usar
                await ctx.Guild.BanMemberAsync(member, 0, razon);

                var banMessage = new DiscordEmbedBuilder()
                {
                    Title = "Usuario baneado- >" + member.Username,
                    Description = "Razon: " + razon,
                    Color = DiscordColor.Red
                    };
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(banMessage));
            }
            else
            {
                var messageNoPermsToBan = new DiscordEmbedBuilder()
                {
                    Title = "Acceso denegado ",
                    Description = "No eres administrador para ejecutar este comando",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(messageNoPermsToBan));

            }

        }
        [SlashCommand("kick", "Kickea a un usuario del servidor")]
        public async Task Kick(InteractionContext ctx, [Option("Usuario", "El usuario al cual quieres banear")] DiscordUser usuario)
        {
            await ctx.DeferAsync();
            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator)) //Se puede cambiar por el de ban
            {
                await ctx.DeferAsync();
                if (ctx.Member.Permissions.HasPermission(Permissions.Administrator))
                {
                    var member = (DiscordMember)usuario;
                    await member.RemoveAsync();

                    var kickMessage = new DiscordEmbedBuilder()
                    {
                        Title = member.Username + " Fue kickeado del servidor",
                        //Description = "Kickeado por: " + ctx.User.Username,
                        Color = DiscordColor.Red
                    };
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(kickMessage));

                }
                else
                {

                    var messageNoPermsToBan = new DiscordEmbedBuilder()
                    {
                        Title = "Acceso denegado ",
                        Description = "No eres administrador para ejecutar este comando",
                        Color = DiscordColor.Red
                    };

                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(messageNoPermsToBan));

                }
            }
        }
        [SlashCommand("mute", "Mutea a un usuario del servidor")]
        public async Task mute(InteractionContext ctx, [Option("Usuario", "El usuario al cual quieres mutear")] DiscordUser usuario,
                                          [Option("duracion", "El usuario al cual quieres banear")] long duracion = 0)
        {
            await ctx.DeferAsync();
            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator))
            {
                var TimeDuration = DateTime.Now + TimeSpan.FromSeconds(duracion);
                var member = (DiscordMember)usuario;
                await member.TimeoutAsync(TimeDuration);

                var timeoutMessage = new DiscordEmbedBuilder()
                {
                    Title = member.Username + " Ha sido muteado",
                    Description = "Duracion " + TimeSpan.FromSeconds(duracion).ToString()
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(timeoutMessage));

            }
            else
            {
                var messageNoPermsToMute = new DiscordEmbedBuilder()
                {
                    Title = "Acceso denegado ",
                    Description = "No eres administrador para ejecutar este comando",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(messageNoPermsToMute));

            }
        }//

    }
}
