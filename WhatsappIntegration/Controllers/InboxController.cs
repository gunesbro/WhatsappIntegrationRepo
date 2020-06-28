using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsappIntegration.DAL.Abstract;
using WhatsappIntegration.DAL.Concrete.EFCore;
using WhatsappIntegration.DAL.Context;
using WhatsappIntegration.Entity.Concrete;
using WhatsappIntegration.Utility;

namespace WhatsappIntegration.WebUI.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        IdentityContext context;
        private IUnitOfWork unitOfWork;
        private UserManager<UserAgent> userManager;
        public InboxController(IUnitOfWork _unitOfWork, IdentityContext _context, UserManager<UserAgent> _userManager)
        {
            unitOfWork = _unitOfWork;
            context = _context;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var user = await userManager.GetUserAsync(User);
            var result = unitOfWork.VChatList.Find(f=>f.CompanyId ==user.CompanyId);
            if (result != null)
            {
                //foreach (var item in result)
                //{
                //    item.UnreadMessageCount = unitOfWork.ChatMessages.GetUnreadMessageCount(item.ChatId);
                //}
                int pageSize = 2;
                return View(PaginatedList<VChatList>.Create(result.AsNoTracking(), pageNumber ?? 1, pageSize));
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

        [HttpPost]
        public JsonResult ChangeSmartReplyState(int chatId, bool state)
        {
            var chat = unitOfWork.Chat.Find(f => f.ChatId == chatId && f.SmartReplyState != state).FirstOrDefault();
            if (chat != null)
            {
                chat.SmartReplyState = state;
                unitOfWork.Chat.Update(chat);
                unitOfWork.SaveChanges();
                return Json(new { state = "success" });
            }
            return Json(NotFound());
        }
    }
}