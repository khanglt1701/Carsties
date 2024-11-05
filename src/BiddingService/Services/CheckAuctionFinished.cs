using System;
using BiddingService.Models;
using Contracts;
using MassTransit.Transports;
using MongoDB.Entities;

namespace BiddingService.Services;

public class CheckAuctionFinished(ILogger<CheckAuctionFinished> logger, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly ILogger<CheckAuctionFinished> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting check for finished actions");

        stoppingToken.Register(() => _logger.LogInformation("===> Auction check is stopping"));

        while (!stoppingToken.IsCancellationRequested)
        {
          await CheckAuctions(stoppingToken);

          await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task CheckAuctions(CancellationToken stoppingToken)
    {
        var finishedAuctions = await DB.Find<Auction>()
            .Match(x => x.AuctionEnd <= DateTime.UtcNow)
            .Match(x => !x.Finished)
            .ExecuteAsync(stoppingToken);

        if (finishedAuctions.Count == 0) return;

        _logger.LogInformation($"==> Found {finishedAuctions.Count} auctions that have completed");

        var scope = _serviceProvider.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<PublishEndpoint>();

        foreach (var auction in finishedAuctions)
        {
            auction.Finished = true;
            await auction.SaveAsync(null, stoppingToken);

            var winningBid = await DB.Find<Bid>()
                .Match(x => x.AuctionId == auction.ID)
                .Match(x => x.BidStatus == BidStatus.Accepted)
                .Sort(x => x.Descending(s => s.Amount))
                .ExecuteFirstAsync(stoppingToken);

            await endpoint.Publish(new AuctionFinished
            {
                ItemSold = winningBid != null,
                AuctionId = auction.ID,
                Winner = winningBid?.Bidder,
                Amount = winningBid?.Amount,
                Seller = auction.Seller
            }, stoppingToken);
        }
    }
}
