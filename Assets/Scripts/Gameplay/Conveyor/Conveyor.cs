using System;
using Atomic.Elements;
using Game.Gameplay.Conveyors;
using GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Content
{
    //TODO: Реализовать конвертер ресурсов   
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
            //_view.
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

        public void Start()
        {
        }

        public void Update()
        {
            Countdown.Tick(Time.deltaTime);
            ConvertComponent.Update();
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

        private ConvertAnimMechanics _convertAnimMechanics;
        private WaitingAnimMechanics _waitingAnimMechanics;
        private TransferResourcesMechanics _transferResourcesMechanics;
        private ProgressBarMechanics _progressBarMechanics;
        
        public void Compose(Conveyor_Core core)
        {
            _progressBarMechanics = new ProgressBarMechanics(_progressBar, core);
            _convertAnimMechanics = new ConvertAnimMechanics(_animator, core.ConvertComponent.OnConvertedEvent);
            //_waitingAnimMechanics = new WaitingAnimMechanics(_animator, core.)
        }

        public void OnEnable()
        {
            //_waitingAnimMechanics.OnEnable();
            _convertAnimMechanics.OnEnable();
        }

        public void OnDisable()
        {
            //_waitingAnimMechanics.OnDisable();
            _convertAnimMechanics.OnDisable();
        }
        
        public void Update()
        {
            _progressBarMechanics.Update();
        }
        
        
    }
}