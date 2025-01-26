using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JFrame
{
    public class ActionSM : BaseSMSync<CombatManager, BaseActionState, ActionSMTrigger>
    {
        ActionInitState actionInitState;
        ActionDisableState actionDisableState;
        ActionStandbyState actionStandbyState;
        ActionExecutingState actionExecutingState;
        ActionCdingState actionCdingState;
        ActionCrowdControlState actionCrowdControlState;

        protected override List<BaseActionState> GetAllStates()
        {
            var states = new List<BaseActionState>();

            actionInitState = new ActionInitState();
            actionDisableState = new ActionDisableState();
            actionStandbyState = new ActionStandbyState();
            actionExecutingState = new ActionExecutingState();
            actionCdingState = new ActionCdingState();
            actionCrowdControlState = new ActionCrowdControlState();


            states.Add(actionInitState);
            states.Add(actionDisableState);
            states.Add(actionStandbyState);
            states.Add(actionExecutingState);
            states.Add(actionCdingState);
            states.Add(actionCrowdControlState);

            return states;
        }

        protected override Dictionary<string, SMConfig> GetConfigs()
        {
            var configs = new Dictionary<string, SMConfig>();

            var initName = actionInitState.Name;
            var initConfig = new SMConfig();
            initConfig.state = actionInitState;
            initConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            initConfig.dicPermit.Add(ActionSMTrigger.Disable, actionDisableState);
            configs.Add(initName, initConfig);


            var disableName = actionDisableState.Name;
            var disableConfig = new SMConfig();
            disableConfig.state = actionDisableState;
            disableConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            disableConfig.dicPermit.Add(ActionSMTrigger.Standby, actionStandbyState);
            configs.Add(disableName, disableConfig);

            var standbyName = actionStandbyState.Name;
            var standbyConfig = new SMConfig();
            standbyConfig.state = actionStandbyState;
            standbyConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            standbyConfig.dicPermit.Add(ActionSMTrigger.Execute, actionExecutingState);
            configs.Add(standbyName, standbyConfig);

            var executingName = actionExecutingState.Name;
            var executingConfig = new SMConfig();
            executingConfig.state = actionExecutingState;
            executingConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            executingConfig.dicPermit.Add(ActionSMTrigger.Cd, actionCdingState);
            configs.Add(executingName, executingConfig);

            var cdingName = actionCdingState.Name;
            var cdingConfig = new SMConfig();
            cdingConfig.state = actionCdingState;
            cdingConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            cdingConfig.dicPermit.Add(ActionSMTrigger.Standby, actionStandbyState);
            configs.Add(cdingName, cdingConfig);


            var crowControlName = actionCrowdControlState.Name;
            var crowControlConfig = new SMConfig();
            crowControlConfig.state = actionCrowdControlState;
            crowControlConfig.dicPermit = new Dictionary<ActionSMTrigger, BaseActionState>();
            crowControlConfig.dicPermit.Add(ActionSMTrigger.Cd, actionCdingState);
            configs.Add(crowControlName, crowControlConfig);
            

            return configs;
        }
    }
}
