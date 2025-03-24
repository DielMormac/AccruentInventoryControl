using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Constants;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;
using AutoMapper;

namespace AccruentInventoryControl.Application.Mappers
{
    public class WarehouseTransactionReportMapper : Profile
    {
        public WarehouseTransactionReportMapper()
        {
            CreateMap<WarehouseTransactionReportResponse, WarehouseTransactionReport>()
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.WarehouseTransactions, opt => opt.MapFrom(src => src.WarehouseTransactions));

            CreateMap<WarehouseTransactionReport, WarehouseTransactionReportResponse>()
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.WarehouseTransactions, opt => opt.MapFrom(src => src.WarehouseTransactions));

            CreateMap<WarehouseTransaction, WarehouseTransactionResponse>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetWarehouseTransactionStatus(src.Status)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.PreviousQuantity, opt => opt.MapFrom(src => src.PreviousQuantity))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity));
        }

        private string GetWarehouseTransactionStatus(WarehouseTransactionStatus status)
        {
            switch (status)
            {
                case WarehouseTransactionStatus.Pending:
                    return WarehouseTransactionStatusConstants.Pending;
                case WarehouseTransactionStatus.OutOfStock:
                    return WarehouseTransactionStatusConstants.OutOfStock;
                case WarehouseTransactionStatus.Completed:
                    return WarehouseTransactionStatusConstants.Completed;
                case WarehouseTransactionStatus.Cancelled:
                    return WarehouseTransactionStatusConstants.Cancelled;
                default:
                    return WarehouseTransactionStatusConstants.Unknown;
            }
        }
    }
}
