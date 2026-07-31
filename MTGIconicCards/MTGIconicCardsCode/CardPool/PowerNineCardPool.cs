using Godot;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;

#nullable enable
namespace MTGIconicCards.MTGIconicCardsCode.CardPool;

public class PowerNineCardPool : CustomCardPoolModel
{
  public const string energyColorName = "colorless";

  public override bool IsShared => true;

  public override bool SeenByDefault => true;

  public override string Title => "powernine";

  public override string EnergyColorName => "colorless";

  public override string CardFrameMaterialPath => "card_frame_colorless";

  public override Color DeckEntryCardColor => new Color("A3A3A3FF");

  public override bool IsColorless => true;
  
  protected override CardModel[] GenerateAllCards()
  {
    return new CardModel[9]
    {
      (CardModel)ModelDb.Card<AncestralRecall>(),
      (CardModel)ModelDb.Card<BlackLotus>(),
      (CardModel)ModelDb.Card<MoxEmerald>(),
      (CardModel)ModelDb.Card<MoxJet>(),
      (CardModel)ModelDb.Card<MoxPearl>(),
      (CardModel)ModelDb.Card<MoxRuby>(),
      (CardModel)ModelDb.Card<MoxSapphire>(),
      (CardModel)ModelDb.Card<TimeTwister>(),
      (CardModel)ModelDb.Card<TimeWalk>()
    };
  }
}