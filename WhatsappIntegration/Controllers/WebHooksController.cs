using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;
using WhatsappIntegration.Utility;

namespace WhatsappIntegration.WebUI.Controllers
{
    //https://www.twilio.com/console/sms/whatsapp/sandbox -> In this url you have to fill : 'WHEN A MESSAGE COMES IN' and 'STATUS CALLBACK URL'  
    [Authorize]
    public class WebHooksController : TwilioController
    {
        private IUnitOfWork unitOfWork;
        private IConfiguration Configuration { get; }
        private readonly UserManager<UserAgent> userManager;
        public WebHooksController(IUnitOfWork _unitOfWork, IConfiguration _configuration, UserManager<UserAgent> _userManager)
        {
            unitOfWork = _unitOfWork;
            Configuration = _configuration;
            userManager = _userManager;
        }
        int chatId;
        [HttpPost]
        //When Message comes from Twilio this method will be fired. [site url]/Webhooks/Index
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            SaveIncomingWhatsappMessage(incomingMessage);
            var response = SmartReply(incomingMessage);
            if (response != null)
            {
                var messagingResponse = new MessagingResponse();
                messagingResponse.Append(response);
                SaveSmartReplyMessage(response.BodyAttribute);

                return TwiML(messagingResponse);
                
            }
            return null;
        }

        private void SaveSmartReplyMessage(string message)
        {
            ChatMessages smartMessage = new ChatMessages()
            {
                AnsweringUserId = Enums.SmartReplyAgentId,
                ChatId = chatId,
                Message = message,
                MessageDate = DateTime.Now,
                MessageDirection = Enums.ChatMessageFromUserAgent,
                IsItRead = false,
            };
            unitOfWork.ChatMessages.Insert(smartMessage);
            unitOfWork.SaveChanges();

        }

        [HttpPost]
        //When we send message to Twilio,They will return message status to us. [site url]/Webhooks/StatusCallBack
        public ActionResult StatusCallBack(SmsRequest request)
        {
            var smsSid = request.SmsSid;
            var messageStatus = request.MessageStatus;
            var result = unitOfWork.ChatMessages.Find(f => f.TwilioMessageId == smsSid).FirstOrDefault();
            if (result != null)
            {
                result.TwilioMessageStatus = messageStatus;
                unitOfWork.ChatMessages.Update(result);
                unitOfWork.SaveChanges();
            }

            return null;
        }

        public void SaveIncomingWhatsappMessage(SmsRequest incomingMessage)
        {
            incomingMessage.To = incomingMessage.To.Replace("whatsapp:", "");
            incomingMessage.From = incomingMessage.From.Replace("whatsapp:", "");
            var result = unitOfWork.ChatTypes.Find(o => o.ChatTypeIdentity == incomingMessage.To).FirstOrDefault();
            if (result != null)
            {
                var isUserChatExist = unitOfWork.Chat.Find(f => f.PhoneNumber == incomingMessage.From).FirstOrDefault();
                chatId = isUserChatExist.ChatId;
                if (isUserChatExist == null)
                {
                    //Insert Chat
                    Chat chat = new Chat()
                    {
                        CompanyId = result.CompanyId,
                        ChatTypeId = result.ChatTypeId,
                        CreationDate = DateTime.Now,
                        PhoneNumber = incomingMessage.From,
                        SmartReplyState = true,
                        IsDeleted = false
                    };
                    unitOfWork.Chat.Insert(chat);
                    unitOfWork.SaveChanges();
                    chatId = chat.ChatId;
                }
                //Insert ChatMessages
                ChatMessages messages = new ChatMessages()
                {
                    ChatId = chatId,
                    MessageDirection = Enums.ChatMessageFromClient,
                    Message = incomingMessage.Body,
                    MessageDate = DateTime.Now,
                    IsItRead = false,
                    TwilioMessageId = incomingMessage.SmsSid,
                };
                unitOfWork.ChatMessages.Insert(messages);
                unitOfWork.SaveChanges();
            }
        }

        public async Task SendOutgoingWhatsappMessage(string message, int chatId)
        {
            var agent = await userManager.GetUserAsync(User);
            if (agent!= null)
            {
                var chat = unitOfWork.Chat.Find(f => f.ChatId == chatId && f.CompanyId == agent.CompanyId).FirstOrDefault();
                if (chat != null)
                {
                    var from = unitOfWork.ChatTypes.Find(f => f.CompanyId == chat.CompanyId && f.ChatType == "Whatsapp").FirstOrDefault().ChatTypeIdentity;
                    TwilioClient.Init(Configuration["TwilioAccountSId"], Configuration["TwilioAuthToken"]);

                    var sendMessage = MessageResource.Create(
                        body: message,
                        from: new Twilio.Types.PhoneNumber(from),
                        //statusCallback: new Uri("http://postb.in/1234abcd"),
                        to: new Twilio.Types.PhoneNumber(chat.PhoneNumber)
                    );
                    var a = sendMessage.Sid;
                }
            }
        }

        public void SaveOutgoingWhatsappMessage()
        {

        }

        public Message SmartReply(SmsRequest incomingMessage)
        {
            int companyId = FindCompanyId(incomingMessage.To);
            var chat = unitOfWork.Chat.Find(f => f.PhoneNumber == incomingMessage.From && f.CompanyId == companyId).FirstOrDefault();
            if (chat.SmartReplyState = Enums.SmartReplyActive)
            {
                var smartReplies = unitOfWork.SmartReply.Find(w => w.CompanyId == companyId);
                if (smartReplies != null)
                {
                    foreach (var item in smartReplies)
                    {
                        if (item.FullMatchOrContains == Enums.SmartReplyFullMatch)
                        {
                            if (item.Keyword.ToLower().Equals(incomingMessage.Body.ToLower()))
                            {

                                return new Message(item.Answer);
                            }
                        }
                        else if (item.FullMatchOrContains == Enums.SmartReplyContains)
                        {
                            if (item.Keyword.ToLower().Contains(incomingMessage.Body.ToLower()))
                            {
                                return new Message(item.Answer);
                            }
                        }
                    }
                }
            }
            return null;
        }

        public int FindCompanyId(string phone)
        {
            phone = phone.Replace("whatsapp:", "");
            var result = unitOfWork.ChatTypes.Find(o => o.ChatTypeIdentity == phone).FirstOrDefault();
            if (result != null)
            {
                return result.CompanyId;
            }
            return -1;
        }
    }
}