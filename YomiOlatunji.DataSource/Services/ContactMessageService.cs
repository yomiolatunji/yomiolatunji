using YomiOlatunji.Core;
using YomiOlatunji.Core.DbModel.Post;
using YomiOlatunji.DataSource.Interface;
using YomiOlatunji.DataSource.Services.Interfaces;

namespace YomiOlatunji.DataSource.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IContactMessageRepository _repository;

        public ContactMessageService(IContactMessageRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddMessage(ContactMessage message)
        {
            //TODO: send notification as mail
            _repository.Add(message);
            return await _repository.SaveChanges();
        }

        public async Task<IList<ContactMessage>> GetAllContactMessages()
        {
            return (await _repository.Get()).ToList();
        }

        public async Task<PageModel<ContactMessage>> GetContactMessages(int intPageIndex = 1, int intPageSize = 20)
        {
            return await _repository.GetPage(null,intPageIndex,intPageSize);
            
        }
    }
}
