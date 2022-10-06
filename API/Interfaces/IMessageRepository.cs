using API.DTOs;
using API.Helpers;
using API.Model;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessagesThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();


    }
}