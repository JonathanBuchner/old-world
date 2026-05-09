using System.Collections.Generic;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_gen_lib.Data.Rules
{
    public static class RuleData
    {
        public static readonly IReadOnlyDictionary<RuleName, Rule> Entries =
            new Dictionary<RuleName, Rule>
            {
                [RuleName.Add1Attr]				    = new Rule { NameId = RuleName.Add1Attr.ToString(),				    PointCost = 0 },
                [RuleName.Subtract1Attr]			= new Rule { NameId = RuleName.Subtract1Attr.ToString(),			PointCost = 0 },
                [RuleName.Add1Result]				= new Rule { NameId = RuleName.Add1Result.ToString(),				PointCost = 0 },
                [RuleName.Subtract1Result]			= new Rule { NameId = RuleName.Subtract1Result.ToString(),			PointCost = 0 },
                [RuleName.Reroll6]				    = new Rule { NameId = RuleName.Reroll6.ToString(),				    PointCost = 0 },
                [RuleName.Reroll1]				    = new Rule { NameId = RuleName.Reroll1.ToString(),				    PointCost = 0 },
                [RuleName.RerollSuccesses]			= new Rule { NameId = RuleName.RerollSuccesses.ToString(),			PointCost = 0 },
                [RuleName.RerollMisses]				= new Rule { NameId = RuleName.RerollMisses.ToString(),				PointCost = 0 },
                [RuleName.AlwaysHitOn2]			    = new Rule { NameId = RuleName.AlwaysHitOn2.ToString(),			    PointCost = 0 },
                [RuleName.AlwaysHitOn3]			    = new Rule { NameId = RuleName.AlwaysHitOn3.ToString(),			    PointCost = 0 },
                [RuleName.AlwaysHitOn4]			    = new Rule { NameId = RuleName.AlwaysHitOn4.ToString(),			    PointCost = 0 },
                [RuleName.AlwaysHitOn5]			    = new Rule { NameId = RuleName.AlwaysHitOn5.ToString(),             PointCost = 0 },
                [RuleName.AlwaysWoundOn2]			= new Rule { NameId = RuleName.AlwaysWoundOn2.ToString(),			PointCost = 0 },
                [RuleName.AlwaysWoundOn3]			= new Rule { NameId = RuleName.AlwaysWoundOn3.ToString(),			PointCost = 0 },
                [RuleName.AlwaysWoundOn4]			= new Rule { NameId = RuleName.AlwaysWoundOn4.ToString(),			PointCost = 0 },
                [RuleName.AlwaysWoundOn5]			= new Rule { NameId = RuleName.AlwaysWoundOn5.ToString(),			PointCost = 0 },
                [RuleName.Hatred]					= new Rule { NameId = RuleName.Hatred.ToString(),					PointCost = 0 },
                [RuleName.AdditionalHandWeapon]	    = new Rule { NameId = RuleName.AdditionalHandWeapon.ToString(),		PointCost = 0 },
                [RuleName.ArmorBane1]				= new Rule { NameId = RuleName.ArmorBane1.ToString(),				PointCost = 0 },
                [RuleName.ArmorBane2]				= new Rule { NameId = RuleName.ArmorBane2.ToString(),				PointCost = 0 },
                [RuleName.ArmorBane3]				= new Rule { NameId = RuleName.ArmorBane3.ToString(),				PointCost = 0 },
                [RuleName.ArmorBane3Charge]		    = new Rule { NameId = RuleName.ArmorBane3Charge.ToString(),			PointCost = 0 },
                [RuleName.FightExtraRank]			= new Rule { NameId = RuleName.FightExtraRank.ToString(),			PointCost = 0 },
                [RuleName.MoveAndShoot]			    = new Rule { NameId = RuleName.MoveAndShoot.ToString(),				PointCost = 0 },
                [RuleName.MultipleShots2]			= new Rule { NameId = RuleName.MultipleShots2.ToString(),			PointCost = 0 },
                [RuleName.MultipleShots3]			= new Rule { NameId = RuleName.MultipleShots3.ToString(),			PointCost = 0 },
                [RuleName.MultipleShots4]			= new Rule { NameId = RuleName.MultipleShots4.ToString(),			PointCost = 0 },
                [RuleName.Poison]					= new Rule { NameId = RuleName.Poison.ToString(),					PointCost = 0 },
                [RuleName.Ponderous]				= new Rule { NameId = RuleName.Ponderous.ToString(),				PointCost = 0 },
                [RuleName.QuickShot]				= new Rule { NameId = RuleName.QuickShot.ToString(),				PointCost = 0 },
                [RuleName.RequireToHands]			= new Rule { NameId = RuleName.RequireToHands.ToString(),			PointCost = 0 },
                [RuleName.StrengthPlus1Charge]		= new Rule { NameId = RuleName.StrengthPlus1Charge.ToString(),		PointCost = 0 },
                [RuleName.StrengthPlus2Charge]		= new Rule { NameId = RuleName.StrengthPlus2Charge.ToString(),		PointCost = 0 },
                [RuleName.StrengthPlus3Charge]		= new Rule { NameId = RuleName.StrengthPlus3Charge.ToString(),		PointCost = 0 },
                [RuleName.StrikeFirst]				= new Rule { NameId = RuleName.StrikeFirst.ToString(),				PointCost = 0 },
                [RuleName.StrikeLast]				= new Rule { NameId = RuleName.StrikeLast.ToString(),				PointCost = 0 },
                [RuleName.VollyFire]				= new Rule { NameId = RuleName.VollyFire.ToString(),				PointCost = 0 },
                [RuleName.PrimalFury]				= new Rule { NameId = RuleName.PrimalFury.ToString(),				PointCost = 0 },
                [RuleName.Backstab]				    = new Rule { NameId = RuleName.Backstab.ToString(),				    PointCost = 0 },
                [RuleName.MarkOfNurgle]			    = new Rule { NameId = RuleName.MarkOfNurgle.ToString(),				PointCost = 0 },
                [RuleName.DaemonOfNurgle]			= new Rule { NameId = RuleName.DaemonOfNurgle.ToString(),			PointCost = 0 },
                [RuleName.RuneOfStriking]			= new Rule { NameId = RuleName.RuneOfStriking.ToString(),			PointCost = 0 },
                [RuleName.RuneOfParrying]			= new Rule { NameId = RuleName.RuneOfParrying.ToString(),			PointCost = 0 },
                [RuleName.GrudgeRune]				= new Rule { NameId = RuleName.GrudgeRune.ToString(),				PointCost = 0 },
                [RuleName.RuneOfTheReckless]		= new Rule { NameId = RuleName.RuneOfTheReckless.ToString(),		PointCost = 0 },
                [RuleName.InnerCircle]				= new Rule { NameId = RuleName.InnerCircle.ToString(),				PointCost = 0 },
                [RuleName.GuardiansOfTheTemple]	    = new Rule { NameId = RuleName.GuardiansOfTheTemple.ToString(),		PointCost = 0 },
                [RuleName.TheWanderingDuelists]	    = new Rule { NameId = RuleName.TheWanderingDuelists.ToString(),		PointCost = 0 },
                [RuleName.CloudOfFlies]			    = new Rule { NameId = RuleName.CloudOfFlies.ToString(),				PointCost = 0 },
                [RuleName.DoL_WovenMist]			= new Rule { NameId = RuleName.DoL_WovenMist.ToString(),			PointCost = 0 }
            };
    }
}
