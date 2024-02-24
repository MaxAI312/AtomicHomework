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
            _core.Update(Time.deltaTime);
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
            _core.ConvertComponent.Countdown.Stop();
        }
    }

    [Serializable]
    public sealed class Conveyor_Core : IDisposable
    {
        public ConvertComponent ConvertComponent;

        public void Compose(ConveyourConfig config)
        {
            ConvertComponent.Compose(config);
        }

        public void OnEnable()
        {
            ConvertComponent.OnEnable();
        }

        public void OnDisable()
        {
            ConvertComponent.OnDisable();
        }

        public void Update(float deltaTime)
        {
            ConvertComponent.Update(deltaTime);
        }

        public void Dispose()
        {
            ConvertComponent.Dispose();
        }
    }

    [Serializable]
    public sealed class Conveyor_View
    {
        [SerializeField] private List<GameObject> _loadGameObjects;
        [SerializeField] private List<GameObject> _unloadGameObjects;
        [SerializeField] private Animator _animator;
        [SerializeField] private ProgressBar _progressBar;

        private ResourceZoneView _resourceZoneView;

        private WorkAnimMechanics _workAnimMechanics;
        private TransferResourcesMechanics _transferResourcesMechanics;
        private ProgressBarMechanics _progressBarMechanics;
        
        public void Compose(Conveyor_Core core)
        {
            _resourceZoneView = new ResourceZoneView(
                core.ConvertComponent.IngredientCount,
                core.ConvertComponent.ResultCount,
                _loadGameObjects,
                _unloadGameObjects);

            _progressBarMechanics = new ProgressBarMechanics(_progressBar, core.ConvertComponent.Countdown);
            _workAnimMechanics = new WorkAnimMechanics(_animator, core.ConvertComponent.ChangeEnabledObservable);
            
            _transferResourcesMechanics = new TransferResourcesMechanics(
                core.ConvertComponent.ChangeCountObservable,
                _resourceZoneView);
        }

        public void OnEnable()
        {
            _resourceZoneView.OnEnable();
            _workAnimMechanics.OnEnable();
            _transferResourcesMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _workAnimMechanics.OnDisable();
            _transferResourcesMechanics.OnDisable();
        }
        
        public void Update()
        {
            _progressBarMechanics.Update();
        }
    }
}