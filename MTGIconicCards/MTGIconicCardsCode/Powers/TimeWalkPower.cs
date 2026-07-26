using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MTGIconicCards.MTGIconicCardsCode.Powers;

namespace MTGIconicCards.MTGIconicCardsCode.Powers;


public class TimeWalkPower() : MTGIconicCardsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override bool ShouldTakeExtraTurn(Player player)
    {
        return player == this.Owner.Player;
    }

    public override async Task AfterTakingExtraTurn(Player player)
    {
        TimeWalkPower power = this;
        if (player != power.Owner.Player)
            return;

        this.Flash();
        await PowerCmd.TickDownDuration(power);
    }
}