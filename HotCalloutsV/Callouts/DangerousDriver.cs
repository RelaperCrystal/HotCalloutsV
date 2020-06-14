﻿using HotCalloutsV.Common;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace HotCalloutsV.Callouts
{
    [CalloutInfo("DangerousDriver", CalloutProbability.Medium)]
    public class DangerousDriver : Callout
    {
        Ped suspect;
        Vehicle suspectCar;
        Vector3 spawn;
        Blip blip;
        private bool pursuited;

        public override bool OnBeforeCalloutDisplayed()
        {
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Initializing Instance > Dangerous Driver");
            spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(200f));

            ShowCalloutAreaBlipBeforeAccepting(spawn, 30f);
            AddMinimumDistanceCheck(20f, spawn);

            Game.LogTrivial("[Dangerous Driver/HotCallouts] Initialized Instance > Dangerous Driver");
            CalloutMessage = "Dangerous Driver";
            CalloutPosition = spawn;

            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_DANGEROUS_DRIVING IN_OR_ON_POSITION", spawn);
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Displayed Instance > Dangerous Driver");
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Accepted Instance > Dangerous Driver");
            suspectCar = new Vehicle(spawn);
            suspectCar.IsPersistent = true;

            Game.LogTrivial("[Dangerous Driver/HotCallouts] Spawned suspectCar (" + suspectCar.Model.Name + ") and set to Persistent");

            suspect = suspectCar.CreateRandomDriver();
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            Game.LogTrivial($"[Dangerous Driver/HotCallouts] Spawned suspect ({suspect.Model.Name}) and set to Persistent & Block Events");

            blip = suspect.AttachBlip();
            blip.IsFriendly = false;
            blip.IsRouteEnabled = true;
            blip.Name = "Reckless Driver";
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Spawned blip and renamed to Reckless Driver");

            suspect.Tasks.CruiseWithVehicle(15f, VehicleDrivingFlags.Emergency);
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Done > Dangerous Driver");
            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();
            if(!pursuited && Functions.IsPedInPursuit(suspect))
            {
                pursuited = true;
                Game.LogTrivial("[Dangerous Driver/HotCallouts] Fleeing > suspect");
                ScannerHelper.DisplayDispatchDialogue("Dispatch", "suspect fleeing.");
            }

            if(!suspect.Exists() || suspect.IsDead || Functions.IsPedArrested(suspect))
            {
                Game.LogTrivial("[Dangerous Driver/HotCallouts] End > Dangerous Driving");
                PedHelper.DeclareSubjectStatus(suspect);
                End();
            }

            /*if(!suspect == null || !suspect.Exists())
            {
                Game.DisplayNotification("<b>Dispatch: </b>Code 4, suspect has escaped.");
                End();
            }
            
            if(!suspect.IsAlive)
            {
                Game.DisplayNotification("<b>Dispatch: </b>Code 4, suspect down.");
                End();
            }
            if (Functions.IsPedArrested(suspect))
            {
                Game.DisplayNotification("<b>Dispatch: </b>Code 4, suspect in custody.");
                End();
            }
            */
        }

        public override void End()
        {
            base.End();
            Game.LogTrivial("[Dangerous Driver/HotCallouts] Ending Instance");

            if (suspect.Exists()) suspect.Dismiss();
            if (suspectCar.Exists()) suspectCar.Dismiss();
            if (blip.Exists()) blip.Delete();

            Game.LogTrivial("[Dangerous Driver/HotCallouts] Peds dismissed");
        }
    }
}