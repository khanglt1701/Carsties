using System;
using AuctionService.Data;
using Grpc.Core;

namespace AuctionService.Services;

public class GrpcAuctionService(AuctionDbContext dbContext) : GrpcAuction.GrpcAuctionBase
{
    private readonly AuctionDbContext _dbContext = dbContext;

    public override async Task<GrpcAuctionReponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
    {
        System.Console.WriteLine("==> Received Grpc request for auction");

        var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(request.Id)) ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
        var response = new GrpcAuctionReponse
        {
          Auction = new GrpcAuctionModel
          {
            AuctionEnd = auction.AuctionEnd.ToString(),
            Id = auction.Id.ToString(),
            ReservePrice = auction.ReservePrice,
            Seller = auction.Seller
          }
        };

        return response;
    }
}