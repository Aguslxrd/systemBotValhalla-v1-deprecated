using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaBOT.Commands
{
    public class GeneralCommands : BaseCommandModule
    {

        /*
        [Command("calc")]
        public async Task Calculator(CommandContext ctx, int number1, int number2)
        {
            int answer = number1 + number2;
            await ctx.Channel.SendMessageAsync(answer.ToString());

        }
        */
        [Command("ip")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        [Description("Comando para ver la ip del servidor")]
        public async Task sendIpEmbed(CommandContext ctx)
        {
            var embedIPMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("ValhallaClub")
                .WithDescription("IP: mc.ValhallaClub.me")
                .WithColor(DiscordColor.Gold)
                );


            await ctx.RespondAsync(embedIPMessage); //Te responde el mensaje
            //await ctx.Channel.SendMessageAsync(embedIPMessage); //Solo envia el mensaje


        }
        //
        [Command("redes")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        [Description("Comando para ver las redes del servidor")]
        public async Task sendRedesEmbed(CommandContext ctx)
        {
            var embedSendRedes = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Valhalla")
                .AddField("Web: ", "https://www.valhallaclub.me" + " | ") //Si se le agrega ,true al final quedan en fila
                .AddField("Tienda: ", "https://store.valhallaclub.me" + " | ")
                .AddField("TikTok: ", "https://www.tiktok.com/@valhallatheserver" + " | ")
                .AddField("Twitter: ", "https://twitter.com/server_Valhalla" + " | ")
                .AddField("Afiliados: ", "https://www.valhallaclub.me/afiliados" + " | ")


                .WithColor(DiscordColor.Gold)
                );
            await ctx.RespondAsync(embedSendRedes);
        }

        
        [Command("poll")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        [RequireRoles(RoleCheckMode.MatchNames, "Owner")]
        public async Task PollCommand(CommandContext ctx, int TimeLimit, string Option1, string Option2, string Option3, string Option4, params string[] Question)
        {

            try { 
                
            
            var interactivity = ctx.Client.GetInteractivity();
            TimeSpan timer = TimeSpan.FromSeconds(TimeLimit); //cuando terminara

            DiscordEmoji[] optionEmojis = {DiscordEmoji.FromName(ctx.Client,":one:",false), //se agregan los emojis al msg
                                           DiscordEmoji.FromName(ctx.Client,":two:",false),
                                           DiscordEmoji.FromName(ctx.Client,":three:",false),
                                           DiscordEmoji.FromName(ctx.Client,":four:",false)};

            string optionsString = optionEmojis[0] + " | " + Option1 + "\n" + //se agregan los emojis al msg
                                   optionEmojis[1] + " | " + Option2 + "\n" +
                                   optionEmojis[2] + " | " + Option3 + "\n" +
                                   optionEmojis[3] + " | " + Option4;

            var pollMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle(string.Join("", Question))
                .WithDescription(optionsString)
                .WithColor(DiscordColor.Gold)
                     );
            //codigo para guardar las reacciones
            var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage); //putreacton guarda las reacciones
            foreach(var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }
            //codigo para guardar las reacciones END
            //
            var result = await interactivity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach(var emoji in result)   //Se fija si estan checked
            {
                if (emoji.Emoji == optionEmojis[0])
                {
                    count1++;
                }//1
                if (emoji.Emoji == optionEmojis[1])
                {
                    count2++;
                }//2
                if (emoji.Emoji == optionEmojis[2])
                {
                    count2++;
                }//3
                if (emoji.Emoji == optionEmojis[3])
                {
                    count3++;
                }//4
                //fin foreach

            }
            int totalVotes = count1 + count2 + count3 + count4;
            //
            string resultsOfString = optionEmojis[0] + " | " + Option1 + " | " + count1 + " Votos \n" + //Muestra los resultados de la encuesta
                                   optionEmojis[1] + " | " + Option2 + " | " + count2 + " Votos\n" +
                                   optionEmojis[2] + " | " + Option3 + " | " + count3 + " Votos\n" +
                                   optionEmojis[3] + " | " + Option4 + " | " + count4 + " Votos\n\n" +
                                   "Votos totales: " + totalVotes;
            //
            var resultsMessage = new DiscordMessageBuilder()
            .AddEmbed(new DiscordEmbedBuilder()
            .WithColor(DiscordColor.Gold)
            .WithTitle("Resultados de la encuesta: ")
            .WithDescription(resultsOfString)
            );
            await ctx.Channel.SendMessageAsync(resultsMessage);
                }
                catch (Exception ex)
            {
                var errorMsg = new DiscordEmbedBuilder()
                {

                    Title = "(Error?) - > Revisa la sintaxis o contacta con un developer",
                    Description = ex.Message,
                    Color = DiscordColor.Red

                };
            }


            }
        [Command("status")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        [RequireRoles(RoleCheckMode.MatchNames, "Owner")]  // Requiere este rol sino no lo dejara usarlo
        [Description("Comando para ver el estado de la maquina y sus servidores activos")]
        public async Task sendStatusEmbed(CommandContext ctx)
        {
            if (ctx.Guild.Id == 805650345061122098) //GUILD ES SERVIDOR PUEDE SER CHANNEL ETC, es por id
            {
    
                            var embedStatusMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("Status - - > ValhallaClub")
                .WithDescription("IP: mc.ValhallaClub.me")
                .AddField("Bungeecord: ", "webHook de la ram del Bungee")
                .AddField("Lobby: ", "webHook de la ram del lobby")
                .AddField("vClans: ", "webHook de la ram de vClans")
                .WithColor(DiscordColor.Gold)
                );

                await ctx.RespondAsync(embedStatusMessage);

            }
            else
            {
                await ctx.RespondAsync("Este comando no puede ser ejecutado en este servidor.");

            }

        }
        [Command("survey")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        [RequireRoles(RoleCheckMode.MatchNames, "Owner")]
        public async Task SurveyCommands(CommandContext ctx, [Option("string", "Escribe lo que quieras")] string text)
        {

           
            /// HACER LA LOGICA PARA LA ENCUESTA CON BOTONES DE SI Y NO


        }

        [Command("info")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task devCommands(CommandContext ctx)
        {

                 var embedSenddevMSG = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("ValhallaClub")
                .AddField("Developed by : ", "Aguslxrd#0410" + " | ") //Si se le agrega ,true al final quedan en fila
                .AddField("All Rights Reserved to : ", "DevClub" + " | ") //Si se le agrega ,true al final quedan en fila
                .WithColor(DiscordColor.Gold)
                );
            await ctx.RespondAsync(embedSenddevMSG);

        }
        //
        [Command("ayuda")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task infoCommands(CommandContext ctx)
        {

            var embedSendInfoMSG = new DiscordMessageBuilder()
           .AddEmbed(new DiscordEmbedBuilder()
           .WithTitle("ValhallaClub")
                .AddField("Lee las reglas en: ", "#Rules " + " | ") 
                .AddField("Nuestras redes con: ", "!redes" + " | ")
                .AddField("Entra en nuestro servidor: ", "!ip" + " | ")
           .WithColor(DiscordColor.Gold)
           );
            await ctx.RespondAsync(embedSendInfoMSG);

        }
        //Status de playing/jugando
        [Command("setactivity")]
        public async Task setactivity(CommandContext ctx)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator))
            {
                DiscordActivity activity = new DiscordActivity();
                DiscordClient discord = ctx.Client;
                //input = Console.ReadLine();
                activity.Name = "Yggdrasil";
                await discord.UpdateStatusAsync(activity);
                await ctx.RespondAsync("Actividad establecida!");

                return;
            }
            else 
            {
                await ctx.RespondAsync("No tienes permiso para ejecutar este comando.");
            }
        }
        /*[Command("senddm")]           //CODIGO PARA ENVIAR DMS
        public async Task SendDMExample(CommandContext ctx)
        {
            var DMChannel = await ctx.Member.CreateDmChannelAsync();
            await DMChannel.SendMessageAsync("Test");
        }*/



    }

}
