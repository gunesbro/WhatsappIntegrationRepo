using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Concrete.EFCore;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.WebUI.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        IdentityContext context;
        private IUnitOfWork unitOfWork;
        public InboxController(IUnitOfWork _unitOfWork, IdentityContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        public IActionResult Index()
        {
            var result = context.VChatList.ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    item.UnreadMessageCount = unitOfWork.ChatMessages.GetUnreadMessageCount(item.ChatId);
                }
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public PartialViewResult ChatBox(int chatId, string person, string phone)
        {
            int take = 8;
            ViewBag.Person = person; ViewBag.Phone = phone; ViewBag.ChatId = chatId; ViewBag.Take = take;
            var chatMessages = unitOfWork.ChatMessages.GetAll()
                .Where(f => f.ChatId == chatId)
                .OrderByDescending(o => o.ChatMessageId)
                .Take(take)
                .ToList();
            unitOfWork.ChatMessages.SetUnreadToRead(chatId);
            return PartialView(chatMessages);
        }

        [HttpPost]
        public JsonResult SkipTakeMessages(int chatId, int skip, int take)
        {
            var chatMessages = unitOfWork.ChatMessages.GetAll()
                .Where(f => f.ChatId == chatId)
                .OrderByDescending(o => o.ChatMessageId)
                .Skip(skip)
                .Take(take)
                .ToList();

            return Json(chatMessages);
        }
    }
}