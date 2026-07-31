using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MTGIconicCards.MTGIconicCardsCode.CardPool;
using MTGIconicCards.MTGIconicCardsCode.Cards;

namespace MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;

[Pool(typeof(PowerNineCardPool))]
public class TimeTwister() : PowerNine(3,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new CardsVar(7)
        ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        TimeTwister timeTwister = this;
        
        await CreatureCmd.TriggerAnim(timeTwister.Owner.Creature, "Cast", timeTwister.Owner.Character.CastAnimDelay);





        foreach (Creature creature in timeTwister.CombatState.GetTeammatesOf(timeTwister.Owner.Creature)
                     .Where<Creature>((Func<Creature, bool>)(c => c != null && c.IsAlive && c.IsPlayer)))
        {
            foreach (CardModel card in PileType.Hand.GetPile(creature.Player).Cards.ToList<CardModel>())
            {
                CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Draw);
            }
            await CardPileCmd.Shuffle(choiceContext, creature.Player);
            await CardPileCmd.DrawWithoutBlockingOnOtherPlayers(choiceContext, timeTwister.DynamicVars.Cards.BaseValue,
                creature.Player, this);
        }
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}