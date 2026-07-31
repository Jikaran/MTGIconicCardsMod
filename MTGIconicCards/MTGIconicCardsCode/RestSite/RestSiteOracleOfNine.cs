using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using MTGIconicCards.MTGIconicCardsCode.CardPool;
using MTGIconicCards.MTGIconicCardsCode.Cards.PowerNine;
using MTGIconicCards.MTGIconicCardsCode.Extensions;

namespace MTGIconicCards.MTGIconicCardsCode.RestSite;

public class RestSiteOracleOfNine(Player owner) : RestSiteOption(owner){
    
    public override string OptionId => "ORACLE_OF_NINE";

    public override async Task<bool> OnSelect()
    {
        RestSiteOracleOfNine restSiteOracleOfNine = this;
        
        CardCreationOptions options1 = new CardCreationOptions(
            new List<CardPoolModel>([ModelDb.CardPool<PowerNineCardPool>()]
            ),
                
                CardCreationSource.Other,
                CardRarityOddsType.Uniform
        
            );
        List<CardModel> options = CardFactory.CreateForReward(restSiteOracleOfNine.Owner, 3, options1).Select<CardCreationResult, CardModel>((Func<CardCreationResult, CardModel>) (c => c.Card)).ToList<CardModel>();
        CardModel chosenCard = await CardSelectCmd.FromChooseACardScreen((PlayerChoiceContext) new BlockingPlayerChoiceContext(), (IReadOnlyList<CardModel>) options, restSiteOracleOfNine.Owner, true);
        if (chosenCard != null)
            CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(chosenCard, PileType.Deck));
        foreach (CardModel card in options)
        {
            if (card != chosenCard)
                restSiteOracleOfNine.Owner.RunState.CurrentMapPointHistoryEntry?.GetEntry(restSiteOracleOfNine.Owner.NetId).CardChoices.Add(new CardChoiceHistoryEntry(card, false));
        }
        options = (List<CardModel>) null;
        chosenCard = (CardModel) null;
        
        return true;
        
    }
    
    public override Task DoLocalPostSelectVfx(CancellationToken ct = default (CancellationToken))
    {
        NDebugAudioManager.Instance.Play("sts_sfx_shovel_v1.mp3", variance: PitchVariance.Small);
        return Task.CompletedTask;
    }
    
    
}
