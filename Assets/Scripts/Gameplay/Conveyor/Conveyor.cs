using System;
using System.Collections.Generic;
using Game.Gameplay.Conveyors;
using GameEngine;
using UnityEngine;

namespace Content
{
    public sealed class Conveyor : MonoBehaviour, IStopable
    {
        [SerializeField] private ConveyourConfig _config;
        [SerializeField] private Conveyor_Core _core;
        [SerializeField] private Conveyor_View _view;

        public void Awake()
        {
            _core.Compose(_config);
            _view.Compose(_core);
        }

        private void OnEnable()
        {
            _core.OnEnable();
            _view.OnEnable();
        }

        private void Update()
        {
            _core.Update();
            _view.Update();
        }

        private void OnDisable()
        {
            _core.OnDisable();
            _view.OnDisable();
        }

        private void OnDestroy()
        {
            _core.Dispose();
        }

        public void Stop()
        {
            _core.Countdown.Stop();
        }
    }

    [Serializable]
    public sealed class Conveyor_Core : IDisposable
    {
        public Countdown Countdown;
        
        public ConvertComponent ConvertComponent;

        public void Compose(ConveyourConfig config)
        {
            Countdown = new Countdown(config.workTime);
            ConvertComponent.Compose(Countdown, config);
        }

        public void OnEnable()
        {
            ConvertComponent.OnEnable();
        }

        public void OnDisable()
        {
            ConvertComponent.OnDisable();
        }

        public void Update()
        {
            Countdown.Tick(Time.deltaTime);
            ConvertComponent.Update();
            if (Countdown.IsWorking && ConvertComponent.Enabled.Value == false)
            {
                Countdown.Stop();
                Countdown.Reset();
            }
        }

        public void Dispose()
        {
            ConvertComponent.Dispose();
        }
    }

    [Serializable]
    public sealed class Conveyor_View
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private List<GameObject> _loadGameObjects;
        [SerializeField] private List<GameObject> _unloadGameObjects;

        private ConvertAnimMechanics _convertAnimMechanics;
        private WaitingAnimMechanics _waitingAnimMechanics;
        private TransferResourcesMechanics _transferResourcesMechanics;
        private ProgressBarMechanics _progressBarMechanics;
        
        public void Compose(Conveyor_Core core)
        {
            _progressBarMechanics = new ProgressBarMechanics(_progressBar, core);
            _convertAnimMechanics = new ConvertAnimMechanics(_animator, core.ConvertComponent.ChangeEnabledObservable);
            _waitingAnimMechanics = new WaitingAnimMechanics(_animator, core.ConvertComponent.ChangeEnabledObservable);
            _transferResourcesMechanics = new TransferResourcesMechanics(
                _loadGameObjects,
                _unloadGameObjects,
                core.ConvertComponent.ChangeCountObservable,
                core.ConvertComponent.ResultCount);
        }

        public void OnEnable()
        {
            _waitingAnimMechanics.OnEnable();
            _convertAnimMechanics.OnEnable();
            _transferResourcesMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _waitingAnimMechanics.OnDisable();
            _convertAnimMechanics.OnDisable();
            _transferResourcesMechanics.OnDisable();
        }
        
        public void Update()
        {
            _progressBarMechanics.Update();
        }
    }
}