﻿using UnityEngine;
using Verse;
using RimWorld;
using Verse.Sound;
using static ResearchPal.ResourceBank.String;

namespace ResearchPal
{
  public class Settings : ModSettings
    {
        #region tuning parameters

        public static bool shouldPause;
        public static bool shouldReset;
        public static bool shouldSeparateByTechLevels;

        public static bool alignToAncestors = false;

        public static bool placeModTechSeparately = true;

        public static int largeModTechCount = 5;

        public static bool delayLayoutGeneration = false;

        public static bool searchByDescription = false;

        public static bool asyncLoadingOnStartup = false;

        public static bool progressTooltip = true;

        public static bool alwaysDisplayProgress = true;

        public static bool dontIgnoreHiddenPrerequisites = true;

        public static bool showIndexOnQueue = false;

        public static float scrollingSpeedMultiplier = 1f;
        public static float zoomingSpeedMultiplier = 1f;
        public static float draggingDisplayDelay = 0.25f;

        #endregion tuning parameters

        public static void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard list = new Listing_Standard(GameFont.Small);
            list.ColumnWidth = rect.width / 2;
            list.Begin(rect);

            if (list.ButtonText(ResetTreeLayout)) {
                SoundDefOf.Click.PlayOneShotOnCamera();
                if (Tree.ResetLayout()) {
                    Messages.Message(
                        LayoutRegenerated, MessageTypeDefOf.CautionInput, false);
                }
            }

            list.CheckboxLabeled(
               DontIgnoreHiddenPrerequisites,
               ref dontIgnoreHiddenPrerequisites,
               DontIgnoreHiddenPrerequisitesTip);

            list.CheckboxLabeled(
                ShouldSeparateByTechLevels,
                ref shouldSeparateByTechLevels,
                ShouldSeparateByTechLevelsTip);
            list.CheckboxLabeled(
                AlignCloserToAncestors,
                ref alignToAncestors,
                AlignCloserToAncestorsTip);
            list.CheckboxLabeled(
                PlaceModTechSeparately,
                ref placeModTechSeparately,
                PlaceModTechSeparatelyTip);
            if (placeModTechSeparately) {
                list.Label(MinimumSeparateModTech, -1, MinimumSeparateModTechTip);
                string buffer = largeModTechCount.ToString();
                list.IntEntry(ref largeModTechCount, ref buffer);
            }
            list.CheckboxLabeled(
                SearchByDescription,
                ref searchByDescription,
                SearchByDescriptionTip);
            list.Gap();

            list.CheckboxLabeled(
                ShouldPauseOnOpen, ref shouldPause, ShouldPauseOnOpenTip);
            list.CheckboxLabeled(
                ShouldResetOnOpen, ref shouldReset, ShouldResetOnOpenTip);
            if (!asyncLoadingOnStartup || delayLayoutGeneration) {
                list.CheckboxLabeled(
                    DelayLayoutGeneration,
                    ref delayLayoutGeneration,
                    DelayLayoutGenerationTip);
            }
            if (!delayLayoutGeneration) {
                list.CheckboxLabeled(
                    AsyncLoadingOnStartup,
                    ref asyncLoadingOnStartup,
                    AsyncLoadingOnStartupTip);
            }
            list.Gap();

            list.CheckboxLabeled(ProgressTooltip, ref progressTooltip, ProgressTooltipTip);
            list.CheckboxLabeled(AlwaysDisplayProgress, ref alwaysDisplayProgress, AlwaysDisplayProgressTip);
            list.CheckboxLabeled(ShowIndexOnQueue, ref showIndexOnQueue, ShowIndexOnQueueTip);

            list.Gap();
            list.Label(
                "ResearchPal.ScrollSpeedMultiplier".Translate()
                    + string.Format(" {0:0.00}", scrollingSpeedMultiplier), -1,
                "ResearchPal.ScrollSpeedMultiplierTip".Translate());
            scrollingSpeedMultiplier = list.Slider(scrollingSpeedMultiplier, 0.1f, 5);
            list.Label(
                "ResearchPal.ZoomingSpeedMultiplier".Translate()
                    + string.Format(" {0:0.00}", zoomingSpeedMultiplier), -1,
                "ResearchPal.ZoomingSpeedMultiplierTip".Translate());
            zoomingSpeedMultiplier = list.Slider(zoomingSpeedMultiplier, 0.1f, 5);
            list.Label(
                "ResearchPal.DraggingDisplayDelay".Translate()
                    + string.Format(": {0:0.00}s", draggingDisplayDelay), -1,
                "ResearchPal.DraggingDisplayDelayTip".Translate());
            draggingDisplayDelay = list.Slider(draggingDisplayDelay, 0, 1);
            list.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref shouldSeparateByTechLevels, "ShouldSeparateByTechLevels", false);
            Scribe_Values.Look(ref shouldPause, "ShouldPauseOnOpen", true);
            Scribe_Values.Look(ref shouldReset, "ShouldResetOnOpen", false);
            Scribe_Values.Look(ref alignToAncestors, "AlignCloserToAncestors", false);
            Scribe_Values.Look(ref placeModTechSeparately, "placeModTechsSeparately", true);
            Scribe_Values.Look(ref largeModTechCount, "MinimumSeparateModTech", 5);
            Scribe_Values.Look(ref searchByDescription, "SearchByDescription", false);
            Scribe_Values.Look(ref delayLayoutGeneration, "DelayResearchLayoutGeneration", false);
            Scribe_Values.Look(ref asyncLoadingOnStartup, "AsyncLoadingOnStartup", false);
            Scribe_Values.Look(ref progressTooltip, "ProgressTooltip", false);
            Scribe_Values.Look(ref alwaysDisplayProgress, "AlwaysDisplayProgress", false);
            Scribe_Values.Look(ref showIndexOnQueue, "ShowQueuePositionOnQueue", false);
            Scribe_Values.Look(ref dontIgnoreHiddenPrerequisites, "dontIgnoreHiddenPrerequisites", true);
            Scribe_Values.Look(ref scrollingSpeedMultiplier, "ScrollingSpeedMultiplier", 1);
            Scribe_Values.Look(ref zoomingSpeedMultiplier, "zoomingSpeedMultiplier", 1);
            Scribe_Values.Look(ref draggingDisplayDelay, "zoomingSpeedMultiplier", 0.25f);
        }
    }
}