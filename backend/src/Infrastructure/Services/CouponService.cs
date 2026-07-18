using JAS.ECommerce.Application.DTOs.Auth;
using JAS.ECommerce.Application.DTOs.Coupon;
using JAS.ECommerce.Application.Interfaces;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Enums;
using JAS.ECommerce.Domain.Interfaces;

namespace JAS.ECommerce.Infrastructure.Services;

public class CouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CouponService> _logger;

    public CouponService(IUnitOfWork unitOfWork, ILogger<CouponService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CouponDto?> ValidateCouponAsync(string code, decimal orderAmount)
    {
        try
        {
            var coupons = await _unitOfWork.CouponRepository.GetAllAsync();
            var coupon = coupons.FirstOrDefault(c => c.Code == code && c.IsActive);

            if (coupon == null)
            {
                _logger.LogWarning($"Coupon not found: {code}");
                return null;
            }

            if (coupon.StartDate > DateTime.UtcNow || (coupon.EndDate.HasValue && coupon.EndDate < DateTime.UtcNow))
            {
                _logger.LogWarning($"Coupon expired or not started: {code}");
                return null;
            }

            if (coupon.UsageLimit.HasValue && coupon.CurrentUsageCount >= coupon.UsageLimit)
            {
                _logger.LogWarning($"Coupon usage limit exceeded: {code}");
                return null;
            }

            if (coupon.MinPurchaseAmount.HasValue && orderAmount < coupon.MinPurchaseAmount)
            {
                _logger.LogWarning($"Order amount below minimum for coupon: {code}");
                return null;
            }

            var discountAmount = CalculateDiscount(coupon, orderAmount);
            return new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Type = (int)coupon.Type,
                Value = coupon.Value,
                DiscountAmount = discountAmount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating coupon");
            throw;
        }
    }

    public async Task<bool> ApplyCouponAsync(int couponId, int userId, int orderId)
    {
        try
        {
            var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponId)
                ?? throw new KeyNotFoundException("Coupon not found");

            var usage = new CouponUsage
            {
                CouponId = couponId,
                UserId = userId,
                OrderId = orderId,
                UsedAt = DateTime.UtcNow
            };

            coupon.CurrentUsageCount++;
            _unitOfWork.CouponRepository.Update(coupon);
            await _unitOfWork.CouponUsageRepository.AddAsync(usage);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Coupon applied: {coupon.Code}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying coupon");
            throw;
        }
    }

    private static decimal CalculateDiscount(Coupon coupon, decimal orderAmount)
    {
        decimal discount = coupon.Type switch
        {
            CouponType.Percentage => (orderAmount * coupon.Value) / 100,
            CouponType.FixedAmount => coupon.Value,
            CouponType.FreeShipping => 0, // Handled separately
            _ => 0
        };

        if (coupon.MaxDiscount.HasValue && discount > coupon.MaxDiscount)
        {
            discount = coupon.MaxDiscount.Value;
        }

        return discount;
    }
}
