﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using RunTimeBot.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RunTimeBot.RootDialogs
{
    [Serializable]
    [LuisModel("240a288c-12cb-49a9-b794-c2f083e32d9c", "d7fea63a593245659e9c5be8085995c6")]
    public class RootDialogEWI : LuisDialog<object>
    {
        //Constructor
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        //None intent, all user messages not undertood by Luis will go to this intent.
        [LuisIntent("None")]
        public async Task noneIntent(IDialogContext context, IAwaitable<IMessageActivity> awaitableResult, LuisResult result)
        {
            /*string answer = Answers.getRandomAnswer(Answers.AnswersTypes.NONE);
            await context.SayAsync(answer);
            MyHttpRequests.SaveInNotUnderstoodIntent(result.Query);
            Util.storageInLogs(result.Query, 4, answer, DateTime.Now);
            context.Wait(MessageReceived);*/
            await context.SayAsync("Hallo, Ich Bin Quick Response Bot");
        }

        //Empty intent, when a message is understood in Luis, but does not exist an implementation on code.
        //The the result will come to the empty intent.
        [LuisIntent("")]
        public async Task emptyIntent(IDialogContext context, LuisResult result)
        {
            context.ConversationData.SetValue("EmptyLuisResult", result);
            context.ConversationData.SetValue("LuisType", "EWI");
            context.Call(new EmptyDialog(), ResumeAfter);
        }

        //Method called after the asyncronous execution of the dialogs.
        private async Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var res = await result;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                context.Wait(MessageReceived);
            }
        }

    }
}