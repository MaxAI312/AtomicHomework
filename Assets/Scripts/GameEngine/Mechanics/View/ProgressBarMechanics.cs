using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Content
{
    [Serializable]
    public class ProgressBarMechanics
    {
        private readonly ProgressBar _bar;
        private readonly Countdown _countdown;

        public ProgressBarMechanics(ProgressBar bar, Conveyor_Core core)
        {
            _bar = bar;
            _countdown = core.ConvertComponent.Countdown;
        }

        public void Update()
        {
            float progress = 1 - (_countdown.RemainingTime / _countdown.Duration);
            _bar.SetProgress(progress);
        }
    }
}