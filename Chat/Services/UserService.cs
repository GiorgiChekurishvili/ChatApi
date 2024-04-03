using AutoMapper;
using Chat.DTOs;
using Chat.Entities;
using Chat.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Chat.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _Repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public UserService(IUserRepository repository, IMapper mapper, IDistributedCache cache)
        {
            _Repository = repository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<UserForReturnDto>> GetAllUsers()
        {
            var cachekey = "GetAllUsers";
            var cachedata = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cachedata))
            {
                return JsonConvert.DeserializeObject<IEnumerable<UserForReturnDto>>(cachedata);
            }


            var users = _Repository.GetAll();
            var data = _mapper.Map<IEnumerable<UserForReturnDto>>(users);
            var cacheoptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(3)).SetAbsoluteExpiration(TimeSpan.FromHours(1));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(data), cacheoptions);
            
            return _mapper.Map<IEnumerable<UserForReturnDto>>(users);
        }

        public UserForReturnDto GetById(int id)
        {
            var userbyid = _Repository.GetById(id);
            return _mapper.Map<UserForReturnDto>(userbyid);
        }
    }
}
