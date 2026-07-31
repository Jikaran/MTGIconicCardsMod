using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MTGIconicCards.MTGIconicCardsCode.Cards;

namespace MTGIconicCards.MTGIconicCardsCode.Cards;

[Pool(typeof(ColorlessCardPool))]
public class Brainstorm() : MTGIconicCardsCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        new CardsVar(3),
        new DynamicVar("PutBack", 2)
    ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Brainstorm source = this;
        await CreatureCmd.TriggerAnim(source.Owner.Creature, "Cast", source.Owner.Character.CastAnimDelay);
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, source.DynamicVars.Cards.BaseValue, source.Owner);
        CardSelectorPrefs prefs = new CardSelectorPrefs(this.SelectionScreenPrompt, 2);
        CardModel card = (
            await CardSelectCmd.FromHand(
                choiceContext, 
                source.Owner, 
                prefs, 
                (Func<CardModel, bool>) (null), 
                (AbstractModel) source
                )
            ).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Top);
    }

    protected override void OnUpgrade()
    {

    }
}