using AutoMapper;
using Chat.DTOs;
using Chat.Entities;
using Chat.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public MessageService(IMessageRepository messageRepository, IMapper mapper, IDistributedCache cache)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<MessageForReceiveDto>> GetAllMessages(int id)
        {
            var cachekey = $"GetAllMessages-{id}";
            var cachedata = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cachedata))
            {
                return JsonConvert.DeserializeObject<IEnumerable<MessageForReceiveDto>>(cachedata);
            }


            var messages = _messageRepository.GetAllByUserId(id);
            var data = _mapper.Map<IEnumerable<MessageForReceiveDto>>(messages);
            var cacheoptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)).SetAbsoluteExpiration(TimeSpan.FromHours(1));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(data), cacheoptions);

            return data;
        }

        public MessageForSendDto Send(MessageForSendDto _message, int receiverId, int senderId)
        {
            var messageget = _mapper.Map<Message>(_message);
           
            var data = _messageRepository.Insert(messageget, receiverId, senderId);

            return _mapper.Map<MessageForSendDto>(data);
        }
    }
}
