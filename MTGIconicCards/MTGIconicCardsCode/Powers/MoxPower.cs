using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MTGIconicCards.MTGIconicCardsCode.Powers;

namespace MTGIconicCards.MTGIconicCardsCode.Powers;


public class MoxPower() : MTGIconicCardsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != this.Owner.Player ? amount : amount + (Decimal) this.Amount;
    }    
}