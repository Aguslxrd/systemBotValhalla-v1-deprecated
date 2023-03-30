using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ValhallaBOT.Commands;
using ValhallaBOT.SlashCommands;
using ValhallaBOT.StaffCmds;
using ValhallaBOT.StreamersNotifys;

namespace ValhallaBOT
{
    public class bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }

        public CommandsNextExtension Commands { get; private set; }

        private YTVideoAlert _video = new YTVideoAlert();
        private YTVideoAlert temp = new YTVideoAlert();
        private YTEngine _YTEngine = new YTEngine();

        public async Task RunAsync()
        {

            //config
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJSON = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJSON.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                //UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });
            //

            //Client.ComponentInteractionCreated += ButtonPressResponse;

            //COMANDOS
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new String[] { configJSON.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };
            //DECLARACIONES DE COMANDOS

            //comandos generales
            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<GeneralCommands>(); //FunCommands
            Commands.CommandErrored += OnCommandError;
            //slashCommands
            var slashCommandsConfig = Client.UseSlashCommands();
            slashCommandsConfig.RegisterCommands<UtilsSlashCommands>(805650345061122098);
            slashCommandsConfig.RegisterCommands<Moderation>(805650345061122098);
            



            //conexion para el bot online.


            await Client.ConnectAsync();
            await Task.Delay(-1);
            ulong channelIdToNotify = 974383829575933992; //Discord channel ID
            await VideoNotifierTimer(_YTEngine.channelId, _YTEngine.apiKey, Client, channelIdToNotify);
            await Task.Delay(-1);
        }
        //////////////////////////COOLDOWN
        private async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e) //metodo para mensaje de cooldown
        {
            if (e.Exception is ChecksFailedException)
            {
                var castedException = (ChecksFailedException)e.Exception;
                string cooldownTimer = string.Empty;

                foreach (var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute)check;

                    TimeSpan timeleft = cooldown.GetRemainingCooldown(e.Context);
                    cooldownTimer = timeleft.ToString(@"hh\:mm\:ss");
                }

                var cooldownMessage = new DiscordEmbedBuilder()
                {

                    Title = "Comando en cooldown",
                    Description = "Tiempo restante: " + cooldownTimer,
                    Color = DiscordColor.Red
                };
                await e.Context.Channel.SendMessageAsync(embed: cooldownMessage);
            }
        }
        
        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        //////////////////////////COOLDOWN
        ///

        /*private async Task ButtonPressResponse(DiscordClient sender, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            if (e.Interaction.Data.CustomId == "IpButton")
            {
                string ipcmdsend = "IP: mc.valhallaclub.me";
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent(ipcmdsend));
            //
            }else if(e.Interaction.Data.CustomId == "webButton")
            {
                string webcmdsend = "https://www.valhallaclub.me";
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent(webcmdsend));
            //
            }else if(e.Interaction.Data.CustomId == "tiendaButton")
            {
                string tiendacmdsend = "https://store.valhallaclub.me";

                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent(tiendacmdsend));
            }

        }//task async*/
        private async Task VideoNotifierTimer(string channelId, string apiKey, DiscordClient client, ulong channelIdToNotify)
        {
            var timer = new Timer(900000); //15 min
            timer.Elapsed += async (sender, e) =>
            {
                _video = _YTEngine.GetLatestVideo(channelId, apiKey);
                var lastCheckedAt = DateTime.Now;//Guarda cuando fue el ultimo check
                if(_video != null)
                {
                    if(_video.PublishedVideoAt < lastCheckedAt)
                    {
                        var message = $"Nuevo video | **{_video.videoTitle}** \n" +
                                     $"Publicado hace: {_video.PublishedVideoAt} \n" +
                                     "URL: " + _video.videoUrl;

                        await client.GetChannelAsync(channelIdToNotify).Result.SendMessageAsync(message);
                        temp = _video;
                    }
                    else
                    {
                        Console.WriteLine("[vEngine] - - > Currently no new videos were found.");
                    }
                }
            };
            timer.Start();

        }



    }


}
