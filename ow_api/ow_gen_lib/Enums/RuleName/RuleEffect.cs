namespace ow_gen_lib.Enums.SpecialRules
{
    public enum RuleEffect
    {
        #region Custom / Testing
        
        // Calc rules
        Add1Attr,
        Subtract1Attr,
        Add1Result,
        Subtract1Result,
        Reroll6,
        Reroll1,
        RerollSuccesses,
        RerollMisses,

        #endregion

        #region Base Game Rules

        #region General Rules
        Hatred,                 // Reroll misses                            
        CleavingBlow,           // 6's may ignore arm & rege                    https://tow.whfb.app/special-rules/cleaving-blow
        ElvenReflexes,          // +1 Initiative first round of combat          https://tow.whfb.app/special-rules/elven-reflexes
        KillingBlow,            // 6's may ignore arm & rege & multi wound      https://tow.whfb.app/special-rules/killing-blow                    
        MartialProwess,         // Supporting attacks to flank or rear          https://tow.whfb.app/special-rules/martial-prowess
        MonsterSlayer,
        WillOfTheGods,
        ArmorBane1,
        ArmorBane2,
        ArmorBane3,
        ArmorBane4,
        ArmorBane5,
        AlwaysHitOn2,
        AlwaysHitOn3,
        AlwaysHitOn4,
        AlwaysHitOn5,
        AlwaysWoundOn2,
        AlwaysWoundOn3,
        AlwaysWoundOn4,
        AlwaysWoundOn5,
        AlwaysArmorSaveOn2,
        AlwaysArmorSaveOn3,
        AlwaysArmorSaveOn4,
        AlwaysArmorSaveOn5,
        AlwaysArmorSaveOn6,

        #endregion

        #region Weapon
        AdditionalHandWeapon,
        ArmorBane3Charge,
        FightExtraRank,
        MoveAndShoot,
        MultipleShots2,
        MultipleShots3,
        MultipleShots4,
        Poison,
        Ponderous,
        QuickShot,
        RequireToHands,
        StrengthPlus1Charge,
        StrengthPlus2Charge,
        StrengthPlus3Charge,
        StrikeFirst,
        StrikeLast,
        VollyFire,
        #endregion

        #endregion

        #region ArmyBooks

        #region Beastmen
        PrimalFury,             // Reroll at 1
        #endregion

        #region Chaos Dwarves
        Backstab,               // Sometimes hatred     https://tow.whfb.app/special-rules/backstab 
        #endregion

        #region Demons
        MarkOfNurgle,           // -1 hit               https://tow.whfb.app/special-rules/mark-of-nurgle 
        DaemonOfNurgle,         // -1 hit               https://tow.whfb.app/special-rules/daemon-of-nurgle 
        RuneOfStriking,         // +1 WS                https://tow.whfb.app/magic-item/rune-of-striking 
        RuneOfParrying,         // -1 to hit            https://tow.whfb.app/magic-items/weapon-runes 
        GrudgeRune,             // Reroll nat 1         https://tow.whfb.app/magic-items/weapon-runes 
        #endregion

        #region Dwarves
        RuneOfTheReckless,      // + 1 hit                https://tow.whfb.app/magic-item/rune-of-the-reckless 
        #endregion

        #region Empire of Man
        InnerCircle,            // Reroll nat 1         https://tow.whfb.app/special-rules/inner-circle 
        GuardiansOfTheTemple,   // Reroll nat 1         https://tow.whfb.app/special-rules/guardians-of-the-temple
        #endregion

        #region High Elves
        #endregion

        #region DarkElves
        EternalHatred,          // Hatred against High Elves in every round      https://tow.whfb.app/special-rules/eternal-hatred
        HekartisBlessing,       // Reroll failed Casting roll                   https://tow.whfb.app/special-rules/hekartis-blessing
        Murderous,              // Reroll natural 1 To Wound in combat          https://tow.whfb.app/special-rules/murderous
        SeaDragonCloak,         // +1 armour value against non-magical shooting https://tow.whfb.app/special-rules/sea-dragon-cloak
        #endregion

        #region Realms of ment
        TheWanderingDuelists,    // +1 WS && sometimes re-roll successful rolls To Hit
        #endregion

        #region Skaven
        CloudOfFlies,           // -1 hit.              https://tow.whfb.app/special-rules/cloud-of-flies 
        #endregion

        #region Wood elves
        DoL_WovenMist,          // -1 hit.              https://tow.whfb.app/special-rules/dances-of-loec 
        #endregion

        #endregion ArmyBooks

    }
}
