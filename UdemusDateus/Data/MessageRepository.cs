using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using UdemusDateus.DTOs;
using UdemusDateus.Entities;
using UdemusDateus.Helpers;
using UdemusDateus.Interfaces;

namespace UdemusDateus.Data;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public MessageRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public void AddGroup(Group group)
    {
        _dataContext.Groups.Add(group);
    }

    public void RemoveConnection(Connection connection)
    {
        _dataContext.Connections.Remove(connection);
    }

    public async Task<Connection> GetConnection(string connectionId)
    {
        return await _dataContext.Connections.FindAsync(connectionId);
    }

    public async Task<Group> GetMessageGroup(string groupName)
    {
        return await _dataContext.Groups.Include(c => c.Connections).FirstOrDefaultAsync(g => g.Name == groupName);
    }

    public async Task<Group> GetGroupForConnection(string connectionId)
    {
        return await _dataContext.Groups
            .Include(c => c.Connections)
            .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId)).FirstOrDefaultAsync();
    }

    public void AddMessage(Message message)
    {
        _dataContext.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        _dataContext.Messages.Remove(message);
    }

    public async Task<Message> GetMessage(int id)
    {
        return await _dataContext.Messages.Include(u => u.Sender).Include(u => u.Recipient).SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = _dataContext.Messages
            .OrderByDescending(m => m.MessageSent)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
            "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
            _ => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
        };

        return await PagedList<MessageDto>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        var messages = await _dataContext.Messages
            .Where(m => m.Recipient.UserName == currentUsername && m.RecipientDeleted == false &&
                m.Sender.UserName == recipientUsername || m.Recipient.UserName == recipientUsername && m.SenderDeleted == false && m.Sender.UserName == currentUsername)
            .OrderBy(m => m.MessageSent)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var unreadMessages = messages.Where(m => m.DateRead == null
                                                 && m.RecipientUsername == currentUsername).ToList();

        if (unreadMessages.Any())
        {
            foreach (var message in unreadMessages)
            {
                message.DateRead = DateTime.UtcNow;
            }
        }

        return messages;
    }
}