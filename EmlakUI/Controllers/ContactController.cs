using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                var messsage = await contactService.ReadInIPAddress(m => m.ipAddress == ipAddress);

                if (messsage != null)
                {
                    var time = DateTime.Now - messsage.MessageDate;
                    if (time.TotalMinutes < 5)
                    {
                        TempData["timeMessage"] = "Çox sıx mesaj göndərirsiniz! Zəhmət olmasa 5 dəqiqə sonra yenidən cəhd edin.";
                        return RedirectToAction(nameof(Index), "Contact");
                    }
                }
                var newMessage = new Contact()
                {
                    ipAddress = ipAddress,
                    Email = contact.Email,
                    Message = contact.Message,
                    Name = contact.Name,
                    Subject = contact.Subject,
                };
                await contactService.BL_CreateAsync(newMessage);
                TempData["successSendMessage"] = "Mesajınız qeydə alınmışdır! Ən qısa vaxtda sizə geri dönüş ediləcəkdir.";
                return RedirectToAction(nameof(Index), "Contact");
            }
            return RedirectToAction(nameof(Index), "Contact");
        }
    }
}
