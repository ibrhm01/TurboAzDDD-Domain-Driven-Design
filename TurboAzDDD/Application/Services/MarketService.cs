using System;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Market;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MarketService : IMarketService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MarketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        /// 
        public async Task<int> CreateAsync(CreateMarketDto createDto)
        {

            if (await _unitOfWork.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == createDto.MarketName.Trim()))
                throw new DuplicateNameException("There is already a Market with this name");

            var mapped = _mapper.Map<Market>(createDto);

            await _unitOfWork.MarketRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateAsync(int id, UpdateMarketDto updateDto)
        {
            Market? market = await _unitOfWork.MarketRepository.GetByIdAsyncForAll(id);


            if (market is null) throw new EntityNotFoundException("There is no such Market");

            else if (await _unitOfWork.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == updateDto.MarketName.Trim()))
                throw new DuplicateNameException("There is already a Market with this name");

            else
            {
                var mapped = _mapper.Map(updateDto, market);
                await _unitOfWork.MarketRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<List<GetMarketDto>> GetAllAsync()
        {
            List<GetMarketDto> getMarketDtos = new();

            List<Market> markets = await _unitOfWork.MarketRepository.GetAllAsync();


            var mapped = _mapper.Map(markets, getMarketDtos);
            return mapped;

        }
        public async Task<GetMarketDto> GetOneAsync(int id)
        {
            GetMarketDto getMarketDto = new();

            Market? market = await _unitOfWork.MarketRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Market");

            var mapped = _mapper.Map(market, getMarketDto);
            return mapped;
        }

        public async Task<int> DeleteAsync(int id)
        {

            var market = await _unitOfWork.MarketRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Market");
            await _unitOfWork.MarketRepository.DeleteAsync(market);
            return await _unitOfWork.CompleteAsync();

        }
    }
}

