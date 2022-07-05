using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs;
using MEC;

namespace SCP106_BD
{

    internal sealed class EventHandlers
    {
        private static readonly CoroutineHandle[] handles = new CoroutineHandle[] { };
        private static readonly Config config = SCP106_BD.Singleton.Config;
        public Player currentSCP106 = null;
        public void OnRoundStarted()
        {
            Timing.RunCoroutine(DetectSCP106(), "DetectSCP106");
        }
        public void OnRoundEnded(RoundEndedEventArgs _)
        {
            Timing.KillCoroutines(handles);
        }
        public void OnDied(DiedEventArgs ev)
        {
            currentSCP106 = null;
            if (ev.Target.Role.Type == RoleType.Scp106)
            {
                Timing.KillCoroutines("106DoorHandler");
                Timing.ResumeCoroutines("DetectSCP106");
            }
        }
        public void Containing(ContainingEventArgs _)
        {
            currentSCP106 = null;
            Timing.KillCoroutines("106DoorHandler");
            Timing.ResumeCoroutines("DetectSCP106");
        }
        public void OnChangeRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp106)
            {
                {
                    Timing.RunCoroutine(DetectSCP106(), "DetectSCP106");
                };
            }
        }
        public void ForceKillCoroutines()
        {
            Timing.KillCoroutines(handles);
        }
        private IEnumerator<float> DetectSCP106()
        {
            while (true)
            {
                Log.Debug("Buscando SCP106");
                foreach (Player player in Player.List)
                {
                    if (player.Role.Type == RoleType.Scp106)
                    {
                        Log.Debug("Se ha encontrado!");
                        currentSCP106 = player;
                        Timing.RunCoroutine(SCP106DoorHandler(), "106DoorHandler");
                        yield return Timing.PauseCoroutines("DetectSCP106");
                    }
                    else
                    {
                        Log.Debug("No se ha encontrado");
                        yield return Timing.WaitForSeconds(config.Time_Per_SCP_Check);
                    }
                }
            }
        }
        private IEnumerator<float> SCP106DoorHandler()
        {
            while (!currentSCP106.IsDead)
            {
                foreach (Door door in Door.List)
                {
                    if (UnityEngine.Vector3.Distance(currentSCP106.Position, door.Position) <= 0.5F)
                    {
                        door.BreakDoor();
                    }
                }
                yield return Timing.WaitForSeconds(0.05F);
            }
        }
    }
}
