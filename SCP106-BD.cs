using System;
using Exiled.API.Features;

namespace SCP106_BD
{
    public class SCP106_BD : Plugin<Config>
    {
        public override string Name => "SCP106-BreaksDoors";
        public override string Author => "MarcosVLl2";
        public override Version Version { get; } = new Version(0, 0, 3);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 3);
        public static SCP106_BD Singleton { get; private set; }
        private EventHandlers Eventhandler { get; set; }
        private static Player rememberSCP106 { get; set; } = null;

        public SCP106_BD()
        {
            
        }
        public override void OnEnabled()
        {
            Log.Debug("Thank you for using my plugin! Any suggestions or bugfixes contact \"nombre_original#8857\"");
            Singleton = this;
            Eventhandler = new EventHandlers();
            Eventhandler.currentSCP106 = rememberSCP106;
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnregisterEvents();
            Eventhandler.ForceKillCoroutines();
            Eventhandler = null;
            Singleton = null;
            base.OnDisabled();
        }
        public override void OnReloaded()
        {
            rememberSCP106 = Eventhandler.currentSCP106;
            base.OnReloaded();
        }
        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole += Eventhandler.OnChangeRole;
            Exiled.Events.Handlers.Server.RoundStarted += Eventhandler.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += Eventhandler.OnRoundEnded;
            Exiled.Events.Handlers.Scp106.Containing += Eventhandler.Containing;
            Exiled.Events.Handlers.Player.Died += Eventhandler.OnDied;
        }
        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= Eventhandler.OnChangeRole;
            Exiled.Events.Handlers.Server.RoundStarted -= Eventhandler.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= Eventhandler.OnRoundEnded;
            Exiled.Events.Handlers.Scp106.Containing -= Eventhandler.Containing;
            Exiled.Events.Handlers.Player.Died -= Eventhandler.OnDied;
        }
    }
}