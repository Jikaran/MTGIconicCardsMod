using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MTGIconicCards.MTGIconicCardsCode.Cards;
using MTGIconicCards.MTGIconicCardsCode.Relics;

namespace MTGIconicCards.MTGIconicCardsCode.Relics;


[Pool(typeof(SharedRelicPool))]
public class BottledBolt() : MTGIconicCardsRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<LightningBolt>()];
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        BottledBolt bottledBolt = this;
        
        if (player != bottledBolt.Owner)
            return;

        List<CardModel> lightningBolt = CardFactory.GetDistinctForCombat(bottledBolt.Owner,
            [ModelDb.Card<LightningBolt>()], 1, bottledBolt.Owner.RunState.Rng.CombatCardGeneration).ToList();

        if (lightningBolt.Count == 0)
            return;
        
        bottledBolt.Flash();

        foreach (CardModel card in lightningBolt)
        {
            card.SetToFreeThisTurn();
            CardKeyword[] cardKeywordArray = new CardKeyword[1]
            {
                CardKeyword.Exhaust
            };
            CardCmd.ApplyKeyword(card, cardKeywordArray);
        }
        await CardPileCmd.AddGeneratedCardsToCombat(lightningBolt, PileType.Hand, bottledBolt.Owner);
        
    }
}